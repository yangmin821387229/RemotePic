using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RemotePicClient;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private RemotePicServer server;

        public UnitTest1()
        {
            server = new RemotePicServer();

            //Setup the appServer
            if (!server.Setup(2019))
            {
                Console.WriteLine("Failed to setup!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine();

            //Try to start the appServer
            if (!server.Start())
            {
                Console.WriteLine("Failed to start!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("The server started successfully, press key 'q' to stop it!");

            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                continue;
            }

            //Stop the appServer
            server.Stop();

            Console.WriteLine("The server was stopped!");
            Console.ReadKey();
        }

        [TestMethod]
        public void TestMethod1()
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect("127.0.0.1", 2019);
            using (var stream = new FileStream("timg1.jpg",FileMode.Open,FileAccess.Read))
            {

                //var commandData = new byte[4];//协议命令只占4位
                var commandData = Encoding.UTF8.GetBytes("Test");//协议命令只占4位,如果占的位数长过协议，那么协议解析肯定会出错的

                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, (int)stream.Length);

                var dataLen = BitConverter.GetBytes(stream.Length);//int类型占4位，根据协议这里也只能4位，否则会出错

                var sendData = new byte[8 + stream.Length];//命令加内容长度为8

                // +-------+---+-------------------------------+
                // |request| l |                               |
                // | name  | e |    request body               |
                // |  (4)  | n |                               |
                // |       |(4)|                               |
                // +-------+---+-------------------------------+

                Array.ConstrainedCopy(commandData, 0, sendData, 0, 4);
                Array.ConstrainedCopy(dataLen, 0, sendData, 4, 4);
                Array.ConstrainedCopy(buffer, 0, sendData, 8, buffer.Length);

                for (int i = 0; i < 1000; i++)
                {
                    socket.Send(sendData);
                }

            }

            Console.Read();
        }
    }
}
