using System;
using System.Net.Http;

namespace NotiMexitel
{
   public class NotiRequestService //: INotiRequest
   {
      const string url = "https://embamex.sre.gob.mx/cuba/index.php/avisos/159-info-de-sc-citas-mexitel";
      string currentPage = string.Empty;
      string oldPage = string.Empty;

      public bool GetMexitelNotification()
      {
         bool result = false;
         using (var httpClient = new HttpClient())
            currentPage = httpClient.GetStringAsync(url).Result;
         if (oldPage != currentPage)
         {
            if (string.IsNullOrWhiteSpace(oldPage))
            {
               oldPage = currentPage;
               throw new Exception("Se descargó la página para comparar");
            }
            result = true;
            oldPage = currentPage;
         }
         return result;
      }
   }
}
