using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemotePic
{
    public partial class FormMain : Form
    {
        private string picDir = @"F:\Code\RemotePic\RemotePic\bin\Debug\pics\";
        private int imageIndex = 1;

        public FormMain()
        {
            InitializeComponent();
        }

        private string GetImageName()
        {
            return $"{imageIndex}.jpg";
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            string imgLocation = LoadPic();
            this.pbMain.ImageLocation = imgLocation;

            Send(imgLocation);
        }

        private string LoadPic()
        {
            return Path.Combine(picDir,GetImageName());
        }

        private void Send(string imgLocation)
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect("127.0.0.1", 2019);
            using (var stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read))
            {

                //var commandData = new byte[4];//协议命令只占4位
                var commandData = Encoding.UTF8.GetBytes("Show");//协议命令只占4位,如果占的位数长过协议，那么协议解析肯定会出错的

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

                //for (int i = 0; i < 1000; i++)
                //{
                    socket.Send(sendData);
                //}
                socket.Close();
            }
        }

        private void btnFullScreen_Click(object sender, EventArgs e)
        {
            string imgLocation = LoadPic();
            this.pbMain.ImageLocation = imgLocation;
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect("127.0.0.1", 2019);
            using (var stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read))
            {

                //var commandData = new byte[4];//协议命令只占4位
                var commandData = Encoding.UTF8.GetBytes("Full");//协议命令只占4位,如果占的位数长过协议，那么协议解析肯定会出错的

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

                //for (int i = 0; i < 1000; i++)
                //{
                socket.Send(sendData);
                //}
                socket.Close();
            }
        }

        private void btnBefore_Click(object sender, EventArgs e)
        {
            if (imageIndex <= 1) return;
             imageIndex--;
            string imgLocation = LoadPic();
            this.pbMain.ImageLocation = imgLocation;
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect("127.0.0.1", 2019);
            using (var stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read))
            {

                //var commandData = new byte[4];//协议命令只占4位
                var commandData = Encoding.UTF8.GetBytes("Show");//协议命令只占4位,如果占的位数长过协议，那么协议解析肯定会出错的

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

                //for (int i = 0; i < 1000; i++)
                //{
                socket.Send(sendData);
                //}
                socket.Close();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            imageIndex++;
            string imgLocation = LoadPic();
            if (!File.Exists(imgLocation))
            {
                imageIndex--;
                imgLocation = LoadPic();
            }
            this.pbMain.ImageLocation = imgLocation;
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect("127.0.0.1", 2019);
            using (var stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read))
            {

                //var commandData = new byte[4];//协议命令只占4位
                var commandData = Encoding.UTF8.GetBytes("Show");//协议命令只占4位,如果占的位数长过协议，那么协议解析肯定会出错的

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

                //for (int i = 0; i < 1000; i++)
                //{
                socket.Send(sendData);
                //}
                socket.Close();
            }
        }
    }
}
