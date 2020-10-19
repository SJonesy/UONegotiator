using System;
using System.Collections.Generic;
using System.Text;

namespace UONegotiator.UOPacket
{
    public class BaseUOPacket
    {
        public virtual PacketAction OnReceiveFromClient() { return PacketAction.FORWARD; }
        public virtual PacketAction OnReceiveFromServer() { return PacketAction.FORWARD; }
        public virtual byte[] GetBytes() { return new byte[1]; }

    }
}