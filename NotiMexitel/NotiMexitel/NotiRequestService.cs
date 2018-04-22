using System;
using System.Net.Http;
using Xamarin.Forms;

namespace NotiMexitel
{
   public class NotiRequestService //: INotiRequest
   {
      const string Mexico = "https://embamex.sre.gob.mx/cuba/index.php/avisos/159-info-de-sc-citas-mexitel";
      const string Panama = "http://200.46.78.75/portal_migracion_digital/views/visa_cuba.php";
      
      public bool GetMexitelNotification()
      {
         return GetNotification(Mexico, nameof(Mexico));
      }
      
      public bool GetPanamaNotification()
      {
         return GetNotification(Panama, nameof(Panama));
      }


      private bool GetNotification(string url, string target)
      {
         bool result = false;
         var currentPage = string.Empty;
         var oldPage = string.Empty;
         if(Application.Current.Properties.ContainsKey(target))
            oldPage = (string)Application.Current.Properties[target];
         using (var httpClient = new HttpClient())
            currentPage = httpClient.GetStringAsync(Mexico).Result;
         if (oldPage != currentPage)
         {
            if (string.IsNullOrWhiteSpace(oldPage))
            {
               App.Current.Properties[target] = currentPage;
               throw new Exception($"Se descargó la página de {target} para comparar");
            }
            result = true;
            oldPage = currentPage;
         }
         return result;
      }
   }
}
