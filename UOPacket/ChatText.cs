using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UONegotiator.UOPacket
{
    class ChatText : BaseUOPacket
    {
        private byte cmd = CMD.CHAT_TEXT;
        private List<byte> length;
        private List<byte> languageCode;
        private List<byte> type;
        private List<byte> data;

        public ChatText(List<byte> bytes)
        {
            length = bytes.GetRange(1, 2);
            languageCode = bytes.GetRange(3, 4);
            type = bytes.GetRange(7, 2);
            data = bytes.GetRange(9, bytes.Count - 9);
        }

        public override byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.Add(cmd);
            bytes.AddRange(length);
            bytes.AddRange(languageCode);
            bytes.AddRange(type);
            bytes.AddRange(data);

            return bytes.ToArray();
        }

        public override byte GetCmd() { return this.cmd; }
    }
}
