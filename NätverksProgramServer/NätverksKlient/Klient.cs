using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace NätverksKlient
{
    public partial class Klient : Form
    {
        TcpClient klient = new TcpClient();
        public Klient()
        {
            InitializeComponent();
            klient.NoDelay = true;
        }

         public async void StartSending ( string message )
        {
            if ( klient.Connected )
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    byte[] filData = System.IO.File.ReadAllBytes(openFileDialog1.FileName);

                    try
                    {
                        await klient.GetStream().WriteAsync(filData, 0, filData.Length);
                    }
                    catch (Exception error) { MessageBox.Show(error.Message, "Klientfel"); return; }
                }
            }
        }
    }
}
