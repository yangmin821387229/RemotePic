using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotePicClient
{
    public delegate void UIEventHandler(byte[] image,bool fullScreen);
    public class UIHelper
    {
       public static event UIEventHandler RaiseUIEvent;

        public static void InvokeEvent(byte[] image,bool fullScreen=false)
        {
            RaiseUIEvent(image,fullScreen);
        }
    }
}
