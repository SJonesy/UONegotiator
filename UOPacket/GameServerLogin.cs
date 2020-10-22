using System;
using System.Collections.Generic;
using System.Text;

namespace UONegotiator.UOPacket
{
    class GameServerLogin : BaseUOPacket
    {
        private byte cmd = CMD.GAME_SERVER_LOGIN;
        private List<byte> keyUsed;
        private List<byte> sid;
        private List<byte> password;

        public GameServerLogin(List<byte> bytes)
        {
            keyUsed = bytes.GetRange(1, 4);
            sid = bytes.GetRange(5, 30);
            password = bytes.GetRange(35, 30);
        }

        public override byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.Add(cmd);
            bytes.AddRange(keyUsed);
            bytes.AddRange(sid);
            bytes.AddRange(password);

            return bytes.ToArray();
        }

        public override byte GetCmd() { return this.cmd; }
    }
}
