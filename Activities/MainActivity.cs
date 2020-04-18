using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using Android.Views;

namespace BullsNCowsProject.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Locked)]
    public class MainActivity : AppCompatActivity
    {
        private Button btnInstructions;
        private Button btnSettings;
        private Button btnGameStart;
        private Button btnExit;
        private GameManager gameManager;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            
            SetContentView(Resource.Layout.activity_main);

            gameManager = GameManager.getInstance(this);

            var settingsFile = GetSharedPreferences(Consts.settingsFileName, FileCreationMode.Private);

            btnInstructions = FindViewById<Button>(Resource.Id.btnInstructions);
            btnInstructions.Click += BtnInstructions_Click;

            btnSettings = FindViewById<Button>(Resource.Id.btnSettings);
            btnSettings.Click += BtnSettings_Click;

            btnGameStart = FindViewById<Button>(Resource.Id.btnGameStart);
            btnGameStart.Click += BtnGameStart_Click;

            btnExit = FindViewById<Button>(Resource.Id.btnExit);
            btnExit.Click += BtnExit_Click;

            Intent serviceIntent = new Intent(this, typeof(MusicService));
            serviceIntent.PutExtra("setPlayStatus", settingsFile.GetBoolean(Consts.musicMuteSettingsName, settingsFile.GetBoolean(Consts.musicMuteSettingsName, Consts.playMusicDefault)));
            StartService(serviceIntent);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_settings:
                    StartActivity(typeof(SettingsActivity));

                    return true;

                case Resource.Id.menu_scores:
                    StartActivity(typeof(ScoresActivity));

                    return true;

                case Resource.Id.menu_exit:
                    Finish();

                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        public override void OnBackPressed()
        {
            ExitGame();
        }

        private void BtnExit_Click(object sender, System.EventArgs e)
        {
            ExitGame();
        }

        private void BtnGameStart_Click(object sender, System.EventArgs e)
        {
            gameManager.StartGame();
        }

        private void BtnSettings_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(SettingsActivity));
        }

        private void BtnInstructions_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(InstructionsActivity));
        }

        private void ExitGame()
        {
            Intent serviceIntent = new Intent(this, typeof(MusicService));
            serviceIntent.PutExtra("setPlayStatus", false);
            StartService(serviceIntent);

            Finish();
        }
    }
}