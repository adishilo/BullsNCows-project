
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
    [Activity(Label = "Activity")]
    public class ScoresActivity : Activity
    {
        ListView lvGameScores;
        ScoreItemAdapter scoresAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_scores);

            lvGameScores = FindViewById<ListView>(Resource.Id.lvGameScores);
            scoresAdapter = new ScoreItemAdapter(this, GameManager.getInstance().GameScores);
            lvGameScores.Adapter = scoresAdapter;
        }

        public override void OnBackPressed()
        {
            Finish();
        }
    }
}
