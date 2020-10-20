using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace UONegotiator.UOPacket
{
    class SupportedFeatures : BaseUOPacket
    {
        public static int s_Size = 3;

        public new byte cmd = CMD.SUPPORTED_FEATURES;
        private List<byte> flags;

        public SupportedFeatures(List<byte> bytes)
        {
            flags = bytes.GetRange(1, 2);
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
    }
}
