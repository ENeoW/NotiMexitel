using System;
using System.Net.Http;
using Xamarin.Forms;

namespace NotiMexitel
{
   public class NotiRequestService //: INotiRequest
   {
      const string mexico = "https://embamex.sre.gob.mx/cuba/index.php/avisos/";
      const string avisos = "https://embamex.sre.gob.mx/cuba/index.php/avisos/182-aviso-importante";

      private static string oldPageMexico = string.Empty;
      private static string oldPagePanama = string.Empty;

      public bool GetMexitelNotification()
      {
         return GetNotification();
      }

      public bool GetPanamaNotification()
      {
         return false; // GetNotification(Panama, nameof(Panama), ref oldPagePanama);
      }


      private bool GetNotification()
      {
         try
         {
            using (var httpClient = new HttpClient())
            {
               var page = httpClient.GetStringAsync(mexico).Result;
               for (int i = 183; i < 199; i++)
               {
                  if (page.Contains($"/cuba/index.php/avisos/{i}"))
                  {
                     return true;
                  }
               }
               page = httpClient.GetStringAsync(avisos).Result;
               if (!page.Contains("/cuba/images/stories/AvisoTurnos.png"))
               {
                  return true;
               }
            }
            return false;
         }
         catch (Exception e)
         {
            throw new Exception($"Something wrong happens, {e.Message}");
         }
         
      }
   }
}
