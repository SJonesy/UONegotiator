using System;
using System.Collections.Generic;
using System.Text;

namespace UONegotiator.UOPacket
{
    class UnknownPacket : BaseUOPacket
    {
        public new byte cmd;
        private byte[] bytes;
        public UnknownPacket(byte[] bytes)
        {
            this.bytes = bytes;
            this.cmd = bytes[0];
        }

        public override byte[] GetBytes()
        {
            return this.bytes;
        }
    }
}
