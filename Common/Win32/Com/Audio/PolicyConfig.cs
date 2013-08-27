using System;
using System.Runtime.InteropServices;
using Common.Win32.Com.Property;

namespace Common.Win32.Com.Audio
{
    [Guid("F8679F50-850A-41CF-9C72-430F290290C8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPolicyConfig
    {
        int GetMixFormat(string pszDeviceName, out IntPtr ppFormat);
        int GetDeviceFormat(string pszDeviceName, bool bDefault, out IntPtr ppFormat);
        int ResetDeviceFormat(string pszDeviceName);
        int SetDeviceFormat(string pszDeviceName, IntPtr pEndpointFormat, IntPtr MixFormat);
        int GetProcessingPeriod(string pszDeviceName, bool bDefault, IntPtr pmftDefaultPeriod, IntPtr pmftMinimumPeriod);
        int SetProcessingPeriod(string pszDeviceName, IntPtr pmftPeriod);
        int GetShareMode(string pszDeviceName, IntPtr pMode);
        int SetShareMode(string pszDeviceName, IntPtr mode);
        int GetPropertyValue(string pszDeviceName, ref SPropertyKey key, out SPropVariant pv);
        int SetPropertyValue(string pszDeviceName, ref SPropertyKey key, ref SPropVariant pv);
        int SetDefaultEndpoint(string pszDeviceName, ERole role);
        int SetEndpointVisibility(string pszDeviceName, bool bVisible);
    }

    [ComImport, Guid("870AF99C-171D-4F9E-AF0D-E63DF40C2BC9")]
    public class CPolicyConfigClient
    {
    }

    public static class PolicyConfigExtensions
    {
        public static void SetDefaultEndpoint(this IPolicyConfig policyConfig, string deviceName, ERole role = ERole.Console)
        {
            policyConfig.SetDefaultEndpoint(deviceName, role);
        }
    }
}
