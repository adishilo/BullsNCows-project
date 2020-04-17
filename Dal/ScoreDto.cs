namespace BullsNCowsProject.Dal
{
    public class ScoreDto
    {
        public int Id { get; }
        public string PlayerNumber { get; }
        public string ComputerNumber { get; }
        public bool IsPlayerWin { get; }
        public double PlayTimeMsec { get; }

        public ScoreDto(
            int id,
            string playerNumber,
            string computerNumber,
            bool isPlayerWin,
            double playTimeMsec)
        {
            Id = id;
            PlayerNumber = playerNumber;
            ComputerNumber = computerNumber;
            IsPlayerWin = isPlayerWin;
            PlayTimeMsec = playTimeMsec;
        }
    }
}
