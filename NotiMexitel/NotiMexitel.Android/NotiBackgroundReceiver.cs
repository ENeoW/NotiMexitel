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
using Autofac;

namespace NotiMexitel.Droid
{
   [BroadcastReceiver]
   class NotiBackgroundReceiver : BroadcastReceiver
   {
      private static IContainer container { get; set; }

      public override void OnReceive(Context context, Intent intent)
      {
         using (var scope = container.BeginLifetimeScope())
         {
            var notiRequest = scope.Resolve<INotiRequest>();
            var isChanged = notiRequest.GetMexitelNotification().Result;
            if (isChanged)
            {

            }
         }
         var am = (AlarmManager)context.GetSystemService(Context.AlarmService);
         am.w
      }
   }
}