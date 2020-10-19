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
using System.Text;

namespace UONegotiator.UOPacket
{
    class Seed : BaseUOPacket
    {
        public int Size = 21;

        private byte cmd = CMD.SEED;
        private byte[] seed;
        private byte[] client_major;
        private byte[] client_minor;
        private byte[] client_revision;
        private byte[] client_patch;

        public Seed(byte[] seed, byte[] client_major, byte[] client_minor, byte[] client_revision, byte[] client_patch)
        {
            this.seed = seed;
            this.client_major = client_major;
            this.client_minor = client_minor;
            this.client_revision = client_revision;
            this.client_patch = client_patch;
        }

        public Seed(int seed, int client_major, int client_minor, int client_revision, int client_patch)
        {
            this.seed = BitConverter.GetBytes(seed);
            this.client_major = BitConverter.GetBytes(client_major);
            this.client_minor = BitConverter.GetBytes(client_minor);
            this.client_revision = BitConverter.GetBytes(client_revision);
            this.client_patch = BitConverter.GetBytes(client_patch);
        }

        public override byte[] GetBytes()
        {
            byte[] bytes = new byte[21];
            bytes[0] = cmd;
            Buffer.BlockCopy(seed, 0, bytes, 1, 4);
            Buffer.BlockCopy(client_major, 0, bytes, 5, 4);
            Buffer.BlockCopy(client_minor, 0, bytes, 9, 4);
            Buffer.BlockCopy(client_revision, 0, bytes, 13, 4);
            Buffer.BlockCopy(client_patch, 0, bytes, 17, 4);

            return bytes;
        }
    }
}

