using System.Collections.Generic;

using Android.App;
using Android.Graphics;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;

namespace BullsNCowsProject.Components
{
    class HistoryItemAdapter : BaseAdapter<HistoryItem>
    {
        private Activity activity;
        private List<HistoryItem> historyItems;

        public HistoryItemAdapter(Activity activity, List<HistoryItem> historyItems)
        {
            this.activity = activity;
            this.historyItems = historyItems;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count
        {
            get { return historyItems.Count; }
        }

        public override HistoryItem this[int position]
        {
            get { return historyItems[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater layoutInflater = activity.LayoutInflater;
            View view = layoutInflater.Inflate(Resource.Layout.item_history, parent, false);

            TextView tvGuessedNumber = view.FindViewById<TextView>(Resource.Id.tvGuessedNumber);
            TextView tvBullsNumber = view.FindViewById<TextView>(Resource.Id.tvNumberOfBulls);
            TextView tvCowsNumber = view.FindViewById<TextView>(Resource.Id.tvNumberOfCows);

            HistoryItem renderedHistoryItem = historyItems[position];
            if (renderedHistoryItem != null)
            {
                tvGuessedNumber.Text = $"{Count-position}. {renderedHistoryItem.GuessedNumber}";
                tvBullsNumber.Text = $"B={renderedHistoryItem.BullsNumber}";
                tvCowsNumber.Text = $"C={renderedHistoryItem.CowsNumber}";

                if (renderedHistoryItem.IsEmphasis)
                {
                    view.SetBackgroundColor(new Color(ContextCompat.GetColor(activity, Resource.Color.emphasis)));
                    tvGuessedNumber.SetTypeface(null, TypefaceStyle.Bold);
                    tvBullsNumber.SetTypeface(null, TypefaceStyle.Bold);
                    tvCowsNumber.SetTypeface(null, TypefaceStyle.Bold);
                }
            }
            return view;
        }
       
        /**
         * summary: Gets a <see cref="HistoryItem"/> object and insert it to the first place in the list <see cref="historyItems"/>
         * 
         * param name="item"
         */
        public void AddHistoryItem(HistoryItem item)
        {
            historyItems.Insert(0, item);
        }

        /**
         * summary: Replace the <see cref="HistoryItem"/> in the first spot of the list with another <see cref="HistoryItem"/> it gets 
         * as a parameter
         * 
         * param name="item"
         */ 
        public void ReplaceWithCompleteHistoryItem(HistoryItem item)
        {
            historyItems[0] = item;
        }
    }
}