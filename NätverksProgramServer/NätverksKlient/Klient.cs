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
using System.IO;

namespace NätverksKlient
{
    public partial class Klient : Form
    {
        TcpClient klient = new TcpClient();
        public Klient()
        {
            InitializeComponent();
            klient.NoDelay = true;
            btnSendFile.Enabled = false;
        }

        public async void Connect()
        {
            IPAddress adress = IPAddress.Parse(tbxIP.Text);
            int port = int.Parse(tbxport.Text);

            try
            {
                await klient.ConnectAsync(adress, port);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Kunde inte ansluta"); return;
            }
            Lyssna();
            btnConnect.Enabled = false;
            btnSendFile.Enabled = true;
        }

         public async void StartSending ( )
        {
            if ( klient.Connected )
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    byte[] filData = System.IO.File.ReadAllBytes(openFileDialog1.FileName);
                    
                    byte[] nr = BitConverter.GetBytes(filData.Length);
                    try
                    {
                        await klient.GetStream().WriteAsync(nr, 0, nr.Length);
                        await klient.GetStream().WriteAsync(filData, 0, filData.Length);
                    }
                    catch (Exception error) { MessageBox.Show(error.Message, "Klientfel"); return; }
                }
            }
        }
        public async void Lyssna()
        {
            
                try
                {
                    byte[] buffer;
                    byte[] nr = new byte[4];
                    
                    int n = await klient.GetStream().ReadAsync(nr, 0, 4);
                    MessageBox.Show("har börjat ta emot filen");
                   int filstorlek = BitConverter.ToInt32(nr, 0);
                   buffer = new byte[filstorlek];
                    await klient.GetStream().ReadAsync(buffer, 0, filstorlek);
                    MessageBox.Show("har tagit emot filen");
                    //await utström.WriteAsync(buffer, 1, n);
                    //StreamWriter  skrivare = new StreamWriter(utström);
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        
                        File.WriteAllBytes(saveFileDialog1.FileName, buffer);
                    }

                }
                catch
                {

                }
                Lyssna();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!klient.Connected) Connect();
        }

        private void btnSendFile_Click(object sender, EventArgs e)
        {
            StartSending();
        }

        private void Klient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (klient != null) klient.Close();
        }
    }
}
