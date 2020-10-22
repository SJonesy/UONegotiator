
using System;
using System.Collections.Generic;
using System.Text;

namespace UONegotiator.UOPacket
{
    class AssistVersion : BaseUOPacket
    {
        private byte cmd = CMD.ASSIST_VERSION;
        private List<byte> length;
        private List<byte> assistVersion;

        public AssistVersion(List<byte> bytes)
        {
            length = bytes.GetRange(1, 2);
            assistVersion = bytes.GetRange(3, bytes.Count - 3);
        }

        public override byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.Add(cmd);
            bytes.AddRange(length);
            bytes.AddRange(assistVersion);

            return bytes.ToArray();
        }

        public override PacketAction OnReceiveFromServer()
        {
            return PacketAction.DROP;
        }

        public override byte GetCmd() { return this.cmd; }

    }
}
