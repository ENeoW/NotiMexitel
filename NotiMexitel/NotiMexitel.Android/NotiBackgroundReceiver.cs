using System;
using System.Drawing;
using Android.App;
using Android.Content;
using Android.Media;
using Autofac;

namespace NotiMexitel.Droid
{
   [BroadcastReceiver]
   class NotiBackgroundReceiver : BroadcastReceiver
   {
      //private static IContainer container { get; set; }
      private static int notificationId = 0;
      private static NotiRequestService notiRequest = new NotiRequestService();

      public override void OnReceive(Context context, Intent intent)
      {
         //using (var scope = container.BeginLifetimeScope())
         //{
         //var notiRequest = scope.Resolve<INotiRequest>();         
         try
         {
            var isChanged = notiRequest.GetMexitelNotification();
            if (isChanged)
               BuildNotifation(context, "Ha habido un cambio en la página de avisos de México.", isCritial: true);
            else
               BuildNotifation(context, $"La página de avisos de México no ha cambiado.");
         }
         catch (Exception e)
         {
            var message = e.Message + "\n";
            while (e.InnerException != null)
            {
               message += e.InnerException.Message + "\n";
               e = e.InnerException;
            }
            BuildNotifation(context, message, "Pagina de México");
         }
      }

      private void BuildNotifation(Context context, string contentText, string title = "Mexitel", bool isCritial = false)
      {
         var pendingIntent = PendingIntent.GetActivity(context, 0, new Intent(context, typeof(MainActivity)), 0);
         const string NOTIFICATION_CHANNEL_ID = "my_channel_id_01";
         var notificationManager = context.GetSystemService(Context.NotificationService) as NotificationManager;

         var notificationChannel = new NotificationChannel(NOTIFICATION_CHANNEL_ID, "My Notifications", isCritial ? NotificationImportance.Max : NotificationImportance.Default);
         // Configure the notification channel.
         //notificationChannel.setDescription("Channel description");
         notificationChannel.EnableLights(true);
         //notificationChannel.LightColor = Color.Red;
         notificationChannel.SetVibrationPattern(new long[] { 0, 1000, 500, 1000 });
         notificationChannel.EnableVibration(true);
         notificationChannel.SetSound(isCritial ?
            RingtoneManager.GetDefaultUri(RingtoneType.Alarm) : 
            RingtoneManager.GetDefaultUri(RingtoneType.Notification), 
            new AudioAttributes.Builder()
            .SetUsage(isCritial ? AudioUsageKind.Alarm : AudioUsageKind.Notification)
            .Build()
            );
         notificationManager.CreateNotificationChannel(notificationChannel);

         var notification = new Notification.Builder(context, NOTIFICATION_CHANNEL_ID  )
               .SetContentIntent(pendingIntent)
               .SetSmallIcon(Resource.Drawable.icon)
               .SetContentTitle(title)
               .SetStyle(new Notification.BigTextStyle().BigText(contentText))
               .SetContentText(contentText)
               .SetVisibility(NotificationVisibility.Public)
               .SetCategory(Notification.CategoryAlarm)
               .Build();

         // Publish the notification:         
         notificationManager.Notify(notificationId++, notification);
      }
   }
}