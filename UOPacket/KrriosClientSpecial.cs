using System;
using System.Collections.Generic;
using System.Text;

namespace UONegotiator.UOPacket
{
    // 0xF0
    class KrriosClientSpecial : BaseUOPacket
    {
        private byte cmd = CMD.KRRIOS_CLIENT_SPECIAL;
        private List<byte> length;
        private byte subcmd;
        private List<byte> data;

        public KrriosClientSpecial(List<byte> bytes)
        {
            length = bytes.GetRange(1, 2);
            subcmd = bytes[3];
            data = bytes.GetRange(4, bytes.Count - 4);
        }

        public override byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.Add(cmd);
            bytes.AddRange(length);
            bytes.Add(subcmd);
            bytes.AddRange(data);

            return bytes.ToArray();
        }

        public override PacketAction OnReceiveFromServer()
        {
            return PacketAction.DROP;
        }

        public override byte GetCmd() { return this.cmd; }
    }
}
