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

namespace Server
{
	public partial class ServerForm : Form
	{
		TcpListener	lyssnare;
		TcpClient	klient;

        IPAddress   hostAddress;

		public ServerForm( )
		{
			InitializeComponent();

            string hostName = Dns.GetHostName();
            IPAddress[] hostAddresses = Dns.GetHostAddresses( hostName );
            foreach ( IPAddress address in hostAddresses )
            {
                if ( address.AddressFamily == AddressFamily.InterNetwork )
                {
                    hostAddress = address;
                    Text = hostName + ": " + address.ToString();
                }
            }
		}

		private void btnLyssna_Click( object sender, EventArgs e )
		{
			lyssnare = new TcpListener( hostAddress, int.Parse( tbxPort.Text ) );
			lyssnare.Start();
			StartAccepting();
		}

        public async void StartAccepting()
        {
            try
            {
                klient = await lyssnare.AcceptTcpClientAsync();
            }
            catch ( Exception error )
            {
                MessageBox.Show( error.Message, "Serverfel" );
                return;
            }

            StartReading();
        }
        public async void StartReading()
        {
            byte[] buffer = new byte[1024];

			int n = 0;
            try {
                n = await klient.GetStream().ReadAsync( buffer, 0, 1024 );
            }
            catch ( Exception error ) { MessageBox.Show( error.Message, "Serverfel" ); return;}

            tbxKlientData.AppendText( Encoding.Unicode.GetString( buffer, 0, n ) );

            StartReading();
        }
	}
}
