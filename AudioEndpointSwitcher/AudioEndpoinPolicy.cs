using System;
using System.Collections.Generic;
using Common.Win32.Com.Audio;
using Common.Win32.Com.Property;

namespace AudioEndpointSwitcher
{
    internal class CAudioEndpointInfo
    {
        public CAudioEndpointInfo(string id, string friendlyName)
        {
            Id = id;
            FriendlyName = friendlyName;
        }

        public CAudioEndpointInfo(IMMDevice device)
        {
            Id = device.GetId();

            IPropertyStore propertyStore = device.OpenPropertyStore(EStgm.Read);
            SPropVariant friendlyName = propertyStore.GetValue(PropKeys.DeviceFriendlyName);
            try
            {
                FriendlyName = (string) friendlyName.Data;
            }
            finally
            {
                friendlyName.Clear();
            }
        }

        public string Id { get; private set; }
        public string FriendlyName { get; private set; }
    }

    internal class CAudioEndpointsPolicy
    {
        private readonly IMMDeviceEnumerator _deviceEnumerator = (IMMDeviceEnumerator) new MMDeviceEnumerator();
        private readonly IPolicyConfig _policyConfig = (IPolicyConfig) new CPolicyConfigClient();


        public CAudioEndpointInfo[] Endpoints
        {
            get
            {
                IMMDeviceCollection devices = _deviceEnumerator.EnumAudioEndpoints(EDataFlow.Render, DeviceState.Active);
                uint deviceCount = devices.GetCount();

                var endPoints = new CAudioEndpointInfo[deviceCount];
                for (uint i = 0; i != deviceCount; ++i)
                    endPoints[i] = new CAudioEndpointInfo(devices.Item(i));
                return endPoints;          
            }
        }

        public CAudioEndpointInfo Default
        {
            get
            {
                return new CAudioEndpointInfo(_deviceEnumerator.GetDefaultAudioEndpoint(EDataFlow.Render, ERole.Console));
            }
            set
            {
                _policyConfig.SetDefaultEndpoint(value.Id, ERole.Console);
            }
        }
    }
}
