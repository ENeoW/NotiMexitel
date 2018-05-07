using System;
using System.Net.Http;
using Xamarin.Forms;

namespace NotiMexitel
{
   public class NotiRequestService //: INotiRequest
   {
      const string Mexico = "https://embamex.sre.gob.mx/cuba/index.php/avisos/159-info-de-sc-citas-mexitel";
      const string Panama = "http://200.46.78.75/portal_migracion_digital/views/visa_cuba.php";

      private static string oldPageMexico = string.Empty;
      private static string oldPagePanama = string.Empty;

      public bool GetMexitelNotification()
      {
         return GetNotification(Mexico, nameof(Mexico), ref oldPageMexico);
      }

      public bool GetPanamaNotification()
      {
         return GetNotification(Panama, nameof(Panama), ref oldPagePanama);
      }


      private bool GetNotification(string url, string target, ref string oldPage)
      {
         bool result = false;
         var currentPage = string.Empty;
         try
         {
            using (var httpClient = new HttpClient())
               currentPage = httpClient.GetStringAsync(Mexico).Result;
         }
         catch (Exception e)
         {
            throw new Exception($"No se pudo acceder a la página de {target}, revise su conexión a internet");
         }
         if (oldPage != currentPage)
         {
            if (string.IsNullOrWhiteSpace(oldPage))
            {
               oldPage = currentPage;
               throw new Exception($"Se descargó la página de {target} para comparar");
            }
            result = true;
            oldPage = currentPage;
         }
         return result;
      }
   }
}
