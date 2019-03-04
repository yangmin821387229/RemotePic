﻿using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotePicClient
{
   public class RemotePicServer:AppServer<RemotePicSession,BinaryRequestInfo>
    {
        public RemotePicServer():base(new RemotePicReceiveFilterFactory())
        {

        }
    }
}
