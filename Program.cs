using System;
using System.Collections.Generic;
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

            // TODO: queue might be overkill, do I ever need more than 1 key stored at a time?
            Queue<byte[]> keyQueue = new Queue<byte[]>();

            int sessionIdentifiter = 0;
            while (true)
            {
                TcpClient connectingClient = listener.AcceptTcpClient();
                Thread sessionThread = new Thread( unused => new Session(connectingClient, sessionIdentifiter, ref keyQueue) );
                sessionThread.Start(connectingClient);

                sessionIdentifiter++;
            }
        }
    }
}
