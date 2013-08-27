using System;
using System.Runtime.InteropServices;
using Common.Win32.Com.Property;

namespace Common.Win32.Com.Audio
{
    [Guid("D666063F-1587-4E43-81F1-B948E807363F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMDevice
    {
        int Activate(ref Guid iid, uint dwClsCtx, ref SPropVariant pActivationParams, out IntPtr ppInterface);
        int OpenPropertyStore(EStgm stgmAccess, out IPropertyStore ppProperties);
        int GetId([MarshalAs(UnmanagedType.LPWStr)] out string ppstrId);
        int GetState(out DeviceState pdwState);
    }

    public static class MMDeviceExtensions
    {
        public static IPropertyStore OpenPropertyStore(this IMMDevice mmDevice, EStgm access)
        {
            IPropertyStore propertyStore;
            mmDevice.OpenPropertyStore(access, out propertyStore);
            return propertyStore;
        }

        public static string GetId(this IMMDevice mmDevice)
        {
            string id;
            mmDevice.GetId(out id);
            return id;
        }

        public static DeviceState GetState(this IMMDevice mmDevice)
        {
            DeviceState state;
            mmDevice.GetState(out state);
            return state;
        }

    }
}
