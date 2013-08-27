using System;
using System.Runtime.InteropServices;

namespace Common.Win32.Com.Audio
{
    [Guid("0BD7A1BE-7A1A-44DB-8397-CC5392387B5E"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMDeviceCollection
    {
        int GetCount(out uint pcDevices);
        int Item(uint nDevice, out IMMDevice ppDevice);
    }

    public static class MMDeviceCollectionExtensions
    {
        public static uint GetCount(this IMMDeviceCollection deviceCollection)
        {
            uint count;
            deviceCollection.GetCount(out count);
            return count;
        }

        public static IMMDevice Item(this IMMDeviceCollection deviceCollection, uint index)
        {
            IMMDevice device;
            deviceCollection.Item(index, out device);
            return device;
        }
    }
}
