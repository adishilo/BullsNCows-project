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
using Android.Widget;

namespace BullsNCowsProject.Activities
{
    [Activity(Label = "InstructionsActivity", ScreenOrientation = ScreenOrientation.Locked)]
    public class InstructionsActivity : Activity
    {
        Button btnMainMenu;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            
            SetContentView(Resource.Layout.activity_instructions);

            btnMainMenu = FindViewById<Button>(Resource.Id.btnMainMenu);
            btnMainMenu.Click += BtnMainMenu_Click;
        }

        private void BtnMainMenu_Click(object sender, EventArgs e)
        {
            Finish();
        }
    }
}