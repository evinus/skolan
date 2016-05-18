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
                // lyssnar efter inkommande klienter och sen starta lyssna funktioner på den klienten.
               TcpClient klient = await lyssnare.AcceptTcpClientAsync();
               klient.NoDelay = false;
               klienter.Add(klient);
               tbxLogg.AppendText("\r\n"+ DateTime.Now.ToString("h:mm:ss tt") + ": " + (klient.Client.RemoteEndPoint as IPEndPoint).Address.ToString() + " Har anslutet");
               Lyssna(klient);
            }
            catch(Exception error)
            {
                MessageBox.Show( "\r\n"+ error.Message +" kunde inte lysssna", this.Text );
                return;
            }
            StartaLyssna();
        }
        public async void Lyssna(TcpClient client)
        {
            byte[] buffer;
            byte[] nr = new byte[4];
            byte[] namn = new byte[1024];
            int n = 0;
            byte[] tal = new byte[4];
            byte[] Meddelande = new byte[200];
            try
            {
                //ser vilket tal som har skickats.   
                await client.GetStream().ReadAsync(tal, 0, 4);
                int Tal = BitConverter.ToInt32(tal, 0);

                if (Tal == 1)
                {
                    //läser in storleken på filen.
                    n = await client.GetStream().ReadAsync(nr, 0, 4);

                    int filStorlek = BitConverter.ToInt32(nr, 0);
                    tbxLogg.AppendText("\r\n har fått storleken " + filStorlek.ToString());


                    buffer = new byte[filStorlek];
                    int a = 0;
                    //Läser in filen
                    while (client.Available > 0)
                    {
                        a += await client.GetStream().ReadAsync(buffer, a, client.Available);
                    }
                    tbxLogg.AppendText("\r\n Ska börja skicka filen");

                    //SKickar ut filen till alla klienter
                    for (int i = 0; i < klienter.Count; i++)
                    {
                        //if (k == client) continue;
                        await klienter[i].GetStream().WriteAsync(tal, 0, 4);
                        await klienter[i].GetStream().WriteAsync(nr, 0, 4);
                        await klienter[i].GetStream().WriteAsync(buffer, 0, filStorlek);

                    }
                }
                if (Tal == 2) // tar emot meddelandet och skickar vidare det.
                {
                    await client.GetStream().ReadAsync(Meddelande, 0, 200);

                    for (int i = 0; i < klienter.Count; i++)
                    {
                        await klienter[i].GetStream().WriteAsync(tal, 0, 4);
                        await klienter[i].GetStream().WriteAsync(Meddelande, 0, 200);
                    }
                }

            }
            catch (Exception error) // Om det blir fel tas klienten borts.
            {
                tbxLogg.AppendText("\r\n" + DateTime.Now.ToString("h:mm:ss tt") + error.Message);
                if (client.Connected == false) { klienter.Remove(client); client.Close(); return; }
            }
            Lyssna(client);
        }

        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            lyssnare.Stop();
            foreach (TcpClient k in klienter)
            {
                k.Close();
            }
        } 
    }
}
