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

        }
    }
}
