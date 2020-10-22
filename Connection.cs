using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using UONegotiator.UOPacket;

namespace UONegotiator
{
    class Connection
    {
        private TcpClient tcpClient;
        private NetworkStream stream;
        private List<byte> parseBuffer;
        private List<byte> decompressBuffer;
        private string name;
        private int sessionIdentifier;

        public bool CompressionEnabled;
        public bool DecompressionEnabled;

        public Connection (TcpClient tcpClient, string name, int sessionIdentifier)
        {
            this.tcpClient = tcpClient;
            this.stream = tcpClient.GetStream();
            this.name = name;
            this.sessionIdentifier = sessionIdentifier;
            this.parseBuffer = new List<byte>();
            this.decompressBuffer = new List<byte>();
        }

        public void Write(byte[] bytes, byte cmd)
        {
            Output(bytes, cmd);

            if (CompressionEnabled)
            {
                byte[] buffer = new byte[bytes.Length * 4];
                int numCompressedBytes = 0;
                Compression.Compress(bytes, 0, bytes.Length, buffer, ref numCompressedBytes);
                stream.Write(buffer[0..numCompressedBytes]);
            }
            else
            {
                stream.Write(bytes);
            }

        }

        private void Output(byte[] bytes, byte cmd)
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
                string padding2 = String.Concat(Enumerable.Repeat(" ", 16 - count));
                Console.WriteLine("[{0}] {1}: {2} {3} {4} {5} {6:x2}",
                    sessionIdentifier, name, hex, padding, msg, padding2, cmd);
            }
        }

        public bool ReadData()
        {
            if (tcpClient.Available == 0)
                return false;

            byte[] buffer = new byte[tcpClient.ReceiveBufferSize];
            int numReadBytes = stream.Read(buffer, 0, tcpClient.Available);

            if (DecompressionEnabled)
            {
                decompressBuffer.AddRange(buffer[0..numReadBytes]);
                Decompress();
            }
            else
            {
                parseBuffer.AddRange(buffer[0..numReadBytes]);
            }
                
            return true;
        }

        private int Decompress()
        {
            byte[] buffer = new byte[tcpClient.ReceiveBufferSize];
            int numBytesDecompressed = 0;

            numBytesDecompressed = Decompression.Decompress(ref buffer, tcpClient.ReceiveBufferSize, decompressBuffer.ToArray(), decompressBuffer.Count - 1);
            if (numBytesDecompressed > 0)
            {
                parseBuffer.AddRange(buffer[0..numBytesDecompressed]);
                decompressBuffer.Clear();
            }

            return numBytesDecompressed;
        }

        public BaseUOPacket GetNextPacket(Source source)
        {
            byte cmd = parseBuffer[0];
            int size = PacketUtil.GetSize(parseBuffer, source);
            if (size > parseBuffer.Count)
            {
                // We haven't received the entire packet yet
                return null;
            }
            if (size == 0)
            {
                Console.WriteLine("[{0}] Unknown packet 0x{1:x2} detected, chomping byte..", sessionIdentifier, cmd);
                this.ChompBytes(1);
                return null;
            }

            UOPacket.BaseUOPacket packet = PacketUtil.GetPacket(parseBuffer.GetRange(0, size), source);
            parseBuffer.RemoveRange(0, size);
            return packet;
        }

        public List<byte> ChompBytes(int numBytes)
        {
            byte[] bytes = new byte[4];
            stream.Read(bytes, 0, numBytes);
            
            return new List<byte>(bytes);
        }

        // https://stackoverflow.com/questions/1387459/how-to-check-if-tcpclient-connection-is-closed/19706302
        public bool Connected()
        {
            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] tcpConnections = ipProperties.GetActiveTcpConnections()
                .Where(   x => x.LocalEndPoint.Equals(tcpClient.Client.LocalEndPoint) 
                       && x.RemoteEndPoint.Equals(tcpClient.Client.RemoteEndPoint))
                .ToArray();

            if (tcpConnections != null && tcpConnections.Length > 0)
            {
                TcpState stateOfConnection = tcpConnections.First().State;
                if (stateOfConnection == TcpState.Established)
                {
                    return true;
                }
            }

            return true;
        }

        public NetworkStream GetStream()
        {
            return this.stream;
        }

        public void CopyTo(NetworkStream outStream)
        {
            if (tcpClient.Available > 0)
            {
                byte[] buffer = new byte[tcpClient.ReceiveBufferSize];
                int numReadBytes = stream.Read(buffer, 0, tcpClient.Available);
                parseBuffer.AddRange(buffer[0..numReadBytes]);
            }
            if (parseBuffer.Count > 0)
            {
                byte[] bytes = parseBuffer.ToArray();
                outStream.Write(bytes);
                Output(bytes, bytes[0]);
                parseBuffer.Clear();
            }
        }

        public void Close()
        {
            tcpClient.Close();
        }

        public bool PendingParse()
        {
            return (parseBuffer.Count > 0);
        }

        public bool PendingDecompress()
        {
            return (decompressBuffer.Count > 0);
        }
    }
}
