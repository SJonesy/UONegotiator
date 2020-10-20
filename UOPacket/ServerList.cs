/*
http://docs.polserver.com/packets/index.php?Packet=0xA8

Packet Name: Game Server List
Last Modified: 2009-03-02 04:52:25
Modified By: Turley

Packet: 0xA8
Sent By: Server
Size: Variable

Packet Build
BYTE[1] cmd
BYTE[2] length
BYTE[1] System Info Flag
BYTE[2] # of servers

Then each server --
BYTE[2] server index (0-based)
BYTE[32] server name
BYTE percent full
BYTE timezone
BYTE[4] server ip to ping

Subcommand Build
N/A


Notes
System Info Flags: 0xCC - Do not send video card info. 0x64 - Send Video card. RunUO And SteamEngine both send 0x5D.

Server IP has to be sent in reverse order. For example, 192.168.0.1 is sent as 0100A8C0.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace UONegotiator.UOPacket
{
    public class ServerList : BaseUOPacket
    {
        public static int s_Size = 46;

        public new byte cmd = CMD.SERVER_LIST;

        private List<byte> length;
        private byte systemInfoFlag;
        private List<byte> numberOfServers;
        private ServerListItem gameServer;

        public ServerList(List<byte> bytes)
        {
            Debug.Assert(bytes[0] == CMD.SERVER_LIST);
            length          = bytes.GetRange(1, 2);
            systemInfoFlag  = bytes[3];
            numberOfServers = bytes.GetRange(4, 2);
            gameServer      = new ServerListItem(bytes.GetRange(6, 40).ToArray());
        }

        public override byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.Add(cmd);
            bytes.AddRange(length);
            bytes.Add(systemInfoFlag);
            bytes.AddRange(numberOfServers);
            bytes.AddRange(gameServer.GetBytes());
            
            return bytes.ToArray();
        }

        public override PacketAction OnReceiveFromServer() 
        { 
            return PacketAction.FORWARD; 
        }

        private class ServerListItem
        {
            private byte[] serverIndex;
            private byte[] serverName;
            private byte full;
            private byte timezone;
            private byte[] address;

            public ServerListItem (byte[] bytes)
            {
                serverIndex = new byte[2];
                serverName = new byte[32];
                address = new byte[4];

                Buffer.BlockCopy(bytes, 0, serverIndex, 0, 2);
                Buffer.BlockCopy(bytes, 2, serverName, 0, 32);
                full = bytes[35];
                timezone = bytes[36];
                // TODO this needs to come from a config or something,
                // this is just the interger representation of 192.168.86.249 
                // (my iP) which gets correctly stored as 249 86 168 192
                address = BitConverter.GetBytes(3232257785);
            }

            public byte[] GetBytes()
            {
                List<byte> bytes = new List<byte>();
                bytes.AddRange(serverIndex);
                bytes.AddRange(serverName);
                bytes.Add(full);
                bytes.Add(timezone);
                bytes.AddRange(address);

                return bytes.ToArray();
            }
        }
    }
}

