using System;
using System.Collections.Generic;
using System.Text;

namespace UONegotiator.UOPacket
{
    class Connect : BaseUOPacket
    {
        private byte cmd = CMD.CONNECT_TO_GAME_SERVER;
        private List<byte> ip;
        private List<byte> port;
        private List<byte> key;

        public Connect(List<byte> bytes)
        {
            ip = bytes.GetRange(1, 4);
            port = bytes.GetRange(5, 2);
            key = bytes.GetRange(7, 4);
        }

        public override byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.Add(cmd);
            bytes.AddRange(ip);
            bytes.AddRange(port);
            bytes.AddRange(key);

            return bytes.ToArray();
        }

        public override List<byte> GetBytesAsList()
        {
            List<byte> bytes = new List<byte>();
            bytes.Add(cmd);
            bytes.AddRange(ip);
            bytes.AddRange(port);
            bytes.AddRange(key);

            return bytes;
        }

        public override byte GetCmd() { return this.cmd; }
    }
}
