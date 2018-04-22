using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Autofac;

namespace NotiMexitel.Droid
{
   [Service]
   class NotiService : Service
   {
      private static int notificationId = 100000;
      private static NotiRequestService notiRequest = new NotiRequestService();

      public override IBinder OnBind(Intent intent)
      {
         return null;
      }

      [return: GeneratedEnum]
      public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
      {
         try
         {
            var isChanged = notiRequest.GetPanamaNotification();
            if (isChanged)
               BuildNotifation(this, "Ha habido un cambio en la página de avisos de Panama.", isCritial: true);
            else
               BuildNotifation(this, $"La página de avisos de Panama no ha cambiado.");
         }
         catch (Exception e)
         {
            var message = e.Message + "\n";
            while (e.InnerException != null)
            {
               message += e.InnerException.Message + "\n";
               e = e.InnerException;
            }
            BuildNotifation(this, message, "Pagina de Panama");
         }
         return StartCommandResult.Sticky;
      }

      private void BuildNotifation(Context context, string contentText, string title = "Panama", bool isCritial = false)
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