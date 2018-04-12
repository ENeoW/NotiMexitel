using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotiMexitel
{
    public interface INotiRequest
    {
      Task<bool> GetMexitelNotification();
    }
}
