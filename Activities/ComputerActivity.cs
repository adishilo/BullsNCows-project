using System;
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
using BullsNCowsEngine.RealUnEngine;
using BullsNCowsProject.Components;

namespace BullsNCowsProject.Activities
{
    [Activity(Label = "ComputerActivity", ScreenOrientation = ScreenOrientation.Locked)]
    public class ComputerActivity : Activity
    {
        Animation animationFadeIn;
        TextView tvPlayersNumberDisplay;
        EditText etBulls;
        EditText etCows;
        Button btnSubmitAnswer;
        int numberOfDigits;
        string playersNumber;
        bool isCorrectCows = false;
        bool isCorrectBulls = false;
        string computersGuess;
        HistoryItemAdapter historyItemAdapter;
        ListView lvComputerGuessesHistory;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_computer);

            animationFadeIn = AnimationUtils.LoadAnimation(this, Resource.Animation.animation_fadeIn);

            tvPlayersNumberDisplay = FindViewById<TextView>(Resource.Id.tvPlayersNumberDisplay);
            lvComputerGuessesHistory = FindViewById<ListView>(Resource.Id.lvComputerGuessesHistory);

            playersNumber = Intent.GetStringExtra("playersNumber");
            tvPlayersNumberDisplay.Text = $"Player's number:{playersNumber}";
            computersGuess = Intent.GetStringExtra("computersGuess");
            HistoryItem computersGuessPartial = new HistoryItem(computersGuess, "?", "?", true);
            
            historyItemAdapter = new HistoryItemAdapter(this, GameManager.getInstance().ModelComputer.guessesHistory);
            historyItemAdapter.AddHistoryItem(computersGuessPartial);
            lvComputerGuessesHistory.Adapter = historyItemAdapter;
            lvComputerGuessesHistory.StartAnimation(animationFadeIn);

            etBulls = FindViewById<EditText>(Resource.Id.etBulls);
            etBulls.TextChanged += EtBulls_TextChanged;

            etCows = FindViewById<EditText>(Resource.Id.etCows);
            etCows.TextChanged += EtCows_TextChanged;

            btnSubmitAnswer = FindViewById<Button>(Resource.Id.btnSubmitAnswer);
            btnSubmitAnswer.Click += BtnSubmitAnswer_Click;
            btnSubmitAnswer.Enabled = false;

            var settingsFile = GetSharedPreferences(Consts.settingsFileName, FileCreationMode.Private);
            numberOfDigits = settingsFile.GetInt(Consts.numberOfDigitsSettingsName, Consts.numberOfDigitsDefault);
        }

        private void BtnSubmitAnswer_Click(object sender, EventArgs e)
        {
            if (int.Parse(etBulls.Text) == numberOfDigits)
            {
                GameManager.getInstance().EndGame(false);

                AlertDialog.Builder losingDialogBuilder = new AlertDialog.Builder(this);
                losingDialogBuilder.SetTitle("You lost :(");
                losingDialogBuilder.SetMessage("Computer managed to guess your number before you guessed its number");
                losingDialogBuilder.SetCancelable(false);
                losingDialogBuilder.SetPositiveButton("Main Menu", (object sender, DialogClickEventArgs e) =>
                {
                    Finish();
                });
                AlertDialog losingDialog = losingDialogBuilder.Create();
                losingDialog.Show();
            }
            else
            {
                HistoryItem computerGuessComplete = new HistoryItem(computersGuess, etBulls.Text, etCows.Text);
                historyItemAdapter.ReplaceWithCompleteHistoryItem(computerGuessComplete);
                lvComputerGuessesHistory.Adapter = historyItemAdapter;

                GameManager.getInstance().UpdateComputerWithPlayerAnswer(new BullsNCows(
                    int.Parse(etBulls.Text.ToString()),
                    int.Parse(etCows.Text.ToString())));

                GameManager.getInstance().NextTurn();

                Finish();
            }
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
    }
}