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
            // TODO:  get this from a config or something
            server = new TcpClient("127.0.0.1", 2593);
            NetworkStream clientStream = client.GetStream();
            NetworkStream serverStream = server.GetStream();
            bool smartReadPackets = true;

            List<byte> clientBytesToParse = new List<byte>();
            List<byte> serverBytesToParse = new List<byte>();

            // TODO: .Connected does nothing
            while (client.Connected && server.Connected)
            {
                if (client.Available > 0)
                {
                    byte[] bytes = new byte[client.ReceiveBufferSize];
                    int numReadBytes = clientStream.Read(bytes, 0, client.Available);
                    clientBytesToParse.AddRange(bytes[0..numReadBytes]);
                    while (clientBytesToParse.Count > 0)
                    {
                        byte cmd = clientBytesToParse[0];
                        int size = PacketUtil.GetSize(cmd);
                        if (size > clientBytesToParse.Count)
                        {
                            // We haven't received the entire packet yet
                            break;
                        }
                        if (size == 0)
                        {
                            Console.WriteLine("[{0}] Unknown packet 0x{1:x2} detected, switching to dumb-packet-forwarding mode.", sessionIdentifier, cmd);
                            smartReadPackets = false;
                        }

                        UOPacket.BaseUOPacket incomingPacket = PacketUtil.GetPacket(clientBytesToParse.GetRange(0, size));
                        clientBytesToParse.RemoveRange(0, size);
                        var packetResult = incomingPacket.OnReceiveFromClient();
                        if (packetResult == PacketAction.FORWARD)
                        {
                            WriteToServer(incomingPacket.GetBytes());
                        }
                        else if (packetResult == PacketAction.DROP)
                        {
                            Console.WriteLine("[{0}] C->S: Dropping packet {1} with size {2}", sessionIdentifier, incomingPacket.cmd, PacketUtil.GetSize(incomingPacket.cmd));
                        }
                    }
                }

                if (server.Available > 0)
                {
                    byte[] bytes = new byte[server.ReceiveBufferSize];
                    int numReadBytes = serverStream.Read(bytes, 0, server.Available);
                    serverBytesToParse.AddRange(bytes[0..numReadBytes]);
                    while (serverBytesToParse.Count > 0)
                    {
                        byte cmd = serverBytesToParse[0];
                        int size = PacketUtil.GetSize(cmd);
                        if (size > serverBytesToParse.Count)
                        {
                            // We haven't received the entire packet yet
                            break;
                        }
                        if (size == 0)
                        {
                            Console.WriteLine("[{0}] Unknown packet 0x{1:x2} detected, switching to dumb-packet-forwarding mode.", sessionIdentifier, cmd);
                            smartReadPackets = false;
                        }

                        UOPacket.BaseUOPacket incomingPacket = PacketUtil.GetPacket(serverBytesToParse.GetRange(0, size));
                        serverBytesToParse.RemoveRange(0, size);
                        var packetResult = incomingPacket.OnReceiveFromServer();
                        if (packetResult == PacketAction.FORWARD)
                        {
                            WriteToClient(incomingPacket.GetBytes());
                        }
                        else if (packetResult == PacketAction.DROP)
                        {
                            Console.WriteLine("[{0}] S->C: Dropping packet {1} with size {2}", sessionIdentifier, incomingPacket.cmd, PacketUtil.GetSize(incomingPacket.cmd));
                        }
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
