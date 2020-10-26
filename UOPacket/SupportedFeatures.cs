using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace UONegotiator.UOPacket
{
    class SupportedFeatures : BaseUOPacket
    {
        private byte cmd = CMD.SUPPORTED_FEATURES;
        private List<byte> flags;

        public SupportedFeatures(List<byte> bytes)
        {
            flags = bytes.GetRange(1, 4);
        }

        public override byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.Add(cmd);
            bytes.AddRange(flags);

            return bytes.ToArray();
        }

        public override PacketAction OnReceiveFromServer()
        {
            return PacketAction.DROP;
        }

        public override byte GetCmd() { return this.cmd; }
    }
}
