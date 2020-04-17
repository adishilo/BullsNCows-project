using SQLite;

namespace BullsNCowsProject.Dal
{
    [Table("scores")]
    public class DbScore
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        [Column("player_number")]
        public string PlayerNumber { get; set; }

        [Column("computer_number")]
        public string ComputerNumber { get; set; }

        [Column("is_player_win")]
        public bool IsPlayerWin { get; set; }

        [Column("play_time_msec")]
        public double PlayTimeMsec { get; set; }
    }
}
