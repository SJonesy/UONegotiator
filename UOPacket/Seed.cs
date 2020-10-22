/*
http://docs.polserver.com/packets/index.php?Packet=0xEF

Packet Name: KR/2D Client Login/Seed
Last Modified: 2008-12-16 07:57:46
Modified By: Pierce

Packet: 0xEF
Sent By: Client
Size: 21 bytes

Packet Build
BYTE[1] cmd
BYTE[4] seed, usually the client local ip
BYTE[4] client major version
BYTE[4] client minor version
BYTE[4] client revision version
BYTE[4] client prototype version

Subcommand Build
N/A


Notes
Normally older client send a 4 byte seed (local ip).
Newer clients 2.48.0.3+ (KR) and 6.0.5.0+ (2D) are sending
this packet.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace UONegotiator.UOPacket
{
    class Seed : BaseUOPacket
    {
        private byte cmd = CMD.SEED;
        private List<byte> seed;
        private List<byte> client_major;
        private List<byte> client_minor;
        private List<byte> client_revision;
        private List<byte> client_patch;

        public Seed(List<byte> bytes)
        {
            seed            = bytes.GetRange(1, 4);
            client_major    = bytes.GetRange(5, 4);
            client_minor    = bytes.GetRange(9, 4);
            client_revision = bytes.GetRange(13, 4);
            client_patch    = bytes.GetRange(17, 4);
        }

        public override byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.Add(cmd);
            bytes.AddRange(seed);
            bytes.AddRange(client_major);
            bytes.AddRange(client_minor);
            bytes.AddRange(client_revision);
            bytes.AddRange(client_patch);

            return bytes.ToArray();
        }

        public override byte GetCmd() { return this.cmd; }
    }
}

