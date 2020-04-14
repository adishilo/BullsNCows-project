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
         * summary fioejwfio
         * 
         * param name="guess" ifje woifdj
         * param name="target" iofjreowij
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

        public bool Equals(BullsNCows other)
        {
            return Bulls == other.Bulls && Cows == other.Cows;
        }
    }
}