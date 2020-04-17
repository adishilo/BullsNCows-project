using System.Collections.Generic;
using SQLite;
using System.Linq;
using System.IO;
using System;

namespace BullsNCowsProject.Dal
{
    public static class ScoresDal
    {
        private static readonly SQLiteConnection db;

        static ScoresDal()
        {
            string applicationFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "scores");

            Directory.CreateDirectory(applicationFolderPath);

            string databaseFileName = Path.Combine(applicationFolderPath, "scores.db");
            db = new SQLiteConnection(databaseFileName);
            db.CreateTable<DbScore>();
        }

        public static void InsertNewScore(ScoreDto newScore)
        {
            DbScore dbScore = new DbScore();

            dbScore.PlayerNumber = newScore.PlayerNumber;
            dbScore.ComputerNumber = newScore.ComputerNumber;
            dbScore.IsPlayerWin = newScore.IsPlayerWin;
            dbScore.PlayTimeMsec = newScore.PlayTimeMsec;

            db.Insert(dbScore);
        }

        public static List<ScoreDto> GetScores()
        {
            return db.Table<DbScore>()
                .Select(score => new ScoreDto(
                    score.Id,
                    score.PlayerNumber,
                    score.ComputerNumber,
                    score.IsPlayerWin,
                    score.PlayTimeMsec))
                .ToList();
        }
    }
}