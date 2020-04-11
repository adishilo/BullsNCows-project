using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BullsNCowsProject.Components;
using BullsNCowsProject.Dialogs;

namespace BullsNCowsProject.Activities
{
    [Activity(Label = "PlayerActivity")]
    public class PlayerActivity : Activity
    {
        private Button btnAsk;
        private EditText etGuessTypingPlace;
        HistoryItemAdapter historyItemAdapter;
        ListView lvGuessesHistory;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_player);

            var settingsFile = GetSharedPreferences(Consts.settingsFileName, FileCreationMode.Private);
            int numberOfDigits = settingsFile.GetInt(Consts.numberOfDigitsSettingsName, Consts.numberOfDigitsDefault);

            var choseNumberDialog = new NumberChooseDialog(this, numberOfDigits);

            choseNumberDialog.OnNumberChosen += ChoseNumberDialog_OnNumberChosen;
            choseNumberDialog.OnCancel += ChoseNumberDialog_OnCancel;

            btnAsk = FindViewById<Button>(Resource.Id.btnGetAnswer);
            btnAsk.Enabled = false;
            btnAsk.Click += BtnAsk_Click;

            etGuessTypingPlace = FindViewById<EditText>(Resource.Id.etGuessTypingPlace);
            etGuessTypingPlace.TextChanged += EtGuessTypingPlace_TextChanged;

            historyItemAdapter = new HistoryItemAdapter(this);
    
            lvGuessesHistory = FindViewById<ListView>(Resource.Id.lvGuessesHistory);
            
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
            var confirmedGuessEvaluation = GameManager.getInstance().GetGuessEvaluation(etGuessTypingPlace.Text);

            HistoryItem confirmedGuessOnList = new HistoryItem(confirmedGuess, confirmedGuessEvaluation.Bulls.ToString(), confirmedGuessEvaluation.Cows.ToString());

            historyItemAdapter.AddHistoryItem(confirmedGuessOnList);
            lvGuessesHistory.Adapter = historyItemAdapter;

            etGuessTypingPlace.Text = "";
        }

        public override void OnBackPressed()
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetTitle("Exit");
            builder.SetMessage("Are you sure you want to exit?");
            builder.SetCancelable(true);
            builder.SetPositiveButton("Exit", ExitAction);
            builder.SetNegativeButton("cancel", CancelAction);
            AlertDialog dialog = builder.Create();
            dialog.Show();
        }

        private void ExitAction(object sender, DialogClickEventArgs e)
        {
            GameManager.getInstance().CancelGame();

            Finish();
        }

        private void CancelAction(object sender, DialogClickEventArgs e)
        {
        }

        private void ChoseNumberDialog_OnCancel()
        {
            GameManager.getInstance().CancelGame();

            Finish();
        }

        private void ChoseNumberDialog_OnNumberChosen(string chosenNumber)
        {
            GameManager.getInstance().SetChosenNumber(chosenNumber);
        }
    }
}