/* 
http://docs.polserver.com/packets/index.php?Packet=0x80

Packet Name: Login Request
Last Modified: 2009-02-28 15:42:51
Modified By: MuadDib

Packet: 0x80
Sent By: Client
Size: 62 Bytes

Packet Build
BYTE[1] Command
BYTE[30] Account Name
BYTE[30] Password
BYTE[1] NextLoginKey value from uo.cfg on client machine.
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace UONegotiator.UOPacket
{
    class LoginRequest : BaseUOPacket
    {
        private byte cmd = CMD.LOGIN_REQUEST;
        private List<byte> accountName;
        private List<byte> password;
        private byte nextLoginKey;

        public LoginRequest(List<byte> bytes)
        {
            accountName = bytes.GetRange(1, 30);
            password = bytes.GetRange(31, 30);
            nextLoginKey = bytes[32];
        }

        public override byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.Add(cmd);
            bytes.AddRange(accountName);
            bytes.AddRange(password);
            bytes.Add(nextLoginKey);

            return bytes.ToArray();
        }

        public override byte GetCmd() { return this.cmd; }
    }
}
