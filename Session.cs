using System;
using System.Collections.Generic;
using System.IO.Pipes;
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

            UOPacket.Seed seedPacket = new UOPacket.Seed(50, 7, 0, 10, 3);
            WriteToServer(seedPacket.GetBytes());

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
                        WriteToServer(incomingPacket.GetBytes());
                    }

                    WriteToClient(bytes);
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
            NetworkStream clientStream = server.GetStream();
            clientStream.Write(bytes);
            Output("S->C", bytes);
        }

        private void Output(string prefix, byte[] bytes)
        {
            List<byte> byteList = new List<byte>(bytes);
            int chunkCount = byteList.Count / 16;
            int lastChunk = byteList.Count % 16;
            for (int i = 0; i <= chunkCount; i++)
            {
                // TODO you're in the middle of working on this
                var byteArray = byteList.GetRange(i * 16, Math.Min(16, byteList.Count) ).ToArray();
                string hex = BitConverter.ToString(byteArray).Replace("-", " ");
                string msg = "";
                foreach (var b in byteArray)
                {
                    if (b.Equals(0x00))
                        msg += ".";
                    else
                        msg += Encoding.ASCII.GetString(new byte[] { b });
                }

                Console.WriteLine("[{0}] {1}: {2}  {3}", sessionIdentifier, prefix, hex, msg);
            }


            // Start
            //var byteChunk = new byte[16];

            //int incomingOffset = 0;
            //while (incomingOffset < bytes.Length)
            //{
            //    int length = Math.Min(byteChunk.Length, bytes.Length - incomingOffset);

            //    Buffer.BlockCopy(bytes, incomingOffset, byteChunk, 0, length);

            //    incomingOffset += length;

            //    string hex = BitConverter.ToString(byteChunk).Replace("-", " ");
            //    string msg = "";
            //    foreach (var b in byteChunk)
            //    {
            //        if (b.Equals(0x00))
            //            msg += ".";
            //        else
            //            msg += Encoding.ASCII.GetString(new byte[] { b });
            //    }

            //    Console.WriteLine("[{0}] {1}: {2}  {3}", sessionIdentifier, prefix, hex, msg);
            //}
        }
    }
}
