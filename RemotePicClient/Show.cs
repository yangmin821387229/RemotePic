using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotePicClient
{
   public class Show:CommandBase<RemotePicSession,BinaryRequestInfo> 
    {
        public override void ExecuteCommand(RemotePicSession session, BinaryRequestInfo requestInfo)
        {
            UIHelper.InvokeEvent(requestInfo.Body);
        }
    }
}
