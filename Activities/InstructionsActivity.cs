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
    [Activity(Label = "InstructionsActivity")]
    public class InstructionsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            
            SetContentView(Resource.Layout.activity_instructions);
        }
    }
}