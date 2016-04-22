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

namespace SägHej
{
	public partial class KlientForm : Form
	{
		TcpClient klient = new TcpClient();

		public KlientForm( )
		{
			InitializeComponent();
            klient.NoDelay = true;
		}

		private void btnSägHej_Click( object sender, EventArgs e )
		{
			StartSending( "Hej" );
		}

        public async void Connect ( )
        {
            IPAddress adress = IPAddress.Parse( tbxIPAdress.Text );
			int       port   = int.Parse( tbxPort.Text );

            try {
			    await klient.ConnectAsync( adress, port );
            }
            catch ( Exception error ) { MessageBox.Show( error.Message, "Klientfel" ); return; }

			btnSägHej.Enabled = true;
            btnAnslut.Enabled = false;
        }
        public async void StartSending ( string message )
        {
            if ( klient.Connected )
            {
                byte[] utData = Encoding.Unicode.GetBytes( "Hej!" );

                try {
				    await klient.GetStream().WriteAsync( utData, 0, utData.Length );
                }
                catch ( Exception error ) { MessageBox.Show( error.Message, "Klientfel" ); return; }
            }
        }

		private void KlientForm_FormClosing( object sender, FormClosingEventArgs e )
		{
			if ( klient != null )	klient.Close();
		}

        private void btnAnslut_Click( object sender, EventArgs e )
        {
            if ( ! klient.Connected ) Connect();
        }
	}
}
