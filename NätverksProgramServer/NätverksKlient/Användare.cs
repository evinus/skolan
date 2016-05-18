using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NätverksKlient
{
    public static class Användare
    {
        public static TcpClient klient = new TcpClient();
        public static string Namn { get; set; }


        
      
        public static async void Connect(string adress, int port, string namn)
        {
            Namn = namn;
            byte[] _namn = Encoding.Unicode.GetBytes(Namn);
            IPAddress Adress = IPAddress.Parse(adress);
            try
            {
                await klient.ConnectAsync(Adress, port);
                await klient.GetStream().WriteAsync(_namn, 0, _namn.Length);
            }
            catch(Exception error)
            {

            }
        }

    }
}
