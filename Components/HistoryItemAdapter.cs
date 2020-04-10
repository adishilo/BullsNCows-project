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
using BullsNCows.Activities;

namespace BullsNCows.Components
{
    class HistoryItemAdapter : BaseAdapter<HistoryItem>
    {
        private Context context;
        private List<HistoryItem> historyItems;

        public HistoryItemAdapter(Context context)
        {
            this.context = context;
            historyItems = new List<HistoryItem>();
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
            LayoutInflater layoutInflater = ((PlayerActivity)context).LayoutInflater;
            View view = layoutInflater.Inflate(Resource.Layout.item_history, parent, false);

            TextView tvGuessedNumber = view.FindViewById<TextView>(Resource.Id.tvGuessedNumber);
            TextView tvBullsNumber = view.FindViewById<TextView>(Resource.Id.tvNumberOfBulls);
            TextView tvCowsNumber = view.FindViewById<TextView>(Resource.Id.tvNumberOfCows);

            HistoryItem renderedHistoryItem = historyItems[position];
            if (renderedHistoryItem != null)
            {
                tvGuessedNumber.Text = $"{position+1}. {renderedHistoryItem.GuessedNumber} ";
                tvBullsNumber.Text = $"B={renderedHistoryItem.BullsNumber}";
                tvCowsNumber.Text = $"C={renderedHistoryItem.CowsNumber}";
            }
            return view;
        }
       
        public void AddHistoryItem(HistoryItem item)
        {
            historyItems.Add(item);
        }
    }
}