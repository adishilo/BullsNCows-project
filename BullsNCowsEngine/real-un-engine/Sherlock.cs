using System;
using System.Collections.Generic;
using System.Linq;

namespace BullsNCowsEngine.RealUnEngine
{
    public class Sherlock
    {
        private readonly string m_targetNumber;
        private readonly int m_digitsCount;

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

        public Sherlock(int digitsCount)
        {
            m_digitsCount = digitsCount;
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

        public bool GetGuess(Func<string, BullsNCows> answerGuess)
        {
            var index = new Random().Next(m_possibleGuesses.Count);
            string currentGuess = m_possibleGuesses[index];
            var guessResult = answerGuess(currentGuess);

            if (guessResult.Bulls == m_digitsCount)
            {
                return true;
            }

            EliminateRedundantGuesses(guessResult, currentGuess);
            
            return false;
        }

        private void EliminateRedundantGuesses(BullsNCows guessResult, string currentGuess)
        {
            m_possibleGuesses = m_possibleGuesses.Where(num => {
                var tempGuess = new BullsNCows(num, currentGuess);

                return tempGuess.Equals(guessResult);
            }).ToList();
        }
    }
}