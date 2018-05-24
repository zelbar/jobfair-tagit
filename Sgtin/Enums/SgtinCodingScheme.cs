using System;

namespace JobFair.Tagit.Sgtin.Enums
{
    public enum SgtinCodingScheme
    {
        Sgtin64 = (byte) 0x08,
        Sgtin96 = (byte) 0x30,
        Sgtin198 = (byte) 0x36
    }
}