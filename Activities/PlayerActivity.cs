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
using BullsNCows.Dialogs;

namespace BullsNCows.Activities
{
    [Activity(Label = "PlayerActivity")]
    public class PlayerActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_player);

            var settingsFile = GetSharedPreferences(Consts.settingsFileName, FileCreationMode.Private);
            int numberOfDigits = settingsFile.GetInt(Consts.numberOfDigitsSettingsName, Consts.numberOfDigitsDefault);

            var choseNumberDialog = new NumberChooseDialog(this, numberOfDigits);

            choseNumberDialog.OnNumberChosen += ChoseNumberDialog_OnNumberChosen;
            choseNumberDialog.OnCancel += ChoseNumberDialog_OnCancel;
        }

        private void ChoseNumberDialog_OnCancel()
        {
            GameManager.getInstance().CancelGame();

            Finish();
        }

        private void ChoseNumberDialog_OnNumberChosen(string chosenNumber)
        {
            
        }
    }
}