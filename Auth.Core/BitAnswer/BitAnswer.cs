/*
 *
 * BitAnswer Client Library class
 *
 * BitAnswer Ltd. (C) 2009 - ?. All rights reserved.
 */
using System;
using System.Text;
using System.Runtime.InteropServices;

namespace SH3H.WAP.Auth.Core
{
    public class BitAnswerException : Exception
    {
        public int ErrorCode { get; set; }

        public BitAnswerException(int status)
        {
            ErrorCode = status;
        }

        public override string Message
        {
            get
            {
                return "ErrorCode: 0x" + Convert.ToString(ErrorCode, 16);
            }
        }
    }

    public enum BitAnswerExceptionCode
    {
        BIT_SUCCESS = 0,
        BIT_ERR_BUFFER_SMALL = 0x104
    }

    public enum LoginMode
    {
        Local = 1,
        Remote = 2,
        Auto = 3,
        AutoCache = 7,
        Usb = 8
    }

    public enum BindingType
    {
        Existing = 0,
        Local = 1,
        UsbStorage = 2
    }

    public enum SessionType
    {
        XML_TYPE_SN_INFO = 3,
        XML_TYPE_FEATURE_INFO = 4,

        BIT_ADDRESS = 0x101,
        BIT_SYS_TIME = 0x201,
        BIT_CONTROL_TYPE = 0x302,
        BIT_VOL_NUM = 0x303,
        BIT_START_DATE = 0x304,
        BIT_END_DATE = 0x305,
        BIT_EXPIRATION_DAYS = 0x306,
        BIT_USAGE_NUM = 0x307,
        BIT_CONSUMED_USAGE_NUM = 0x308,
        BIT_CONCURRENT_NUM = 0x309,
        BIT_ACTIVATE_DATE = 0x30A,
        BIT_USER_LIMIT = 0x30B,
        BIT_LAST_REMOTE_ACCESS_DATE = 0x30C,
        BIT_MAX_OFFLINE_MINUTES = 0x30D,
        BIT_NEXT_CONNECT_DATE = 0x30E
    }

    public enum InfoType
    {
        BIT_LIST_SRV_ADDR = 0,
        BIT_LIST_LOCAL_SN_INFO = 1,
        BIT_LIST_LOCAL_SN_FEATURE_INFO = 2,
        BIT_LIST_LOCAL_SN_LIC_INFO = 3,
        BIT_LIST_UPDATE_ERROR = 4
    }

    public struct BIT_DATE_TIME
    {
        public UInt16 year;
        public Byte month;
        public Byte dayOfMonth;
        public Byte hour;
        public Byte minute;
        public Byte second;
        public Byte unused;
    }

    public enum FEATURE_TYPE
    {
        BIT_FEATURE_CONVERT = 0x03,
        BIT_FEATURE_READ_ONLY = 0x04,
        BIT_FEATURE_READ_WRITE = 0x05,
        BIT_FEATURE_CRYPTION = 0x09,
        BIT_FEATURE_USER = 0x0a,
        BIT_FEATURE_UNIFIED = 0x0b,
    }

    public unsafe struct FEATURE_INFO
    {
        public UInt32 featureId;
        public FEATURE_TYPE type;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public Byte[] featureName;
        public BIT_DATE_TIME endDateTime;
        public UInt32 expirationDays;
        public UInt32 users;
    }

    public interface BitAnswerInterface
    {
        int Login(string url, string sn, int mode);
        int LoginEx(string url, string sn, uint featureId, string xmlScope, int mode);
        int Logout();
        int ConvertFeature(uint featureId, uint para1, uint para2, uint para3, uint para4, ref uint result);
        int ReadFeature(uint featureId, ref uint featureValue);
        int WriteFeature(uint featureId, uint featureValue);
        int EncryptFeature(uint featureId, byte[] plainBuffer, byte[] cipherBuffer);
        int DecryptFeature(uint featureId, byte[] cipherBuffer, byte[] plainBuffer);
        int QueryFeature(uint featureId, ref uint capacity);
        int ReleaseFeature(uint featureId, ref uint capacity);
        int GetFeatureInfo(uint featureId, ref FEATURE_INFO featureInfo);
        int GetDataItemNum(ref uint number);
        int GetDataItemName(uint index, byte[] name, ref uint nameLen);
        int GetDataItem(string dataItemName, byte[] dataItemValue, ref uint dataItemValueLen);
        int SetDataItem(string dataItemName, byte[] dataItemValue);
        int RemoveDataItem(string dataItemName);
        int GetSessionInfo(SessionType type, byte[] sessionInfo, ref uint sessionInfoLen);
        int UpdateOnline(string url, string sn);
        int GetRequestInfo(string sn, uint bindingType, byte[] requestInfo, ref uint requestInfoSize);
        int GetUpdateInfo(string url, string sn, string requestInfo, byte[] updateInfo, ref uint updateInfoSize);
        int ApplyUpdateInfo(string updateInfo, byte[] receiptInfo, ref uint receiptInfoSize);
        int SetDbPath(string path);
        int GetProductPath(byte[] productPath, ref uint productPathSize);
        int Revoke(string url, string sn, byte[] revocationInfo, ref uint revocationInfoSize);
        int GetInfo(string sn, uint type, byte[] info, ref uint infoSize);
        int SetProxy(string hostName, uint port, string userId, string password);
        int SetLocalServer(string host, uint port, uint timeoutSeconds);
        int GetVersion(ref uint version);
        int RemoveSn(string sn);
        int BorrowOnline(string url, string sn, uint featureId, uint[] features, uint durationMinutes);
        int TestBitService(string url, string sn, uint featureId);
    }

    public class BitAnswerX86 : BitAnswerInterface
    {
        public const string BitAnswerDllName = "00002fd7_00000001";

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_Login")]
        static extern int Bit_Login(string url, string sn, byte[] applicationData, ref uint handle, int mode);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_LoginEx")]
        static extern int Bit_LoginEx(string url, string sn, uint featureId, string xmlScope, byte[] applicationData, ref uint handle, int mode);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_Logout")]
        static extern int Bit_Logout(uint handle);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_ConvertFeature")]
        static extern int Bit_ConvertFeature(uint handle, uint featureId, uint para1, uint para2, uint para3, uint para4, ref uint result);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_ReadFeature")]
        static extern int Bit_ReadFeature(uint handle, uint featureId, ref uint featureValue);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_WriteFeature")]
        static extern int Bit_WriteFeature(uint handle, uint featureId, uint featureValue);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_EncryptFeature")]
        static extern int Bit_EncryptFeature(uint handle, uint featureId, byte[] plainBuffer, byte[] cipherBuffer, uint bufferLen);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_DecryptFeature")]
        static extern int Bit_DecryptFeature(uint handle, uint featureId, byte[] cipherBuffer, byte[] plainBuffer, uint bufferLen);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_QueryFeature")]
        static extern int Bit_QueryFeature(uint handle, uint featureId, ref uint capacity);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_ReleaseFeature")]
        static extern int Bit_ReleaseFeature(uint handle, uint featureId, ref uint capacity);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_GetFeatureInfo")]
        static extern int Bit_GetFeatureInfo(uint handle, uint featureId, ref FEATURE_INFO featureInfo);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_GetDataItemNum")]
        static extern int Bit_GetDataItemNum(uint handle, ref uint number);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_GetDataItemName")]
        static extern int Bit_GetDataItemName(uint handle, uint index, byte[] dataItemName, ref uint DataItemNameLen);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_GetDataItem")]
        static extern int Bit_GetDataItem(uint handle, string dataItemName, byte[] dataItemValue, ref uint dataItemValueLen);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_SetDataItem")]
        static extern int Bit_SetDataItem(uint handle, string dataItemName, byte[] dataItemValue, uint dataItemValueLen);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_RemoveDataItem")]
        static extern int Bit_RemoveDataItem(uint handle, string dataItemName);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_GetSessionInfo")]
        static extern int Bit_GetSessionInfo(uint handle, uint type, byte[] sessionInfo, ref uint sessionInfoLen);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_GetRequestInfo")]
        static extern int Bit_GetRequestInfo(string sn, byte[] applicationData, uint bindingType, byte[] requestInfo, ref uint requestInfoSize);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_GetUpdateInfo")]
        static extern int Bit_GetUpdateInfo(string url, string sn, byte[] applicationData, string requestInfo, byte[] updateInfo, ref uint updateInfoSize);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_ApplyUpdateInfo")]
        static extern int Bit_ApplyUpdateInfo(byte[] applicationData, string updateInfo, byte[] receiptInfo, ref uint receiptInfoSize);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_UpdateOnline")]
        static extern int Bit_UpdateOnline(string url, string sn, byte[] applicationData);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_SetDbPath")]
        static extern int Bit_SetDbPath(string szPath);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_GetProductPath")]
        static extern int Bit_GetProductPath(byte[] applicationData, byte[] productPath, ref uint productPathSize);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_Revoke")]
        static extern int Bit_Revoke(string url, string sn, byte[] applicationData, byte[] revocationInfo, ref uint revocationInfoSize);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_GetInfo")]
        static extern int Bit_GetInfo(string sn, byte[] applicationData, uint type, byte[] info, ref uint infoSize);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_SetProxy")]
        static extern int Bit_SetProxy(byte[] applicationData, string host, uint port, string userId, string password);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_SetLocalServer")]
        static extern int Bit_SetLocalServer(byte[] applicationData, string host, uint port, uint timeoutSeconds);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_RemoveSn")]
        static extern int Bit_RemoveSn(string sn, byte[] applicationData);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_GetVersion")]
        static extern int Bit_GetVersion(ref uint version);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_BorrowOnline")]
        static extern int Bit_BorrowOnline(string url, string sn, uint featureId, byte[] applicationData, uint[] features, uint featuresSize, uint durationMinutes);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_TestBitService")]
        static extern int Bit_TestBitService(string url, string sn, uint featureId, byte[] applicationData);

        static byte[] applicationData = {
	        0x40,0x80,0xbc,0xf2,0x1a,0xd8,0x68,0xde,0xb0,0x60,0x57,0xbb,0xc8,0x30,0x8f,0xc2,
            0xab,0x2c,0x5f,0xb6,0xa9,0xd1,0x90,0xad,0xdb,0x19,0x2b,0x4e,0xb8,0x19,0x6c,0x5d,
            0xa7,0x01,0xd2,0xb8,0x94,0x93,0x66,0x9c,0x7c,0xb3,0x5e,0xe3,0x30,0xd5,0xec,0xd7,
            0xb5,0x02,0xac,0xa0,0x8c,0x95,0x5d,0xc4,0xaa,0xca,0x0e,0x7b,0x1e,0x57,0x5e,0x9d,
            0x98,0x11,0x9b,0xc6,0x35,0xcf,0x01,0x42,0x1c,0x89,0x9a,0x48,0x1e,0xa3,0xe1,0x9d,
            0x4b,0x31,0x70,0xe3,0xf6,0x44,0x89,0x57,0x81,0x75,0xa7,0xc0,0xc9,0xc9,0x46,0xd2,
            0xac,0x06,0xfd,0xdb,0xb8,0x85,0x70,0x7b,0xa1,0x52,0x5b,0xeb,0xd9,0x33,0x20,0x50,
            0x36,0x08,0x36,0x55,0x13,0x5a,0xd9,0xf4,0xe4,0xac,0x6a,0x98,0xab,0x47,0xbe,0x20,
            0xdf,0x4b,0xa4,0x07,0xb2,0x52,0xb0,0xf9,0x32,0x46,0x24,0xbb,0x3a,0x6a,0x0a,0x8a,
            0x79,0xa6,0xd3,0xb6,0x28,0xcb,0x4a,0x5b,0x75,0x47,0xcd,0x50,0xdd,0x03,0xeb,0x26,
            0xec,0x70,0x63,0x00,0x66,0x2d,0xd3,0x60,0x4a,0xee,0xb6,0x26,0x8e,0x3d,0xd3,0x68,
            0x8c,0x7f,0x03,0x4f,0xb6,0xcd,0x62,0xf9,0xc2,0x45,0x58,0xe3,0xc3,0xd4,0x6d
        };

        uint handle = 0;

        public int Login(string url, string sn, int mode)
        {
            return Bit_Login(url, sn, applicationData, ref handle, (int)mode);
        }

        public int LoginEx(string url, string sn, uint featureId, string xmlScope, int mode)
        {
            return Bit_LoginEx(url, sn, featureId, xmlScope, applicationData, ref handle, mode);
        }

        public int Logout()
        {
            return Bit_Logout(handle);
        }

        public int ConvertFeature(uint featureId, uint para1, uint para2, uint para3, uint para4, ref uint result)
        {
            return Bit_ConvertFeature(handle, featureId, para1, para2, para3, para4, ref result);
        }

        public int ReadFeature(uint featureId, ref uint featureValue)
        {
            return Bit_ReadFeature(handle, featureId, ref featureValue);
        }

        public int WriteFeature(uint featureId, uint featureValue)
        {
            return Bit_WriteFeature(handle, featureId, featureValue);
        }

        public int EncryptFeature(uint featureId, byte[] plainBuffer, byte[] cipherBuffer)
        {
            return Bit_EncryptFeature(handle, featureId, plainBuffer, cipherBuffer, (uint)plainBuffer.Length);
        }

        public int DecryptFeature(uint featureId, byte[] cipherBuffer, byte[] plainBuffer)
        {
            return Bit_DecryptFeature(handle, featureId, cipherBuffer, plainBuffer, (uint)cipherBuffer.Length);
        }

        public int QueryFeature(uint featureId, ref uint capacity)
        {
            return Bit_QueryFeature(handle, featureId, ref capacity);
        }

        public int ReleaseFeature(uint featureId, ref uint capacity)
        {
            return Bit_ReleaseFeature(handle, featureId, ref capacity);
        }

        public int GetFeatureInfo(uint featureId, ref FEATURE_INFO featureInfo)
        {
            return Bit_GetFeatureInfo(handle, featureId, ref featureInfo);
        }

        public int GetDataItemNum(ref uint number)
        {
            return Bit_GetDataItemNum(handle, ref number);
        }

        public int GetDataItemName(uint index, byte[] name, ref uint nameLen)
        {
            return Bit_GetDataItemName(handle, index, name, ref nameLen);
        }

        public int GetDataItem(string dataItemName, byte[] dataItemValue, ref uint dataItemValueLen)
        {
            return Bit_GetDataItem(handle, dataItemName, dataItemValue, ref dataItemValueLen);
        }

        public int SetDataItem(string dataItemName, byte[] dataItemValue)
        {
            return Bit_SetDataItem(handle, dataItemName, dataItemValue, (uint)dataItemValue.Length);
        }

        public int RemoveDataItem(string dataItemName)
        {
            return Bit_RemoveDataItem(handle, dataItemName);
        }

        public int GetSessionInfo(SessionType type, byte[] sessionInfo, ref uint sessionInfoLen)
        {
            return Bit_GetSessionInfo(handle, (uint)type, sessionInfo, ref sessionInfoLen);
        }

        public int UpdateOnline(string url, string sn)
        {
            return Bit_UpdateOnline(url, sn, applicationData);
        }

        public int GetRequestInfo(string sn, uint bindingType, byte[] requestInfo, ref uint requestInfoSize)
        {
            return Bit_GetRequestInfo(sn, applicationData, bindingType, requestInfo, ref requestInfoSize);
        }

        public int GetUpdateInfo(string url, string sn, string requestInfo, byte[] updateInfo, ref uint updateInfoSize)
        {
            return Bit_GetUpdateInfo(url, sn, applicationData, requestInfo, updateInfo, ref updateInfoSize);
        }

        public int ApplyUpdateInfo(string updateInfo, byte[] receiptInfo, ref uint receiptInfoSize)
        {
            return Bit_ApplyUpdateInfo(applicationData, updateInfo, receiptInfo, ref receiptInfoSize);
        }

        public int SetDbPath(string path)
        {
            return Bit_SetDbPath(path);
        }

        public int GetProductPath(byte[] productPath, ref uint productPathSize)
        {
            return Bit_GetProductPath(applicationData, productPath, ref productPathSize);
        }

        public int Revoke(string url, string sn, byte[] revocationInfo, ref uint revocationInfoSize)
        {
            return Bit_Revoke(url, sn, applicationData, revocationInfo, ref revocationInfoSize);
        }
        public int GetInfo(string sn, uint type, byte[] info, ref uint infoSize)
        {
            return Bit_GetInfo(sn, applicationData, type, info, ref infoSize);
        }

        public int SetProxy(string hostName, uint port, string userId, string password)
        {
            return Bit_SetProxy(applicationData, hostName, port, userId, password);
        }

        public int SetLocalServer(string host, uint port, uint timeoutSeconds)
        {
            return Bit_SetLocalServer(applicationData, host, port, timeoutSeconds);
        }

        public int RemoveSn(string sn)
        {
            return Bit_RemoveSn(sn, applicationData);
        }

        public int GetVersion(ref uint version)
        {
            return Bit_GetVersion(ref version);
        }

        public int BorrowOnline(string url, string sn, uint featureId, uint[] features, uint durationMinutes)
        {
            uint size = 0;
            if (features != null)
            {
                size = (uint)features.Length;
            }
            return Bit_BorrowOnline(url, sn, featureId, applicationData, features, size, durationMinutes);
        }

        public int TestBitService(string url, string sn, uint featureId)
        {
            return Bit_TestBitService(url, sn, featureId, applicationData);
        }
    }

    public class BitAnswerX64 : BitAnswerInterface
    {
        public const string BitAnswerDllName = "00002fd7_00000001_x64";

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_Login")]
        public static extern int Bit_Login(string url, string sn, byte[] applicationData, ref uint handle, int mode);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_LoginEx")]
        static extern int Bit_LoginEx(string url, string sn, uint featureId, string xmlScope, byte[] applicationData, ref uint handle, int mode);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_Logout")]
        static extern int Bit_Logout(uint handle);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_ConvertFeature")]
        static extern int Bit_ConvertFeature(uint handle, uint featureId, uint para1, uint para2, uint para3, uint para4, ref uint result);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_ReadFeature")]
        static extern int Bit_ReadFeature(uint handle, uint featureId, ref uint featureValue);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_WriteFeature")]
        static extern int Bit_WriteFeature(uint handle, uint featureId, uint featureValue);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_EncryptFeature")]
        static extern int Bit_EncryptFeature(uint handle, uint featureId, byte[] plainBuffer, byte[] cipherBuffer, uint bufferLen);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_DecryptFeature")]
        static extern int Bit_DecryptFeature(uint handle, uint featureId, byte[] plainBuffer, byte[] cipherBuffer, uint bufferLen);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_QueryFeature")]
        static extern int Bit_QueryFeature(uint handle, uint featureId, ref uint capacity);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_ReleaseFeature")]
        static extern int Bit_ReleaseFeature(uint handle, uint featureId, ref uint capacity);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_GetFeatureInfo")]
        static extern int Bit_GetFeatureInfo(uint handle, uint featureId, ref FEATURE_INFO featureInfo);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_GetDataItemNum")]
        static extern int Bit_GetDataItemNum(uint handle, ref uint number);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_GetDataItemName")]
        static extern int Bit_GetDataItemName(uint handle, uint index, byte[] dataItemName, ref uint DataItemNameLen);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_GetDataItem")]
        static extern int Bit_GetDataItem(uint handle, string dataItemName, byte[] dataItemValue, ref uint dataItemValueLen);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_SetDataItem")]
        static extern int Bit_SetDataItem(uint handle, string dataItemName, byte[] dataItemValue, uint dataItemValueLen);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_RemoveDataItem")]
        static extern int Bit_RemoveDataItem(uint handle, string dataItemName);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_GetSessionInfo")]
        static extern int Bit_GetSessionInfo(uint handle, uint type, byte[] sessionInfo, ref uint sessionInfoLen);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_GetRequestInfo")]
        static extern int Bit_GetRequestInfo(string sn, byte[] applicationData, uint bindingType, byte[] requestInfo, ref uint requestInfoSize);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_GetUpdateInfo")]
        static extern int Bit_GetUpdateInfo(string url, string sn, byte[] applicationData, string requestInfo, byte[] updateInfo, ref uint updateInfoSize);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_ApplyUpdateInfo")]
        static extern int Bit_ApplyUpdateInfo(byte[] applicationData, string updateInfo, byte[] receiptInfo, ref uint receiptInfoSize);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_UpdateOnline")]
        static extern int Bit_UpdateOnline(string url, string sn, byte[] applicationData);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_SetDbPath")]
        static extern int Bit_SetDbPath(string szPath);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_GetProductPath")]
        static extern int Bit_GetProductPath(byte[] applicationData, byte[] productPath, ref uint productPathSize);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_Revoke")]
        static extern int Bit_Revoke(string url, string sn, byte[] applicationData, byte[] revocationInfo, ref uint revocationInfoSize);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_GetInfo")]
        static extern int Bit_GetInfo(string sn, byte[] applicationData, uint type, byte[] info, ref uint infoSize);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_SetProxy")]
        static extern int Bit_SetProxy(byte[] applicationData, string hostName, uint port, string userId, string password);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_SetLocalServer")]
        static extern int Bit_SetLocalServer(byte[] applicationData, string host, uint port, uint timeoutSeconds);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_RemoveSn")]
        static extern int Bit_RemoveSn(string sn, byte[] applicationData);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_GetVersion")]
        static extern int Bit_GetVersion(ref uint version);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_BorrowOnline")]
        static extern int Bit_BorrowOnline(string url, string sn, uint featureId, byte[] applicationData, uint[] features, uint featuresSize, uint durationMinutes);

        [DllImport(BitAnswerDllName, EntryPoint = "Bit_TestBitService")]
        static extern int Bit_TestBitService(string url, string sn, uint featureId, byte[] applicationData);

        static byte[] applicationData = {
	        0x40,0x80,0xbc,0xf2,0x1a,0xd8,0x68,0xde,0xb0,0x60,0x57,0xbb,0xc8,0x30,0x8f,0xc2,
            0xab,0x2c,0x5f,0xb6,0xa9,0xd1,0x90,0xad,0xdb,0x19,0x2b,0x4e,0xb8,0x19,0x6c,0x5d,
            0xa7,0x01,0xd2,0xb8,0x94,0x93,0x66,0x9c,0x7c,0xb3,0x5e,0xe3,0x30,0xd5,0xec,0xd7,
            0xb5,0x02,0xac,0xa0,0x8c,0x95,0x5d,0xc4,0xaa,0xca,0x0e,0x7b,0x1e,0x57,0x5e,0x9d,
            0x98,0x11,0x9b,0xc6,0x35,0xcf,0x01,0x42,0x1c,0x89,0x9a,0x48,0x1e,0xa3,0xe1,0x9d,
            0x4b,0x31,0x70,0xe3,0xf6,0x44,0x89,0x57,0x81,0x75,0xa7,0xc0,0xc9,0xc9,0x46,0xd2,
            0xac,0x06,0xfd,0xdb,0xb8,0x85,0x70,0x7b,0xa1,0x52,0x5b,0xeb,0xd9,0x33,0x20,0x50,
            0x36,0x08,0x36,0x55,0x13,0x5a,0xd9,0xf4,0xe4,0xac,0x6a,0x98,0xab,0x47,0xbe,0x20,
            0xdf,0x4b,0xa4,0x07,0xb2,0x52,0xb0,0xf9,0x32,0x46,0x24,0xbb,0x3a,0x6a,0x0a,0x8a,
            0x79,0xa6,0xd3,0xb6,0x28,0xcb,0x4a,0x5b,0x75,0x47,0xcd,0x50,0xdd,0x03,0xeb,0x26,
            0xec,0x70,0x63,0x00,0x66,0x2d,0xd3,0x60,0x4a,0xee,0xb6,0x26,0x8e,0x3d,0xd3,0x68,
            0x8c,0x7f,0x03,0x4f,0xb6,0xcd,0x62,0xf9,0xc2,0x45,0x58,0xe3,0xc3,0xd4,0x6d
        };
        uint handle = 0;

        public int Login(string url, string sn, int mode)
        {
            return Bit_Login(url, sn, applicationData, ref handle, (int)mode);
        }

        public int LoginEx(string url, string sn, uint featureId, string xmlScope, int mode)
        {
            return Bit_LoginEx(url, sn, featureId, xmlScope, applicationData, ref handle, mode);
        }

        public int Logout()
        {
            return Bit_Logout(handle);
        }

        public int ConvertFeature(uint featureId, uint para1, uint para2, uint para3, uint para4, ref uint result)
        {
            return Bit_ConvertFeature(handle, featureId, para1, para2, para3, para4, ref result);
        }

        public int ReadFeature(uint featureId, ref uint featureValue)
        {
            return Bit_ReadFeature(handle, featureId, ref featureValue);
        }

        public int WriteFeature(uint featureId, uint featureValue)
        {
            return Bit_WriteFeature(handle, featureId, featureValue);
        }

        public int EncryptFeature(uint featureId, byte[] plainBuffer, byte[] cipherBuffer)
        {
            return Bit_EncryptFeature(handle, featureId, plainBuffer, cipherBuffer, (uint)plainBuffer.Length);
        }

        public int DecryptFeature(uint featureId, byte[] cipherBuffer, byte[] plainBuffer)
        {
            return Bit_DecryptFeature(handle, featureId, cipherBuffer, plainBuffer, (uint)cipherBuffer.Length);
        }

        public int QueryFeature(uint featureId, ref uint capacity)
        {
            return Bit_QueryFeature(handle, featureId, ref capacity);
        }

        public int ReleaseFeature(uint featureId, ref uint capacity)
        {
            return Bit_ReleaseFeature(handle, featureId, ref capacity);
        }

        public int GetFeatureInfo(uint featureId, ref FEATURE_INFO featureInfo)
        {
            return Bit_GetFeatureInfo(handle, featureId, ref featureInfo);
        }

        public int GetDataItemNum(ref uint number)
        {
            return Bit_GetDataItemNum(handle, ref number);
        }

        public int GetDataItemName(uint index, byte[] name, ref uint nameLen)
        {
            return Bit_GetDataItemName(handle, index, name, ref nameLen);
        }

        public int GetDataItem(string dataItemName, byte[] dataItemValue, ref uint dataItemValueLen)
        {
            return Bit_GetDataItem(handle, dataItemName, dataItemValue, ref dataItemValueLen);
        }

        public int SetDataItem(string dataItemName, byte[] dataItemValue)
        {
            return Bit_SetDataItem(handle, dataItemName, dataItemValue, (uint)dataItemValue.Length);
        }

        public int RemoveDataItem(string dataItemName)
        {
            return Bit_RemoveDataItem(handle, dataItemName);
        }

        public int GetSessionInfo(SessionType type, byte[] sessionInfo, ref uint sessionInfoLen)
        {
            return Bit_GetSessionInfo(handle, (uint)type, sessionInfo, ref sessionInfoLen);
        }

        public int UpdateOnline(string url, string sn)
        {
            return Bit_UpdateOnline(url, sn, applicationData);
        }

        public int GetRequestInfo(string sn, uint bindingType, byte[] requestInfo, ref uint requestInfoSize)
        {
            return Bit_GetRequestInfo(sn, applicationData, bindingType, requestInfo, ref requestInfoSize);
        }

        public int GetUpdateInfo(string url, string sn, string requestInfo, byte[] updateInfo, ref uint updateInfoSize)
        {
            return Bit_GetUpdateInfo(url, sn, applicationData, requestInfo, updateInfo, ref updateInfoSize);
        }

        public int ApplyUpdateInfo(string updateInfo, byte[] receiptInfo, ref uint receiptInfoSize)
        {
            return Bit_ApplyUpdateInfo(applicationData, updateInfo, receiptInfo, ref receiptInfoSize);
        }

        public int SetDbPath(string path)
        {
            return Bit_SetDbPath(path);
        }

        public int GetProductPath(byte[] productPath, ref uint productPathSize)
        {
            return Bit_GetProductPath(applicationData, productPath, ref productPathSize);
        }

        public int Revoke(string url, string sn, byte[] revocationInfo, ref uint revocationInfoSize)
        {
            return Bit_Revoke(url, sn, applicationData, revocationInfo, ref revocationInfoSize);
        }

        public int GetInfo(string sn, uint type, byte[] info, ref uint infoSize)
        {
            return Bit_GetInfo(sn, applicationData, type, info, ref infoSize);
        }

        public int SetProxy(string hostName, uint port, string userId, string password)
        {
            return Bit_SetProxy(applicationData, hostName, port, userId, password);
        }

        public int SetLocalServer(string host, uint port, uint timeoutSeconds)
        {
            return Bit_SetLocalServer(applicationData, host, port, timeoutSeconds);
        }

        public int RemoveSn(string sn)
        {
            return Bit_RemoveSn(sn, applicationData);
        }

        public int GetVersion(ref uint version)
        {
            return Bit_GetVersion(ref version);
        }

        public int BorrowOnline(string url, string sn, uint featureId, uint[] features, uint durationMinutes)
        {
            uint size = 0;
            if (features != null)
            {
                size = (uint)features.Length;
            }
            return Bit_BorrowOnline(url, sn, featureId, applicationData, features, size, durationMinutes);
        }

        public int TestBitService(string url, string sn, uint featureId)
        {
            return Bit_TestBitService(url, sn, featureId, applicationData);
        }
    }

    public class BitAnswer
    {
        public bool IsX64
        {
            get
            {
                return (IntPtr.Size == 8);
            }
        }

        BitAnswerInterface bitAnswer;
        public BitAnswer()
        {
            if (IsX64)
            {
                bitAnswer = new BitAnswerX64();
            }
            else
            {
                bitAnswer = new BitAnswerX86();
            }
        }

        public void Login(string url, string sn, LoginMode mode)
        {
            int status = bitAnswer.Login(url, sn, (int)mode);
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
        }

        public void LoginEx(string url, string sn, uint featureId, string xmlScope, LoginMode mode)
        {
            int status = bitAnswer.LoginEx(url, sn, featureId, xmlScope, (int)mode);
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
        }

        public void Logout()
        {
            int status = bitAnswer.Logout();
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
        }

        public uint ConvertFeature(uint featureId, uint para1, uint para2, uint para3, uint para4)
        {
            uint result = 0;
            int status = bitAnswer.ConvertFeature(featureId, para1, para2, para3, para4, ref result);
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
            return result;
        }

        public uint ReadFeature(uint featureId)
        {
            uint featureValue = 0;
            int status = bitAnswer.ReadFeature(featureId, ref featureValue);
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
            return featureValue;
        }

        public void WriteFeature(uint featureId, uint featureValue)
        {
            int status = bitAnswer.WriteFeature(featureId, featureValue);
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
        }

        public byte[] EncryptFeature(uint featureId, byte[] plainBuffer)
        {
            byte[] cipherBuffer = new byte[plainBuffer.Length];
            int status = bitAnswer.EncryptFeature(featureId, plainBuffer, cipherBuffer);
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
            return cipherBuffer;
        }

        public byte[] DecryptFeature(uint featureId, byte[] cipherBuffer)
        {
            byte[] plainBuffer = new byte[cipherBuffer.Length];
            int status = bitAnswer.DecryptFeature(featureId, cipherBuffer, plainBuffer);
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
            return plainBuffer;
        }

        public uint QueryFeature(uint featureId)
        {
            uint capacity = 0;
            int status = bitAnswer.QueryFeature(featureId, ref capacity);
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
            return capacity;
        }

        public uint ReleaseFeature(uint featureId)
        {
            uint capacity = 0;
            int status = bitAnswer.ReleaseFeature(featureId, ref capacity);
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
            return capacity;
        }

        public FEATURE_INFO GetFeatureInfo(uint featureId)
        {
            FEATURE_INFO featureInfo = new FEATURE_INFO();
            int status = bitAnswer.GetFeatureInfo(featureId, ref featureInfo);
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
            return featureInfo;
        }

        public uint GetDataItemNum()
        {
            uint num = 0;
            int status = bitAnswer.GetDataItemNum(ref num);
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
            return num;
        }

        public string GetDataItemName(uint index)
        {
            uint nameLen = 129;
            byte[] name = new byte[nameLen];
            int status = bitAnswer.GetDataItemName(index, name, ref nameLen);
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
            return Encoding.GetEncoding("gbk").GetString(name);
        }

        public byte[] GetDataItem(string dataItemName)
        {
            uint dataItemValueLen = 1024;
            byte[] dataItemValueTemp = new byte[dataItemValueLen];
            int status = bitAnswer.GetDataItem(dataItemName, dataItemValueTemp, ref dataItemValueLen);
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }

            byte[] dataItemValue = new byte[dataItemValueLen];
            Array.Copy(dataItemValueTemp, dataItemValue, dataItemValueLen);
            return dataItemValue;
        }

        public void SetDataItem(string dataItemName, byte[] dataItemValue)
        {
            int status = bitAnswer.SetDataItem(dataItemName, dataItemValue);
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
        }

        public void RemoveDataItem(string dataItemName)
        {
            int status = bitAnswer.RemoveDataItem(dataItemName);
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
        }

        public string GetSessionInfo(SessionType type)
        {
            uint xmlSessionInfoLen = 10240;
            byte[] xmlSessionInfo = new byte[xmlSessionInfoLen];
            int status = bitAnswer.GetSessionInfo(type, xmlSessionInfo, ref xmlSessionInfoLen);
            if (status == (int)BitAnswerExceptionCode.BIT_ERR_BUFFER_SMALL)
            {
                xmlSessionInfo = new byte[xmlSessionInfoLen];
                status = bitAnswer.GetSessionInfo(type, xmlSessionInfo, ref xmlSessionInfoLen);
            }
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
            return ASCIIEncoding.UTF8.GetString(xmlSessionInfo);
        }

        public string GetInfo(string sn, InfoType type)
        {
            uint infoLen = 10240;
            byte[] info = new byte[infoLen];
            int status = bitAnswer.GetInfo(sn, (uint)type, info, ref infoLen);
            while (status == (int)BitAnswerExceptionCode.BIT_ERR_BUFFER_SMALL)
            {
                infoLen += 10240;
                info = new byte[infoLen];
                status = bitAnswer.GetInfo(sn, (uint)type, info, ref infoLen);
            }
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
            return ASCIIEncoding.UTF8.GetString(info);
        }

        public void UpdateOnline(string url, string sn)
        {
            int status = bitAnswer.UpdateOnline(url, sn);
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
        }

        public string GetRequestInfo(string sn, BindingType bindingType)
        {
            uint requestInfoSize = 10240;
            byte[] requestInfo = new byte[requestInfoSize];
            int status = bitAnswer.GetRequestInfo(sn, (uint)bindingType, requestInfo, ref requestInfoSize);
            if (status == (int)BitAnswerExceptionCode.BIT_ERR_BUFFER_SMALL)
            {
                requestInfo = new byte[requestInfoSize];
                status = bitAnswer.GetRequestInfo(sn, (uint)bindingType, requestInfo, ref requestInfoSize);
            }
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
            return ASCIIEncoding.UTF8.GetString(requestInfo);
        }

        public string GetUpdateInfo(string url, string sn, string requestInfo)
        {
            uint updateInfoSize = 1024;
            byte[] updateInfo = new byte[updateInfoSize];
            int status = bitAnswer.GetUpdateInfo(url, sn, requestInfo, updateInfo, ref updateInfoSize);
            if (status == (int)BitAnswerExceptionCode.BIT_ERR_BUFFER_SMALL)
            {
                updateInfo = new byte[updateInfoSize];
                status = bitAnswer.GetUpdateInfo(url, sn, requestInfo, updateInfo, ref updateInfoSize);
            }
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
            return ASCIIEncoding.UTF8.GetString(updateInfo);
        }

        public string ApplyUpdateInfo(string updateInfo)
        {
            uint receiptInfoSize = 10240;
            byte[] receiptInfo = new byte[receiptInfoSize];
            int status = bitAnswer.ApplyUpdateInfo(updateInfo, receiptInfo, ref receiptInfoSize);
            if (status == (int)BitAnswerExceptionCode.BIT_ERR_BUFFER_SMALL)
            {
                receiptInfo = new byte[receiptInfoSize];
                status = bitAnswer.ApplyUpdateInfo(updateInfo, receiptInfo, ref receiptInfoSize);
            }
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
            return ASCIIEncoding.UTF8.GetString(receiptInfo);
        }

        public void SetDbPath(string path)
        {
            int status = bitAnswer.SetDbPath(path);
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
        }

        public String GetProductPath()
        {
            byte[] path = new byte[256];
            uint size = (uint)path.Length;
            int status = bitAnswer.GetProductPath(path, ref size);
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
            return ASCIIEncoding.UTF8.GetString(path);
        }

        public string Revoke(string sn)
        {
            uint revocationInfoSize = 10240;
            byte[] revocationInfo = new byte[revocationInfoSize];
            int status = bitAnswer.Revoke(null, sn, revocationInfo, ref revocationInfoSize);
            if (status == (int)BitAnswerExceptionCode.BIT_ERR_BUFFER_SMALL)
            {
                revocationInfo = new byte[revocationInfoSize];
                status = bitAnswer.Revoke(null, sn, revocationInfo, ref revocationInfoSize);
            }
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
            return ASCIIEncoding.UTF8.GetString(revocationInfo);
        }

        public void RevokeOnline(string url, string sn)
        {
            uint revocationInfoSize = 10240;
            int status = bitAnswer.Revoke(url, sn, null, ref revocationInfoSize);
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
        }

        public void SetProxy(string host, uint port, string userId, string password)
        {
            int status = bitAnswer.SetProxy(host, port, userId, password);
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
        }

        public void SetLocalServer(string host, uint port, uint timeoutSeconds)
        {
            int status = bitAnswer.SetLocalServer(host, port, timeoutSeconds);
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
        }

        public void RemoveSn(string sn)
        {
            int status = bitAnswer.RemoveSn(sn);
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
        }

        public uint GetVersion()
        {
            uint version = 0;
            int status = bitAnswer.GetVersion(ref version);
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
            return version;
        }

        public void BorrowOnline(string url, string sn, uint featureId, uint[] features, uint durationMinutes)
        {
            int status = bitAnswer.BorrowOnline(url, sn, featureId, features, durationMinutes);
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
        }

        public void TestBitService(string url, string sn, uint featureId)
        {
            int status = bitAnswer.TestBitService(url, sn, featureId);
            if (status != 0)
            {
                throw new BitAnswerException(status);
            }
        }
    }
}
