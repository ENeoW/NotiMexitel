using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Content;

namespace NotiMexitel.Droid
{
   [Activity(Label = "NotiMexitel", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
   public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
   {      
      protected override void OnCreate(Bundle bundle)
      {
         TabLayoutResource = Resource.Layout.Tabbar;
         ToolbarResource = Resource.Layout.Toolbar;

         base.OnCreate(bundle);

         global::Xamarin.Forms.Forms.Init(this, bundle);
         LoadApplication(new App());
         var pendingIntent = PendingIntent.GetActivity(this, 0, new Intent(this, typeof(MainActivity)), 0);
         var intent = new Intent(this, typeof(NotiBackgroundReceiver));
         pendingIntent = PendingIntent.GetBroadcast(this, 0, intent, PendingIntentFlags.UpdateCurrent);
         var alarm = (AlarmManager)GetSystemService(Context.AlarmService);
         alarm.SetRepeating(AlarmType.RtcWakeup, 0, AlarmManager.IntervalHalfHour, pendingIntent);
      }

      //private bool IsAlarmSet()
      //{
      //   return PendingIntent.GetBroadcast(this, 0, intent, PendingIntentFlags.NoCreate) != null;
      //}
   }
}

