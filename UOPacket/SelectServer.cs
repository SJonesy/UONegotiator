using System;
using System.Collections.Generic;
using System.Text;

namespace UONegotiator.UOPacket
{
    class SelectServer : BaseUOPacket
    {
        private byte cmd = CMD.SELECT_SERVER;
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

        public override byte GetCmd() { return this.cmd; }
    }
}
