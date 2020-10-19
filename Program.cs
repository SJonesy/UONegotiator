using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace UONegotiator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to UO Negotiator.");

            TcpListener listener = new TcpListener(IPAddress.Loopback, 2700);
            listener.Start();

            int sessionIdentifiter = 0;
            while (true)
            {
                TcpClient connectingClient = listener.AcceptTcpClient();
                Thread sessionThread = new Thread( unused => new Session(connectingClient, sessionIdentifiter) );
                sessionThread.Start(connectingClient);

                sessionIdentifiter++;
            }
        }
    }
}
