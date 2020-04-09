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

namespace BullsNCows.Activities
{
    [Activity(Label = "SettingsActivity")]
    public class SettingsActivity : Activity
    {
        private RadioButton rbDigits3;
        private RadioButton rbDigits4;
        private RadioButton rbDigits5;
        private RadioGroup rgDigits;
        private ISharedPreferences settingsFile;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_settings);

            settingsFile = GetSharedPreferences(Consts.settingsFileName, FileCreationMode.Private);

            rbDigits3 = FindViewById<RadioButton>(Resource.Id.rb3);
            rbDigits3.Click += RbDigits3_Click;

            rbDigits4 = FindViewById<RadioButton>(Resource.Id.rb4);
            rbDigits4.Click += RbDigits4_Click;

            rbDigits5 = FindViewById<RadioButton>(Resource.Id.rb5);
            rbDigits5.Click += RbDigits5_Click;

            rgDigits = FindViewById<RadioGroup>(Resource.Id.rgDigits);

            ShowNumOfDigitsSelection();
        }

        private void ShowNumOfDigitsSelection()
        {
            int numberOfDigits = settingsFile.GetInt(Consts.numberOfDigitsSettingsName, Consts.numberOfDigitsDefault);

            switch (numberOfDigits)
            {
                case 3:
                    rgDigits.Check(Resource.Id.rb3);
                    break;

                case 4:
                    rgDigits.Check(Resource.Id.rb4);
                    break;

                case 5:
                    rgDigits.Check(Resource.Id.rb5);
                    break;

                default:
                    rgDigits.Check(Resource.Id.rb4);
                    break;
            }
        }

        private void RbDigits5_Click(object sender, EventArgs e)
        {
            var editor = settingsFile.Edit();

            editor.PutInt(Consts.numberOfDigitsSettingsName, 5);
            editor.Commit();
        }

        private void RbDigits4_Click(object sender, EventArgs e)
        {
            var editor = settingsFile.Edit();

            editor.PutInt(Consts.numberOfDigitsSettingsName, 4);
            editor.Commit();
        }

        private void RbDigits3_Click(object sender, EventArgs e)
        {
            var editor = settingsFile.Edit();

            editor.PutInt(Consts.numberOfDigitsSettingsName, 3);
            editor.Commit();
        }
    }
}