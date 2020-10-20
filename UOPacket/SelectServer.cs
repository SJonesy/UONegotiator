using System;
using System.Collections.Generic;
using System.Text;

namespace UONegotiator.UOPacket
{
    class SelectServer : BaseUOPacket
    {
        public static int s_Size = 3;

        public new byte cmd = CMD.SELECT_SERVER;
        private List<byte> chosenShard;

        public SelectServer(List<byte> bytes)
        {
            chosenShard = bytes.GetRange(1, 2);
        }

        public override byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.Add(cmd);
            bytes.AddRange(chosenShard);

            return bytes.ToArray();
        }
    }
}
