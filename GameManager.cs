﻿using Android.Content;
using BullsNCowsProject.Activities;
using BullsNCowsProject.Dialogs;
using BullsNCowsEngine.RealUnEngine;
using BullsNCowsProject.Models;
using System.Collections.Generic;
using BullsNCowsProject.Dal;
using System.Diagnostics;
using System;

namespace BullsNCowsProject
{
    public class GameManager
    {
        private Sherlock currentGameEngine; 
        private static GameManager m_this = null;
        private Context gameContext;
        private bool isPlayerTurn;
        private DateTime gameStartTimeStamp;

        // It is highly irregular to use a getInstance() function of a singleton with a parameter for initialization, because you never know who calls this function
        // first, and if they give the initialization parameter or not.
        // In our case, however, we assume that the first call to getInstance is OnCreate of the MainActivity, so it is "safe". We do not give this parameter on any other
        // invocation of getInstance.
        public static GameManager getInstance(Context gameContext = null)
        {
            if (m_this == null)
            {
                m_this = new GameManager();
            }

            if (gameContext != null)
            {
                m_this.gameContext = gameContext;
            }

            return m_this;
        }

        private GameManager()
        {
        }

        // Set is private because GameManager needs it in order to create the models, get is not - because the activities need access to the models
        public PlayerModel ModelPlayer { get; private set; }
        public ComputerModel ModelComputer { get; private set; }
        public List<ScoreDto> GameScores => ScoresDal.GetScores();
        
        /**
         * summary: Gets everything needed to start the game prepared. Takes the needed values from the SP file, creates the objects needed 
         * to run and display the game, then create <see cref="PlayerActivity"/>
         */
        public void StartGame()
        {
            var settingsFile = gameContext.GetSharedPreferences(Consts.settingsFileName, FileCreationMode.Private);
            int digitsCount = settingsFile.GetInt(Consts.numberOfDigitsSettingsName, Consts.numberOfDigitsDefault);
            int strength = settingsFile.GetInt(Consts.engineStrengthSettingsName, Consts.engineStrengthDefault);

            gameStartTimeStamp = DateTime.Now;

            currentGameEngine = new Sherlock(digitsCount, strength);

            ModelPlayer = new PlayerModel();
            ModelComputer = new ComputerModel();
            
            gameContext.StartActivity(typeof(PlayerActivity));

            isPlayerTurn = true;
        }

        public void EndGame(bool isPlayerWin)
        {
            var score = new ScoreDto(
                0,
                ModelPlayer.playersChosenNumber,
                currentGameEngine.SecretNumber,
                isPlayerWin,
                Math.Floor((double) (DateTime.Now.Ticks - gameStartTimeStamp.Ticks) / 10000));

            ScoresDal.InsertNewScore(score);
        }

        public void SetPlayerNumber(string dialogInput)
        {
            ModelPlayer.playersChosenNumber = dialogInput;
        }

        public bool IsLegalNumber(string guess)
        {
            return currentGameEngine.IsLegalNumber(guess);
        }
        
        public BullsNCows GetPlayerGuessEvaluation(string guess)
        {
            return currentGameEngine.GetGuessEvaluation(guess);
        }

        public BullsNCows GetComputerGuessEvaluation(string playerChosenNumber, string computerGuess)
        {
            return new BullsNCows(playerChosenNumber, computerGuess);
        }

        public void NextTurn()
        {
            isPlayerTurn = !isPlayerTurn;

            if (isPlayerTurn)
            {
                gameContext.StartActivity(typeof(PlayerActivity));
            }
            else
            {
                ModelComputer.CurrentGuess = currentGameEngine.GetGuess();
                Intent infoForComputerScreen = new Intent(gameContext, typeof(ComputerActivity));
                infoForComputerScreen.PutExtra("playersNumber", ModelPlayer.playersChosenNumber);
                infoForComputerScreen.PutExtra("computersGuess", ModelComputer.CurrentGuess);
                gameContext.StartActivity(infoForComputerScreen);
            }
        }

        public void UpdateComputerWithPlayerAnswer(BullsNCows playerAnswer)
        {
            currentGameEngine.EliminateRedundantGuesses(playerAnswer, ModelComputer.CurrentGuess);
        }
    }
}