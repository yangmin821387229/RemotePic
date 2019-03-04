using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemotePicClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UIHelper.RaiseUIEvent += UIHelper_RaiseUIEvent;
        }

        private void UIHelper_RaiseUIEvent(byte[] image,bool fullScreen)
        {
            this.Invoke(new Action<byte[],bool>((t,b) =>
            {
                using (Stream stream = new MemoryStream(image))
                {
                    this.pictureBox1.Image = Image.FromStream(stream);
                }
                if(b)
                {
                    this.WindowState = FormWindowState.Maximized;
                }
            }
            ),image,fullScreen);    
        }
    }
}
