using System;
using System.Collections.Generic;
using System.Text;
using UONegotiator.UOPacket;

namespace UONegotiator
{
    public static class CMD
    {
        public const byte SEED = 0xEF;
        public const byte SERVER_LIST = 0xA8;
    }

    public enum PacketAction
    {
        DROP,
        FORWARD
    }

    public static class PacketUtil
    {
        public static UOPacket.BaseUOPacket GetPacket(byte[] bytes)
        {
            byte cmd = bytes[0];

            switch (cmd)
            {
                case CMD.SERVER_LIST:
                    return new UOPacket.ServerList(bytes);
            }

            return new UOPacket.UnknownPacket(bytes);
        }
    }
}
