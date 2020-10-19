using System;
using System.Collections.Generic;
using System.Text;

namespace UONegotiator.UOPacket
{
    class UnknownPacket : BaseUOPacket
    {
        private byte[] bytes;
        public UnknownPacket(byte[] bytes)
        {
            this.bytes = bytes;
        }

        public override byte[] GetBytes()
        {
            return this.bytes;
        }
    }
}
