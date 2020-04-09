using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace BullsNCows.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button btnInstructions;
        private Button btnSettings;
        private Button btnGameStart;
        private GameManager gameManager;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            
            SetContentView(Resource.Layout.activity_main);

            gameManager = GameManager.getInstance(this);

            btnInstructions = FindViewById<Button>(Resource.Id.btnInstructions);
            btnInstructions.Click += BtnInstructions_Click;

            btnSettings = FindViewById<Button>(Resource.Id.btnSettings);
            btnSettings.Click += BtnSettings_Click;

            btnGameStart = FindViewById<Button>(Resource.Id.btnGameStart);
            btnGameStart.Click += BtnGameStart_Click;
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

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}