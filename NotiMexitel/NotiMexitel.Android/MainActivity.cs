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

         var panamaIntent = new Intent(this, typeof(NotiService));
         var pendingIntent = PendingIntent.GetService(this, 0, panamaIntent, PendingIntentFlags.UpdateCurrent);
                  
         var intent = new Intent(this, typeof(NotiBackgroundReceiver));
         var mexPendingIntent = PendingIntent.GetBroadcast(this, 0, intent, PendingIntentFlags.UpdateCurrent);

         var alarm = (AlarmManager)GetSystemService(Context.AlarmService);
         //alarm.SetRepeating(AlarmType.RtcWakeup, 0, AlarmManager.IntervalFifteenMinutes, pendingIntent);
         alarm.SetRepeating(AlarmType.RtcWakeup, 0, AlarmManager.IntervalFifteenMinutes, mexPendingIntent);
      }
   }
}

