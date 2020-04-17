using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using BullsNCowsProject.Dal;

namespace BullsNCowsProject.Components
{
    public class ScoreItemAdapter : BaseAdapter<ScoreDto>
    {
        private readonly Activity activity;
        private readonly List<ScoreDto> scores;

        public ScoreItemAdapter(Activity activity, List<ScoreDto> scores)
        {
            this.activity = activity;
            this.scores = scores;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count => scores.Count;

        public override ScoreDto this[int position] => scores[position];

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater layoutInflater = activity.LayoutInflater;
            View view = layoutInflater.Inflate(Resource.Layout.item_score, parent, false);

            TextView tvGameNumber = view.FindViewById<TextView>(Resource.Id.tvGameNumber);
            TextView tvPlayerNumber = view.FindViewById<TextView>(Resource.Id.tvPlayerNumber);
            TextView tvComputerNumber = view.FindViewById<TextView>(Resource.Id.tvComputerNumber);
            TextView tvGameTime = view.FindViewById<TextView>(Resource.Id.tvGameTime);
            TextView tvWhoWon = view.FindViewById<TextView>(Resource.Id.tvWhoWon);

            ScoreDto score = scores[position];

            tvGameNumber.Text = $"{score.Id}.";
            tvPlayerNumber.Text = $"👩‍🏭{score.PlayerNumber}";
            tvComputerNumber.Text = $"🖥{score.ComputerNumber}";
            tvGameTime.Text = $"⏳{TimeSpan.FromSeconds(Math.Floor(score.PlayTimeMsec / 1000))}";
            string whoWon = score.IsPlayerWin ? "Player" : "Computer";
            tvWhoWon.Text = $"🏆{whoWon}";

            return view;
        }
    }
}
