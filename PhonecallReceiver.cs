using System;
using Android.App;
using Android.Content;
using Android.Telephony;

namespace BullsNCowsProject
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] { TelephonyManager.ActionPhoneStateChanged })]
    public class PhonecallReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action == TelephonyManager.ActionPhoneStateChanged)
            {
                var state = intent.GetStringExtra(TelephonyManager.ExtraState);
                if (state == TelephonyManager.ExtraStateRinging)
                {
                    SetMusicState(context, false);
                }
                else
                {
                    if (state == TelephonyManager.ExtraStateIdle)
                    {
                        SetMusicState(context, true);
                    }
                }
            }
        }

        private void SetMusicState(Context context, bool activate)
        {
            Intent serviceIntent = new Intent(context, typeof(MusicService));
            serviceIntent.PutExtra("setPlayStatus", activate);
            context.StartService(serviceIntent);
        }
    }
}
