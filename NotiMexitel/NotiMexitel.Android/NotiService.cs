﻿using System;
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
   [Service]
   class NotiService : Service
   {
      private static IContainer container { get; set; }

      public override IBinder OnBind(Intent intent)
      {
         return null;
      }

      [return: GeneratedEnum]
      public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
      {
         using (var scope = container.BeginLifetimeScope())
         {
            var notiRequest = scope.Resolve<INotiRequest>();
            var isChanged = notiRequest.GetMexitelNotification().Result;
            if (isChanged)
            {
               
            }
         }  
         return StartCommandResult.NotSticky;
      }
   }
}