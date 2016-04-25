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
            lyssnare = new TcpListener(IPAddress.Any, 12345);
            lyssnare.Start();
            StartaLyssna();
        }
        public async void StartaLyssna()
        {
            try
            {
               TcpClient klient = await lyssnare.AcceptTcpClientAsync();
               klienter.Add(klient);
               tbxLogg.AppendText(DateTime.Now.ToString("h:mm:ss tt") + ": " + (klient.Client.RemoteEndPoint as IPEndPoint).Address.ToString() + " Har anslutet \r\n");
               Lyssna(klient);
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message +" \r\n kunde inte lysssna");
                return;
            }
            StartaLyssna();
        }
        public async void Lyssna(TcpClient client)
        {
            byte[] buffer;
            byte[] nr = new byte[1024];
            byte[] namn = new byte[1024];
            int n = 0;
            try
            {
                //Läser in namnet
                await client.GetStream().ReadAsync(namn, 0, 1024);
                //läser in längden
                n = await client.GetStream().ReadAsync(nr, 0, 4);
                int filStorlek = BitConverter.ToInt32(nr, 0);
                tbxLogg.AppendText("\r\n har fått storleken " + filStorlek.ToString());
                buffer = new byte[filStorlek];
                //Läser in filen
                await client.GetStream().ReadAsync(buffer, 0, filStorlek);
                tbxLogg.AppendText("\r\n Ska börja skicka filen");

                //SKickar ut filen till alla klienter
                foreach(TcpClient k in klienter)
                {
                   if (k == client) continue;
                        await k.GetStream().WriteAsync(namn, 0, 1024);
                       await  k.GetStream().WriteAsync(nr, 0, 4);
                        await k.GetStream().WriteAsync(buffer, 0, filStorlek);
                    
                }

            }
            catch(Exception error)
            {
                tbxLogg.AppendText(DateTime.Now.ToString("h:mm:ss tt") + error.Message + "\r\n");
                if (client.Connected == false) { klienter.Remove(client); return; }
            }
            Lyssna(client);
        }
    }
}
