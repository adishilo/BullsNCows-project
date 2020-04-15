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

namespace BullsNCowsProject.Models
{
    public class ComputerModel
    {
        public List<HistoryItem> guessesHistory { get; set; } = new List<HistoryItem>();

        public string CurrentGuess { get; set; }
    }
}