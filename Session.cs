using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace UONegotiator
{
    class Session
    {
        private TcpClient client;
        private TcpClient server;
        private int sessionIdentifier;

        public Session (TcpClient incomingClient, int incomingSessionIdentifier)
        {
            sessionIdentifier = incomingSessionIdentifier;
            Console.WriteLine("[{0}] Creating session.", sessionIdentifier);

            client = incomingClient;
            server = new TcpClient("127.0.0.1", 2593);
            NetworkStream clientStream = client.GetStream();
            NetworkStream serverStream = server.GetStream();

            // UOPacket.Seed seedPacket = new UOPacket.Seed(50, 7, 0, 10, 3);
            // WriteToServer(seedPacket.GetBytes());

            // TODO: .Connected does nothing
            while (client.Connected && server.Connected)
            {
                if (client.Available > 0)
                {
                    var bytes = new byte[client.Available];
                    clientStream.Read(bytes, 0, client.Available);

                    UOPacket.BaseUOPacket incomingPacket = PacketUtil.GetPacket(bytes);
                    var packetResult = incomingPacket.OnReceiveFromClient();
                    if (packetResult == PacketAction.FORWARD)
                    { 
                        WriteToServer(incomingPacket.GetBytes());
                    }
                }

                if (server.Available > 0)
                {
                    var bytes = new byte[server.Available];
                    serverStream.Read(bytes, 0, server.Available);

                    UOPacket.BaseUOPacket incomingPacket = PacketUtil.GetPacket(bytes);
                    var packetResult = incomingPacket.OnReceiveFromServer();
                    if (packetResult == PacketAction.FORWARD)
                    {
                        WriteToClient(incomingPacket.GetBytes());
                    }
                }
            }

            Console.WriteLine("[{0}] Session ended.", sessionIdentifier);
        }

        private void WriteToServer(byte[] bytes)
        {
            NetworkStream serverStream = server.GetStream();
            serverStream.Write(bytes);
            Output("C->S", bytes);
        }

        private void WriteToClient(byte[] bytes)
        {
            NetworkStream clientStream = client.GetStream();
            clientStream.Write(bytes);
            Output("S->C", bytes);
        }

        private void Output(string prefix, byte[] bytes)
        {
            List<byte> byteList = new List<byte>(bytes);
            int chunkLength = 16;
            int chunkCount = byteList.Count / chunkLength;
            for (int i = 0; i <= chunkCount; i++)
            {
                int offset = i * chunkLength;
                int count = Math.Min(chunkLength, byteList.Count - offset);

                var byteArray = byteList.GetRange(offset, count).ToArray();

                string hex = BitConverter.ToString(byteArray).Replace("-", " ");
                string msg = "";
                foreach (var b in byteArray)
                {
                    if (b.Equals(0x00))
                        msg += ".";
                    else if ((int)b < 32)
                        msg += "?";
                    else
                        msg += Encoding.ASCII.GetString(new byte[] { b });
                }
                string padding = String.Concat(Enumerable.Repeat("   ", 16 - count));
                Console.WriteLine("[{0}] {1}: {2} {3} {4}", sessionIdentifier, prefix, hex, padding, msg);
            }

        }
    }
}
