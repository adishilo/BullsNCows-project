using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BullsNCowsProject
{
    class MusicService : Service
    {
        public override void OnCreate()
        {
            base.OnCreate();
        }
        public override StartCommandResult OnStartCommand(StartCommandFlags flags, int startId)
        {
            ThreadStart threadStart = new ThreadStart(Run);
            Thread thread = new Thread(threadStart);
            thread.Start();
            return base.OnStartCommand(flags, startId);
        }
        public override Android.OS.IBinder OnBind(Android.Content.Intent intent)
        {
            return null;
        }
        public void Run()

        {             
            StopSelf();
        }
    }
}