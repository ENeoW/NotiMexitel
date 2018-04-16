using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace NotiMexitel
{
   public class NotiRequestService : INotiRequest
   {
      public async Task<bool> GetMexitelNotification()
      {
         var currentPage = string.Empty;
         //TODO recuperar la url desde algun lugar
         var url = "https://embamex.sre.gob.mx/cuba/index.php/avisos/159-info-de-sc-citas-mexitel";
         //TODO recuperar la oldPage desde algun lugar
         var oldPage = string.Empty;
         try
         {
            using (var httpClient = new HttpClient())
               currentPage = await httpClient.GetStringAsync(url);
            if (!string.IsNullOrWhiteSpace(oldPage) && oldPage != currentPage)
            {
               //TODO salvar el contenido de CurrentPage.
               return true;
            }
            return false;
         }
         catch (Exception e)
         {
            Log.Warning("Internet connection", e.Message);
            throw new Exception($"No se ha podido acceder a la página debido a: {e.Message}");            
         }         
      }      
   }
}
