using System;
using System.Runtime.InteropServices;

namespace Common.Win32.Com.Audio
{
    [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMDeviceEnumerator
    {
        int EnumAudioEndpoints(EDataFlow dataFlow, DeviceState dwStateMask, out IMMDeviceCollection ppDevices);
        int GetDefaultAudioEndpoint(EDataFlow dataFlow, ERole role, out IMMDevice ppEndpoint);
        int GetDevice(string pwstrId, out IMMDevice ppDevice);
        int RegisterEndpointNotificationCallback(IntPtr pClient);
        int UnregisterEndpointNotificationCallback(IntPtr pClient);
    }

    [ComImport, Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")]
    public class MMDeviceEnumerator
    {
    }

    public static class MMDeviceEnumeratorExtensions
    {
        public static IMMDeviceCollection EnumAudioEndpoints(this IMMDeviceEnumerator devEnum, EDataFlow dataFlow, DeviceState state)
        {
            IMMDeviceCollection devCollection;
            devEnum.EnumAudioEndpoints(dataFlow, state, out devCollection);
            return devCollection;
        }

        public static IMMDevice GetDefaultAudioEndpoint(this IMMDeviceEnumerator devEnum, EDataFlow dataFlow, ERole role)
        {
            IMMDevice device;
            devEnum.GetDefaultAudioEndpoint(dataFlow, role, out device);
            return device;
        }

        public static IMMDevice GetDevice(this IMMDeviceEnumerator devEnum, string id)
        {
            IMMDevice device;
            devEnum.GetDevice(id, out device);
            return device;
        }
    }
}