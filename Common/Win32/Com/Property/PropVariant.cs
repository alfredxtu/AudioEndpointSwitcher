using System;
using System.Runtime.InteropServices;

namespace Common.Win32.Com.Property
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SPropVariant
    {
        private EVarType vt;
        private short wReserved1;
        private short wReserved2;
        private short wReserved3;
        private IntPtr data1;
        private uint data2;

        public static SPropVariant Create()
        {
            SPropVariant retVal = new SPropVariant();
            retVal.vt = EVarType.Empty;
            retVal.wReserved1 = 0;
            retVal.wReserved2 = 0;
            retVal.wReserved3 = 0;
            retVal.data1 = IntPtr.Zero;
            retVal.data2 = 0;
            return retVal;
        }

        private byte[] DataRaw
        {
            get
            {
                byte[] retVal = new byte[IntPtr.Size + sizeof(uint)];
                switch (IntPtr.Size)
                {
                    case 4:     BitConverter.GetBytes((int)data1).CopyTo(retVal, 0); break;
                    case 8:     BitConverter.GetBytes((long)data1).CopyTo(retVal, 0); break;
                    default:    throw new NotSupportedException();
                }

                BitConverter.GetBytes(data2).CopyTo(retVal, IntPtr.Size);
                return retVal;
            }
        }

        public void Clear()
        {
            PropVariantClear(ref this);
        }

        public EVarType VarType { get { return vt; } }

        public object Data
        {
            get
            {
                switch (VarType)
                {
                    case EVarType.Bstr:             return Marshal.PtrToStringAnsi(data1);
                    case EVarType.Lpstr:            return Marshal.PtrToStringAnsi(data1);
                    case EVarType.Lpwstr:           return Marshal.PtrToStringUni(data1);
                    case EVarType.Empty:
                    case EVarType.Null:             throw new NullReferenceException();
                    case EVarType.I2:
                    case EVarType.I4:
                    case EVarType.R4:
                    case EVarType.R8:
                    case EVarType.Cy:
                    case EVarType.Date:
                    case EVarType.Dispatch:
                    case EVarType.Error:
                    case EVarType.Bool:
                    case EVarType.Variant:
                    case EVarType.Unknown:
                    case EVarType.Decimal:
                    case EVarType.I1:
                    case EVarType.Ui1:
                    case EVarType.Ui2:
                    case EVarType.Ui4:
                    case EVarType.I8:
                    case EVarType.Ui8:
                    case EVarType.Int:
                    case EVarType.Uint:
                    case EVarType.Void:
                    case EVarType.Hresult:
                    case EVarType.Ptr:
                    case EVarType.Safearray:
                    case EVarType.Carray:
                    case EVarType.Userdefined:
                    case EVarType.Record:
                    case EVarType.IntPtr:
                    case EVarType.UintPtr:
                    case EVarType.Filetime:
                    case EVarType.Blob:
                    case EVarType.Stream:
                    case EVarType.Storage:
                    case EVarType.StreamedObject:
                    case EVarType.StoredObject:
                    case EVarType.BlobObject:
                    case EVarType.Cf:
                    case EVarType.Clsid:
                    case EVarType.VersionedStream:
                    case EVarType.BstrBlob:
                    case EVarType.Vector:
                    case EVarType.Array:
                    case EVarType.Byref:
                        throw new NotImplementedException(String.Format("PROPVARIANT: type {0}.", VarType));
                    default:
                        throw new Exception(String.Format("PROPVARIANT: Unexpected type {0}.", VarType));
                }
            }
        }

        [DllImport("ole32.dll")]
        private extern static int PropVariantClear(ref SPropVariant pvar);
    }
}
