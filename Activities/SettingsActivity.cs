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

namespace BullsNCowsProject.Activities
{
    [Activity(Label = "SettingsActivity")]
    public class SettingsActivity : Activity
    {
        private Switch sPlay;
        private Button btnMainMenu;
        private TextView tvProgressDisplay;
        private RadioButton rbDigits3;
        private RadioButton rbDigits4;
        private RadioButton rbDigits5;
        private RadioGroup rgDigits;
        private SeekBar sbDifficulty;
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

            sPlay = FindViewById<Switch>(Resource.Id.sPlay);
            sPlay.Checked = settingsFile.GetBoolean(Consts.musicMuteSettingsName, Consts.playMusicDefault);
            sPlay.CheckedChange += SPlay_CheckedChange;

            btnMainMenu = FindViewById<Button>(Resource.Id.btnMainMenu);
            btnMainMenu.Click += BtnMainMenu_Click;

            sbDifficulty = FindViewById<SeekBar>(Resource.Id.sbDifficulty);
            sbDifficulty.Min = 50;
            sbDifficulty.Progress = settingsFile.GetInt(Consts.engineStrengthSettingsName, 100);
            sbDifficulty.ProgressChanged += SbDifficulty_ProgressChanged;

            tvProgressDisplay = FindViewById<TextView>(Resource.Id.tvProgressDisplay);
            tvProgressDisplay.Text = $"{sbDifficulty.Progress}%";

            ShowNumOfDigitsSelection();
        }

        private void SPlay_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            var editor = settingsFile.Edit();
            editor.PutBoolean(Consts.musicMuteSettingsName, e.IsChecked);
            editor.Commit();

            Intent serviceIntent = new Intent(this, typeof(MusicService));
            serviceIntent.PutExtra("setPlayStatus", e.IsChecked);
            StartService(serviceIntent);
        }

        private void SbDifficulty_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            tvProgressDisplay.Text = $"{sbDifficulty.Progress}%";

            var editor = settingsFile.Edit();
            editor.PutInt(Consts.engineStrengthSettingsName, sbDifficulty.Progress);
            editor.Commit();
        }

        private void BtnMainMenu_Click(object sender, EventArgs e)
        {
            Finish();
        }

        /**
         * summary: takes the number of digits value written in <see cref="settingsFile"/> and checks the right radio button in the settings screen
         */
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