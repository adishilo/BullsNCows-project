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
using BullsNCows.Components;
using BullsNCows.Dialogs;

namespace BullsNCows.Activities
{
    [Activity(Label = "PlayerActivity")]
    public class PlayerActivity : Activity
    {

        HistoryItemAdapter historyItemAdapter;
        ListView lvGuessesHistory;
        HistoryItem historyItem1 = new HistoryItem("534", "45", "43");
        HistoryItem historyItem2 = new HistoryItem("43453", "7", "34");
        HistoryItem historyItem3 = new HistoryItem("6798", "21", "5");

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_player);

            var settingsFile = GetSharedPreferences(Consts.settingsFileName, FileCreationMode.Private);
            int numberOfDigits = settingsFile.GetInt(Consts.numberOfDigitsSettingsName, Consts.numberOfDigitsDefault);

            var choseNumberDialog = new NumberChooseDialog(this, numberOfDigits);

            choseNumberDialog.OnNumberChosen += ChoseNumberDialog_OnNumberChosen;
            choseNumberDialog.OnCancel += ChoseNumberDialog_OnCancel;

            historyItemAdapter = new HistoryItemAdapter(this);
            historyItemAdapter.AddHistoryItem(historyItem1);
            historyItemAdapter.AddHistoryItem(historyItem2);
            historyItemAdapter.AddHistoryItem(historyItem3);

            lvGuessesHistory = FindViewById<ListView>(Resource.Id.lvGuessesHistory);
            lvGuessesHistory.Adapter = historyItemAdapter;
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