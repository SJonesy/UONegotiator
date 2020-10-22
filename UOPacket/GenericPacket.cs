using System;
using System.Collections.Generic;
using System.Text;

namespace UONegotiator.UOPacket
{
    class GenericPacket : BaseUOPacket
    {
        private byte cmd;
        List<byte> bytes;


        public GenericPacket(List<byte> bytes)
        {
            if (bytes.Count > 0)
                this.cmd = bytes[0];
            this.bytes = bytes;
        }

        public override byte[] GetBytes()
        {
            return this.bytes.ToArray();
        }

        public override byte GetCmd() { return this.cmd; }
    }
}
