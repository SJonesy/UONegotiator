// Most of this is stolen straight from UOProxy

using System;
using System.Collections.Generic;
using System.Text;

namespace UONegotiator
{
    public static class Decompression
    {

        public struct uo_decompression
        {
            public int bit;
            public int treepos;
            public int mask;
            public byte value;
        };
    }
}