using System;
using System.Runtime.InteropServices;


namespace Common.Win32.Com.Property
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SPropertyKey
    {
        public Guid fmtid;
        public uint pid;
    }

    public static class PropKeys
    {
        public static readonly SPropertyKey DeviceFriendlyName = new SPropertyKey { fmtid = new Guid(0xa45c254e, 0xdf1c, 0x4efd, 0x80, 0x20, 0x67, 0xd1, 0x46, 0xa8, 0x50, 0xe0), pid = 14 };
    }
}
