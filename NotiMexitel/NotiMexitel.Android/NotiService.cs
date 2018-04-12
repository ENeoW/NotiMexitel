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

namespace NotiMexitel.Droid
{
   [Service]
   class NotiService : Service
   {
      public override IBinder OnBind(Intent intent)
      {
         return null;
      }

      [return: GeneratedEnum]
      public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
      {
         return StartCommandResult.NotSticky;
      }
   }
}