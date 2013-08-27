using System;
using System.Runtime.InteropServices;

namespace Common.Win32.Com.Property
{
    [Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPropertyStore
    {
        int GetCount(out uint cProps);
        int GetAt(uint iProp, out SPropertyKey pkey);
        int GetValue(ref SPropertyKey key, out SPropVariant pv);
        int SetValue(ref SPropertyKey key, ref SPropVariant propvar);
        int Commit();
    }

    public static class PropertyStoreExtensions
    {
        public static uint GetCount(this IPropertyStore propertyStore)
        {
            uint retVal;
            propertyStore.GetCount(out retVal);
            return retVal;
        }

        public static SPropVariant GetValue(this IPropertyStore propertyStore, SPropertyKey key)
        {
            SPropVariant retVal;
            propertyStore.GetValue(ref key, out retVal);
            return retVal;
        }

        public static void SetValue(this IPropertyStore propertyStore, SPropertyKey key, SPropVariant val)
        {
            propertyStore.SetValue(ref key, ref val);
        }
    }
}
