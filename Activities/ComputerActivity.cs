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

namespace BullsNCowsProject.Activities
{
    [Activity(Label = "ComputerActivity")]
    public class ComputerActivity : Activity
    {
        TextView tvPlayersNumberDisplay;
        EditText etBulls;
        EditText etCows;
        Button btnSubmitAnswer;
        string playersNumber;
        bool isCorrectCows;
        bool isCorrectBulls;
        string computersGuess;
        int numberOfDigits;
        HistoryItemAdapter historyItemAdapter;
        ListView lvComputerGuessesHistory;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_computer);

            tvPlayersNumberDisplay = FindViewById<TextView>(Resource.Id.tvPlayersNumberDisplay);
            lvComputerGuessesHistory = FindViewById<ListView>(Resource.Id.lvComputerGuessesHistory);

            playersNumber = Intent.GetStringExtra("playersNumber");
            tvPlayersNumberDisplay.Text = $"Player's number:{playersNumber}";

            etBulls = FindViewById<EditText>(Resource.Id.etBulls);
            etBulls.TextChanged += EtBulls_TextChanged;

            etCows = FindViewById<EditText>(Resource.Id.etCows);
            etCows.TextChanged += EtCows_TextChanged;

            btnSubmitAnswer = FindViewById<Button>(Resource.Id.btnSubmitAnswer);
            btnSubmitAnswer.Click += BtnSubmitAnswer_Click;

            historyItemAdapter = new HistoryItemAdapter(this);

            var settingsFile = GetSharedPreferences(Consts.settingsFileName, FileCreationMode.Private);
            numberOfDigits = settingsFile.GetInt(Consts.numberOfDigitsSettingsName, Consts.numberOfDigitsDefault);

        }

        protected override void OnResume()
        {
            base.OnResume();

            isCorrectCows = false;
            isCorrectBulls = false;
            btnSubmitAnswer.Enabled = false;

            computersGuess = Intent.GetStringExtra("computersGuess");
            HistoryItem computersGuessPartial = new HistoryItem(computersGuess, "?", "?");

            historyItemAdapter.AddHistoryItem(computersGuessPartial);
            lvComputerGuessesHistory.Adapter = historyItemAdapter;
        }

        private void BtnSubmitAnswer_Click(object sender, EventArgs e)
        {
            //if (int.Parse(etBulls.Text) == numberOfDigits)
            //{ 
                
            //}
            HistoryItem computerGuessComplete = new HistoryItem(computersGuess, etBulls.Text, etCows.Text);
            historyItemAdapter.ReplaceWithCompleteHistoryItem(computerGuessComplete);
            lvComputerGuessesHistory.Adapter = historyItemAdapter;

            GameManager.getInstance().NextTurn();
        }

        private void EtCows_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (e.Text == null || e.Text.ToString() == "")
            {
                btnSubmitAnswer.Enabled = false;
                return;
            }

            isCorrectCows = (GameManager.getInstance().GetComputerGuessEvaluation(playersNumber, computersGuess).Cows == int.Parse(e.Text.ToString()));

            if (!isCorrectCows)
            {
                etCows.SetBackgroundResource(Resource.Drawable.error_edit);
                btnSubmitAnswer.Enabled = false;
            }
            else
            {
                etCows.SetBackgroundResource(Resource.Drawable.legal_edit);
                if (isCorrectBulls)
                {
                    btnSubmitAnswer.Enabled = true;
                }
            }
        }

        private void EtBulls_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {

            if (e.Text == null || e.Text.ToString() == "")
            {
                btnSubmitAnswer.Enabled = false;
                return;
            }

            isCorrectBulls = (GameManager.getInstance().GetComputerGuessEvaluation(playersNumber, computersGuess).Bulls == int.Parse(e.Text.ToString()));

            if (!isCorrectBulls)
            {
                etBulls.SetBackgroundResource(Resource.Drawable.error_edit);
                btnSubmitAnswer.Enabled = false;
            }
            else
            {
                etBulls.SetBackgroundResource(Resource.Drawable.legal_edit);
                if (isCorrectCows)
                {
                    btnSubmitAnswer.Enabled = true;
                }
            }
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
    }
}