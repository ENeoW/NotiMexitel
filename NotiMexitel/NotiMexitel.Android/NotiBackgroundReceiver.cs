using System;
using Android.App;
using Android.Content;
using Android.Media;
using Autofac;

namespace NotiMexitel.Droid
{
   [BroadcastReceiver]
   class NotiBackgroundReceiver : BroadcastReceiver
   {
      private static IContainer container { get; set; }
      private static int notificationId = 0;
      private static NotiRequestService notiRequest = new NotiRequestService();

      public override void OnReceive(Context context, Intent intent)
      {
         //using (var scope = container.BeginLifetimeScope())
         //{
         //var notiRequest = scope.Resolve<INotiRequest>();         
         try
         {
            var am = (AlarmManager)context.GetSystemService(Context.AlarmService);
            var isChanged = notiRequest.GetMexitelNotification();
            if (isChanged)
               BuildNotifation(context, "Ha habido un cambio en la página de avisos.", isCritial: true);
            else
               BuildNotifation(context, $"La página de avisos no ha cambiado.");
         }
         catch (Exception e)
         {
            var message = e.Message + "\n";
            while (e.InnerException != null)
            {
               message += e.InnerException.Message + "\n";
               e = e.InnerException;
            }
            BuildNotifation(context, message);
         }
      }

      private void BuildNotifation(Context context, string contentText, string title = "Mexitel", bool isCritial = false)
      {
         var pendingIntent = PendingIntent.GetActivity(context, 0, new Intent(context, typeof(MainActivity)), 0);
         var notification = new Notification.Builder(context)
               .SetContentIntent(pendingIntent)
               .SetSmallIcon(Resource.Drawable.icon)
               .SetContentTitle(title)
               .SetStyle(new Notification.BigTextStyle().BigText(contentText))
               .SetContentText(contentText)
               .SetPriority(isCritial ? (int)NotificationPriority.Max : (int)NotificationPriority.Default)
               .SetVisibility(NotificationVisibility.Public)
               .SetCategory(Notification.CategoryAlarm)
               .SetDefaults(NotificationDefaults.Vibrate)
               .SetSound(isCritial ? RingtoneManager.GetDefaultUri(RingtoneType.Alarm) : RingtoneManager.GetDefaultUri(RingtoneType.Notification))
               .Build();
         var notificationManager = context.GetSystemService(Context.NotificationService) as NotificationManager;

         // Publish the notification:         
         notificationManager.Notify(notificationId++, notification);
      }
   }
}