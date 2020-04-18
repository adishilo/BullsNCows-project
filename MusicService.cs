using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BullsNCowsProject
{
    [Service]
    class MusicService : Service
    {
        private MediaPlayer mediaPlayer;
        public override void OnCreate()
        {
            base.OnCreate();
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            bool setPlayStatus = intent.GetBooleanExtra("setPlayStatus", true);

            if (setPlayStatus)
            {
                StartMusic();
            }
            else
            {
                StopMusic();
            }

            return base.OnStartCommand(intent, flags, startId);
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }
        private void StartMusic()
        {
            if (mediaPlayer == null)
            {
                mediaPlayer = MediaPlayer.Create(this, Resource.Raw.alexander_nakarada_banjos_unite);
                mediaPlayer.Looping = true;
                mediaPlayer.Start();
            }
        }

        private void StopMusic()
        {
            if (mediaPlayer != null)
            {
                mediaPlayer.Stop();
                mediaPlayer.Release();
                mediaPlayer = null;
            }
        }
    }
}