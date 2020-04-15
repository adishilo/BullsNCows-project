using System;
using System.Collections.Generic;
using System.Linq;

namespace BullsNCowsEngine.RealUnEngine
{
    public class Sherlock
    {
        private readonly string m_targetNumber;
        private readonly int m_digitsCount;
        private readonly int m_strength;

        public List<string> m_possibleGuesses = new List<string>();

        /**
         * summary: Function gets as parameter a sequence of digits (a number) in string form,
         * and returns whether or not the input is legal according to the game's rules (no double digits, certain lentgh)
         *
         * param name="number": A number to be checked if legal by the game's ruls
         */
        public bool IsLegalNumber(string number)
        {
            if (number.Length != m_digitsCount)
            {
                return false;
            }

            for (int i = 0; i < number.Length; i++)
            {
                for (int j = i + 1; j < number.Length; j++)
                {
                    if (number[i] == number[j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /**
         *summary: Construct a new object of the <see cref="Sherlock"/> class, with 2 integers as parameters: "digitsCount" and "strength".
         * It also add to the list <see cref="m_possibleGuesses"/> all of the possible guesses at the start of the game using the <see cref="m_digitsCount"/>
         * parameter and the <see cref="IsLegalNumber(string)"/> function, then randomly choose a number out of the list to be the "secret number".
         *
         * param name="digitsCount"
         * param name="strength"
         *
         * assumption: <see cref="m_digitsCount"/> and <see cref="m_strength"/> have values/default values given to them by the GameManager taken from the SP file
         */
        public Sherlock(int digitsCount, int strength)
        {
            m_digitsCount = digitsCount;
            m_strength = strength;
            double countPossibilities = Math.Pow(10, m_digitsCount);

            for (int i = 0; i < countPossibilities ; i++)
            {
                var strNumber = i.ToString($"d{m_digitsCount}");

                if (IsLegalNumber(strNumber))
                {
                    m_possibleGuesses.Add(strNumber);
                }
            }

            var index = new Random().Next(m_possibleGuesses.Count);
            m_targetNumber = m_possibleGuesses[index];

            Console.WriteLine(m_targetNumber);
        }

        /**
         *summary: Gets a guess as parameter and returns a new <see cref="BullsNCows"/> object containing the bulls and cows
         * of the guess compared to the secret number
         *
         * param name="guess"
         */
        public BullsNCows GetGuessEvaluation(string guess)
        {
            return new BullsNCows(m_targetNumber, guess);
        }

        /**
         *summary: Choose a random number(string) from the <see cref="m_possibleGuesses"/> list and returns it.
         */
        public string GetGuess()
        {
            var index = new Random().Next(m_possibleGuesses.Count);
            return m_possibleGuesses[index];
        }

        /**
         *summary: Gets an answer to a guess in the form of a <see cref="BullsNCows"/> object and the guess, removes from
         * the list of possible guesses part of the non-possible guesses according to the answer to the current guess. the chance that
         * a non-possible guess will be removed is determined by <see cref="m_strength"/>
         *
         * param name="guessResult"
         * param name="currentGuess"
         */
        public void EliminateRedundantGuesses(BullsNCows guessResult, string currentGuess)
        {
            m_possibleGuesses = m_possibleGuesses.Where(num => {
                var eliminationChance = new Random().Next(0, 100);
                
                var tempGuess = new BullsNCows(num, currentGuess);

                if (!tempGuess.Equals(guessResult) && eliminationChance > m_strength)
                {
                    return true;
                }

                return tempGuess.Equals(guessResult);

            }).ToList();

            Console.WriteLine($"Remaining numbers: {m_possibleGuesses.Count}");
        }
    }
}