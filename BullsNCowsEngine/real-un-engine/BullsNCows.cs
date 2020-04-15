namespace BullsNCowsEngine.RealUnEngine
{
    public class BullsNCows
    {
        public int Bulls { get; }
        public int Cows { get; }

        public BullsNCows(int bulls, int cows)
        {
            Bulls = bulls;
            Cows = cows;
        }

        /**
         * summary: The function gets two int numbers as parameters, and calculate how many digits are in both numbers ("Cows")
         * and how many digits are in both numbers and are in the same spot ("Bulls"). It then creates an object of the class <see cref="BullsNCows"/>
         * with the values of "Bulls" and "Cows" it calculated beforehand.
         * 
         * param name="guess": The number guessed by the computer/player
         * param name="target": The number we are comparing the guess to.
         * could be the actual secret number or a possible guess.
         * 
         * Assumption:
         * <see cref="target"/> and <see cref="guess"/> are digits only string with no duplicates.
         */
        public BullsNCows(string target, string guess)
        {
            Bulls = 0;
            Cows = 0;

            for (int targetIdx = 0; targetIdx < target.Length; targetIdx++)
            {
                for (int guessIdx = 0; guessIdx < guess.Length; guessIdx++)
                {
                    if (target[targetIdx] == guess[guessIdx])
                    {
                        if (targetIdx == guessIdx)
                        {
                            Bulls++;
                        }
                        else
                        {
                            Cows++;
                        }
                    }
                }
            }
        }

        public override string ToString()
        {
            return $"[Bulls: {Bulls}, Cows: {Cows}]";
        }

        /**
         * summary: The function compares a given <see cref="BullsNCows"/> object to this one. 
         */
        public bool Equals(BullsNCows other)
        {
            return Bulls == other.Bulls && Cows == other.Cows;
        }
    }
}