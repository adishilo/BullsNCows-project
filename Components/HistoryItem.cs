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

namespace BullsNCowsProject.Components
{
    public class HistoryItem
    {
        public string GuessedNumber { get; }
        public string BullsNumber { get; }
        public string CowsNumber { get; }
        public bool IsEmphasis { get; }

        public HistoryItem(string guessedNumber, string bullsNumber, string cowsNumber, bool isEmphasis = false)
        {
            GuessedNumber = guessedNumber;
            BullsNumber = bullsNumber;
            CowsNumber = cowsNumber;
            IsEmphasis = isEmphasis;
        }
    }
}