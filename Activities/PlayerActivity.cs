﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using BullsNCowsProject.Components;
using BullsNCowsProject.Dialogs;

namespace BullsNCowsProject.Activities
{
    [Activity(Label = "PlayerActivity", ScreenOrientation = ScreenOrientation.Locked)]
    public class PlayerActivity : Activity
    {
        Animation animationFadeIn;
        private Button btnAsk;
        private EditText etGuessTypingPlace;
        int numberOfDigits;
        HistoryItemAdapter historyItemAdapter;
        ListView lvGuessesHistory;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_player);

            var settingsFile = GetSharedPreferences(Consts.settingsFileName, FileCreationMode.Private);
            numberOfDigits = settingsFile.GetInt(Consts.numberOfDigitsSettingsName, Consts.numberOfDigitsDefault);

            animationFadeIn = AnimationUtils.LoadAnimation(this, Resource.Animation.animation_fadeIn);

            if (GameManager.getInstance().ModelPlayer.isFirstTurn)
            {
                var choseNumberDialog = new NumberChooseDialog(this, numberOfDigits);

                choseNumberDialog.OnNumberChosen += ChoseNumberDialog_OnNumberChosen;
                choseNumberDialog.OnCancel += ChoseNumberDialog_OnCancel;
            }

            btnAsk = FindViewById<Button>(Resource.Id.btnGetAnswer);
            btnAsk.Enabled = false;
            btnAsk.Click += BtnAsk_Click;

            etGuessTypingPlace = FindViewById<EditText>(Resource.Id.etGuessTypingPlace);
            etGuessTypingPlace.TextChanged += EtGuessTypingPlace_TextChanged;

            historyItemAdapter = new HistoryItemAdapter(this, GameManager.getInstance().ModelPlayer.guessesHistory);

            lvGuessesHistory = FindViewById<ListView>(Resource.Id.lvGuessesHistory);
            lvGuessesHistory.Adapter = historyItemAdapter;
            lvGuessesHistory.StartAnimation(animationFadeIn);
        }

        
        private void EtGuessTypingPlace_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            bool isLegalInput = GameManager.getInstance().IsLegalNumber(e.Text.ToString());

            btnAsk.Enabled = isLegalInput;
            if (!isLegalInput)
            {
                etGuessTypingPlace.SetBackgroundResource(Resource.Drawable.error_edit);
            }
            else
            {
                etGuessTypingPlace.SetBackgroundResource(Resource.Drawable.legal_edit);
            }
        }

        private void BtnAsk_Click(object sender, EventArgs e)
        {
            string confirmedGuess = etGuessTypingPlace.Text;
            var confirmedGuessEvaluation = GameManager.getInstance().GetPlayerGuessEvaluation(etGuessTypingPlace.Text);

            if (confirmedGuessEvaluation.Bulls == numberOfDigits)
            {
                GameManager.getInstance().EndGame(true);

                AlertDialog.Builder winDialogBuilder = new AlertDialog.Builder(this);
                winDialogBuilder.SetTitle("You won!");
                winDialogBuilder.SetMessage("You guessed computer's number before it guessed yours");
                winDialogBuilder.SetCancelable(false);
                winDialogBuilder.SetPositiveButton("Main Menu", (object sender, DialogClickEventArgs e) =>
                {
                    Finish();
                });
                AlertDialog winDialog = winDialogBuilder.Create();
                winDialog.Show();
            }
            else
            {
                HistoryItem confirmedGuessOnList = new HistoryItem(confirmedGuess, confirmedGuessEvaluation.Bulls.ToString(), confirmedGuessEvaluation.Cows.ToString());

                historyItemAdapter.AddHistoryItem(confirmedGuessOnList);


                etGuessTypingPlace.Text = "";

                AlertDialog.Builder guessResultDialogbuilder = new AlertDialog.Builder(this);

                guessResultDialogbuilder.SetTitle("Score");
                guessResultDialogbuilder.SetMessage($"    {confirmedGuessEvaluation.Bulls}🎯\n    {confirmedGuessEvaluation.Cows}🐮");
                guessResultDialogbuilder.SetCancelable(false);
                guessResultDialogbuilder.SetPositiveButton("Continue", ContinueToComputerScreen);

                AlertDialog guessResultDialog = guessResultDialogbuilder.Create();

                guessResultDialog.Show();

                TextView tvAlertMessage = guessResultDialog.FindViewById<TextView>(Android.Resource.Id.Message);

                tvAlertMessage.SetTextSize(Android.Util.ComplexUnitType.Dip, 20);
            }
        }

        private void ContinueToComputerScreen(object sender, DialogClickEventArgs e)
        {
            GameManager.getInstance().ModelPlayer.isFirstTurn = false;
            GameManager.getInstance().NextTurn();

            Finish();
        }

        public override void OnBackPressed()
        {
            AlertDialog.Builder cancelGameDialogBuilder = new AlertDialog.Builder(this);
            cancelGameDialogBuilder.SetTitle("Exit");
            cancelGameDialogBuilder.SetMessage("Are you sure you want to exit?");
            cancelGameDialogBuilder.SetCancelable(true);
            cancelGameDialogBuilder.SetPositiveButton("Exit", (object sender, DialogClickEventArgs e) =>
                {
                    Finish();
                });
            cancelGameDialogBuilder.SetNegativeButton("cancel", (object sender, DialogClickEventArgs e) => { });
            AlertDialog cancelDialog = cancelGameDialogBuilder.Create();
            cancelDialog.Show();
        }

        private void ChoseNumberDialog_OnCancel()
        {
            Finish();
        }

        private void ChoseNumberDialog_OnNumberChosen(string chosenNumber)
        {
            GameManager.getInstance().SetPlayerNumber(chosenNumber);
        }
    }
}