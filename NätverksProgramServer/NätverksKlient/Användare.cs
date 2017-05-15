using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;


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
        catch (Exception error)
        {

        }
    }
    public static async void Skickameddelande(string meddelande)
    {
        try
        {
            //gör om talet 2 till bytes och skickar det
            byte[] tal = BitConverter.GetBytes(2);
            await klient.GetStream().WriteAsync(tal, 0, 4);
            //gör om meddelandet till bytes och skickar det
            byte[] medel = Encoding.Unicode.GetBytes(meddelande);
            await klient.GetStream().WriteAsync(medel, 0, medel.Length);

        }
        catch (Exception error)//blir det fel
        {
            
            return;
        }
    }
    public static async void Lyssna()
    {
        int tal;
        
    }

}

