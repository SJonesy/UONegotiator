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
        // We get the key in 0x8C from the server, and send it in 0x91 from the client in
        // the next session.. we aren't currently using this, but maybe it will be necessary
        // for some connection restoration later on.
        private byte[] key;

        public Session(TcpClient incomingClient, int incomingSessionIdentifier, ref Queue<byte[]> keyQueue)
        {
            int sessionIdentifier = incomingSessionIdentifier;
            key = new byte[4];
            Console.WriteLine("[{0}] Creating session.", sessionIdentifier);

            TcpClient clientTcpClient = incomingClient;
            // TODO:  get this from a config or something
            TcpClient serverTcpClient = new TcpClient("127.0.0.1", 2593);
            bool smartReadPackets = true;

            Connection client = new Connection(clientTcpClient, "S->C", sessionIdentifier);
            Connection server = new Connection(serverTcpClient, "C->S", sessionIdentifier);

            // We must first copy our key packet over, because the client weirdly sends
            // [4 BYTE KEY]0x91[4 BYTE KEY] so there's no packet cmd to parse..
            if (keyQueue.Count > 0)
            {
                key = keyQueue.Dequeue();
                Console.WriteLine("[{0}] Key {1} found, forwarding 4 key bytes from the client:", sessionIdentifier, BitConverter.ToString(key));
                List<byte> incomingKey = client.ChompBytesFromStream(4);

                server.Write(incomingKey.ToArray(), CMD.UNKNOWN);
            }

            while (client.Connected() && server.Connected())
            {
                if (!smartReadPackets)
                {
                    // TODO: This doesn't actually seem to work, as 
                    // soon as I start dumb-forwarding packets the game
                    // stops working..
                    client.CopyTo(server.GetStream());
                    server.CopyTo(client.GetStream());
                    continue;
                }

                if (client.ReadData() || client.PendingParse() || client.PendingDecompress())
                {
                    var packet = client.GetNextPacket(Source.CLIENT);
                    if (packet == null)  // Waiting for more data
                        continue;

                    byte cmd = packet.GetCmd();

                    PacketAction action = packet.OnReceiveFromClient();
                    if (action == PacketAction.FORWARD)
                    {
                        server.Write(packet.GetBytes(), cmd);
                    }
                    else if (action == PacketAction.DROP)
                    {
                        Console.WriteLine("[{0}] C->S: Dropping packet {1:x2}", sessionIdentifier, cmd);
                    }

                    if (cmd == CMD.GAME_SERVER_LOGIN)
                    {
                        Console.WriteLine("[{0}] Packet 0x91 sent to the server.  Enabling compression.", sessionIdentifier);
                        server.DecompressionEnabled = true;
                        client.CompressionEnabled = true;
                    }
                }
                if (server.ReadData() || server.PendingParse() || server.PendingDecompress())
                {
                    var packet = server.GetNextPacket(Source.SERVER);
                    if (packet == null)  // Waiting for more data
                        continue;

                    byte cmd = packet.GetCmd();

                    if (cmd == CMD.CONNECT_TO_GAME_SERVER)
                    {
                        List<byte> bytes = packet.GetBytesAsList();
                        // Get last 4 bytes
                        key = bytes.Skip(Math.Max(0, bytes.Count() - 4)).ToArray();

                        keyQueue.Enqueue(key);
                    }

                    PacketAction action = packet.OnReceiveFromServer();
                    if (action == PacketAction.FORWARD)
                    {
                        client.Write(packet.GetBytes(), cmd);
                    }
                    else if (action == PacketAction.DROP)
                    {
                        Console.WriteLine("[{0}] S->C: Dropping packet {1:x2}", sessionIdentifier, cmd);
                    }

                    if (cmd == CMD.CONNECT_TO_GAME_SERVER)
                    {
                        Console.WriteLine("[{0}] packet 0x8C (Connect) received from the server. Gracefully ending session.", sessionIdentifier, cmd);
                        break;
                    }
                }
            }

            client.Close();
            server.Close();
            Console.WriteLine("[{0}] Session ended.", sessionIdentifier);
        }
    }
}
