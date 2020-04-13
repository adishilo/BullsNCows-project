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

        public BullsNCows GetGuessEvaluation(string guess)
        {
            return new BullsNCows(m_targetNumber, guess);
        }

        public string GetGuess()
        {
            var index = new Random().Next(m_possibleGuesses.Count);
            return m_possibleGuesses[index];
        }

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