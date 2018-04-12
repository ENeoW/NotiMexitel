using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NotiMexitel
{
   public class NotiRequestService : INotiRequest
   {
      public async Task<bool> GetMexitelNotification()
      {
         var currentPage = string.Empty;
         var url = string.Empty; //TODO recuperar la url desde algun lugar
         var oldPage = string.Empty; //TODO recuperar la oldPage desde algun lugar
         try
         {
            using (var httpClient = new HttpClient())
               currentPage = await httpClient.GetStringAsync(url);
            if (oldPage != currentPage)
            {
               //TODO salvar el contenido de CurrentPage.
               return true;
            }
            return false;
         }
         catch (Exception e)
         {
            throw new Exception($"No se ha podido acceder a la página debido a: {e.Message}");
         }         
      }      
   }
}
