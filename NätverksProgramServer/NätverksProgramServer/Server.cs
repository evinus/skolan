using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NätverksProgramServer
{
    public partial class Server : Form
    {

        List<TcpClient> klienter = new List<TcpClient>();
        TcpListener lyssnare;
        IPAddress hostAdress;
        List<int> tal = new List<int>();
        List<byte> filData = new List<byte>();
        public Server()
        {
            InitializeComponent();
            string hostNamn = Dns.GetHostName();
        }
        public async void StartaLyssna()
        {
            try
            {
               TcpClient klient = await lyssnare.AcceptTcpClientAsync();
               klienter.Add(klient);
               Lyssna(klient);
            }
            catch(Exception error)
            {
                MessageBox.Show("kunde inte lysssna");
                return;
            }

        }
        public async void Lyssna(TcpClient client)
        {
            byte[] buffer;
            byte[] nr = new byte[1];
            int n = 0;
            try
            {
                n = await client.GetStream().ReadAsync(nr, 0, 1);
                buffer = new byte[n];
                await client.GetStream().ReadAsync(buffer, 0, n);
                foreach(TcpClient k in klienter)
                {
                    if (k == client) continue;
                    await k.GetStream().WriteAsync(nr, 0, 1);
                    await k.GetStream().WriteAsync(buffer, 0, n);
                }
            }
            catch(Exception error)
            {
                tbxLogg.AppendText(DateTime.Now.ToString("h:mm:ss tt") + error.Message + "\r\n");
            }
        }
    }
}
