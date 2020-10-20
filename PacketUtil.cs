using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UONegotiator.UOPacket;

namespace UONegotiator
{
    public static class CMD
    {
        public const byte SEED = 0xEF;
        public const byte SERVER_LIST = 0xA8;
        public const byte SUPPORTED_FEATURES = 0xB9;
        public const byte LOGIN_REQUEST = 0x80;
        public const byte SELECT_SERVER = 0xA0;
        public const byte CONNECT = 0x8C;
    }

    public enum PacketAction
    {
        DROP,
        FORWARD
    }

    public static class PacketUtil
    {
        public static UOPacket.BaseUOPacket GetPacket(List<byte> bytes)
        {
            byte cmd = bytes[0];

            BaseUOPacket incomingPacket;

            switch (cmd)
            {
                case CMD.SERVER_LIST:
                    incomingPacket = new UOPacket.ServerList(bytes);
                    break;
                case CMD.SUPPORTED_FEATURES:
                    incomingPacket = new UOPacket.SupportedFeatures(bytes);
                    break;
                case CMD.SEED:
                    incomingPacket = new UOPacket.Seed(bytes);
                    break;
                case CMD.LOGIN_REQUEST:
                    incomingPacket = new UOPacket.LoginRequest(bytes);
                    break;
                case CMD.SELECT_SERVER:
                    incomingPacket = new UOPacket.SelectServer(bytes);
                    break;
                case CMD.CONNECT:
                    incomingPacket = new UOPacket.Connect(bytes);
                    break;
                default:
                    incomingPacket = new UOPacket.UnknownPacket(bytes.ToArray());
                    break;
            }

            return incomingPacket;
        }

        public static int GetSize(byte cmd)
        {
            switch (cmd)
            {
                case CMD.SERVER_LIST:
                    return UOPacket.ServerList.s_Size;
                case CMD.SUPPORTED_FEATURES:
                    return UOPacket.SupportedFeatures.s_Size;
                case CMD.SEED:
                    return UOPacket.Seed.s_Size;
                case CMD.LOGIN_REQUEST:
                    return UOPacket.LoginRequest.s_Size;
                case CMD.SELECT_SERVER:
                    return UOPacket.SelectServer.s_Size;
                case CMD.CONNECT:
                    return UOPacket.Connect.s_Size;
            }

            return 0;
        }
    }
}
