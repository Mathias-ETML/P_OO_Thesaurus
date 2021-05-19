using System;
using Microsoft.Win32.SafeHandles;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.IO;

namespace P_Thesaurus.Models.WIN32
{
    /// <summary>
    /// FileAPI class
    /// 
    /// This was made by Mathias R
    /// 
    /// Translating c++ into c#
    /// Getting code from microsoft
    /// and from various other forums.
    /// 
    /// Source and documentation are included.
    /// </summary>
    public static class FileAPI
    {
        /// <summary>
        /// FileTimeToDateTime function
        /// </summary>
        /// <param name="fileTime">FILE_TIME</param>
        /// <returns>DateTime</returns>
        public static DateTime FileTimeToDateTime(ref FILE_TIME fileTime)
        {
            SYSTEM_TIME sysTime = new SYSTEM_TIME();

            if (FileAPI.FileTimeToSystemTime(ref fileTime, ref sysTime))
            {
                return new DateTime(sysTime.wYear, sysTime.wMonth, sysTime.wDay, sysTime.wHour, sysTime.wMinute, sysTime.wSecond, sysTime.wMilliseconds);
            }

            return new DateTime(-1);
        }

        /// <summary>
        /// Create safe file handle using safe create file function
        /// </summary>
        /// <param name="path">path</param>
        /// <returns>SafeFileHandle</returns>
        public static SafeFileHandle CreateFileShortcut(string path)
        {
            // https://stackoverflow.com/questions/2371204/how-to-get-directory-information-via-windows-native-api
            return FileAPI.SafeCreateFile(path, FileAPI.GENERIC_READ, FileShare.Read, null, FileMode.Open, FileAPI.FILE_ATTRIBUTE_NORMAL | FileAPI.FILE_FLAG_BACKUP_SEMANTICS, IntPtr.Zero);
        }

        /// <summary>
        /// FILE_INFO_BY_HANDLE_ENUM enum
        /// </summary>
        public enum FILE_INFO_BY_HANDLE_ENUM {
            FileBasicInfo = 0,
            FileStandardInfo,
            FileNameInfo,
            FileRenameInfo,
            FileDispositionInfo,
            FileAllocationInfo,
            FileEndOfFileInfo,
            FileStreamInfo,
            FileCompressionInfo,
            FileAttributeTagInfo,
            FileIdBothDirectoryInfo,
            FileIdBothDirectoryRestartInfo,
            FileIoPriorityHintInfo,
            FileRemoteProtocolInfo,
            FileFullDirectoryInfo,
            FileFullDirectoryRestartInfo,
            FileStorageInfo,
            FileAlignmentInfo,
            FileIdInfo,
            FileIdExtdDirectoryInfo,
            FileIdExtdDirectoryRestartInfo,
            FileDispositionInfoEx,
            FileRenameInfoEx,
            FileCaseSensitiveInfo,
            FileNormalizedNameInfo,
            MaximumFileInfoByHandleClass
        }

        /// <summary>
        /// kernel32 string shortage
        /// </summary>
        private const string KERNEL32 = "kernel32.dll";

        /// <summary>
        /// FILE_ATTRIBUTE_DIRECTORY field
        /// </summary>
        private const int FILE_TYPE_DISK = 0x0001;

        /// <summary>
        /// GENERIC_READ field
        /// </summary>
        public const uint GENERIC_READ = 0x80000000;

        /// <summary>
        /// FILE_ATTRIBUTE_NORMAL field
        /// </summary>
        public const uint FILE_ATTRIBUTE_NORMAL = 0x00000080;

        /// <summary>
        /// FILE_ATTRIBUTE_NORMAL field
        /// </summary>
        public const uint FILE_FLAG_BACKUP_SEMANTICS = 0x02000000;

        #region dll

        /// <summary>
        /// FindFirstFile
        /// 
        /// https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-findfirstfilea
        /// </summary>
        /// <param name="fileName">filename</param>
        /// <param name="data">WIN32_FIND_DATA pointer</param>
        /// <returns>SafeFileHandle</returns>
        [DllImport(KERNEL32, SetLastError = true, CharSet = CharSet.Unicode, BestFitMapping = false)]
        [ResourceExposure(ResourceScope.None)]
        [return: MarshalAs(UnmanagedType.AsAny)]
        public static extern global::Microsoft.Win32.SafeHandles.SafeFileHandle FindFirstFile(string fileName, ref WIN32_FIND_DATA data);

        /// <summary>
        /// Find next file
        /// 
        /// https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-findnextfilea
        /// </summary>
        /// <param name="hndFindFile">SafeFileHandle</param>
        /// <param name="lpFindFileData">WIN32_FIND_DATA</param>
        /// <returns>if next file was find</returns>
        [DllImport(KERNEL32, SetLastError = true, CharSet = CharSet.Unicode, BestFitMapping = false)]
        [ResourceExposure(ResourceScope.None)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FindNextFile(
                SafeFileHandle hndFindFile,
                ref WIN32_FIND_DATA lpFindFileData);

        /// <summary>
        /// FindClose
        /// 
        /// https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-findclose
        /// </summary>
        /// <param name="handle">handle</param>
        /// <returns>success</returns>
        [DllImport(KERNEL32)]
        [ResourceExposure(ResourceScope.None)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FindClose(IntPtr handle);

        /// <summary>
        /// GetFileType struct
        /// </summary>
        /// <param name="handle"></param>
        /// <returns>file type</returns>
        [DllImport(KERNEL32)]
        [ResourceExposure(ResourceScope.None)]
        [return: MarshalAs(UnmanagedType.U4)]
        public static extern int GetFileType(SafeFileHandle handle);

        /// <summary>
        /// Safe CreateFile
        /// 
        /// Copyright (c) Microsoft Corporation.  All rights reserved.
        /// https://referencesource.microsoft.com/#mscorlib/microsoft/win32/win32native.cs
        /// 
        /// https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-createfilea
        /// </summary>
        /// <param name="lpFileName">path</param>
        /// <param name="dwDesiredAccess">read, write, both or nothing</param>
        /// <param name="dwShareMode">read, write, both or nothing</param>
        /// <param name="securityAttrs">SECURITY_ATTRIBUTES struct</param>
        /// <param name="dwCreationDisposition">exist or not</param>
        /// <param name="dwFlagsAndAttributes">file type</param>
        /// <param name="hTemplateFile">valid handle</param>
        /// <returns>SafeFileHandle</returns>
        [System.Security.SecurityCritical]  // auto-generated
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        public static SafeFileHandle SafeCreateFile(String lpFileName,
                    uint dwDesiredAccess, System.IO.FileShare dwShareMode,
                    SECURITY_ATTRIBUTES securityAttrs, System.IO.FileMode dwCreationDisposition,
                    uint dwFlagsAndAttributes, IntPtr hTemplateFile)
        {
            SafeFileHandle handle = CreateFile(lpFileName, dwDesiredAccess, dwShareMode,
                                securityAttrs, dwCreationDisposition,
                                dwFlagsAndAttributes, hTemplateFile);

            if (!handle.IsInvalid)
            {
                int fileType = GetFileType(handle);
                if (fileType != FILE_TYPE_DISK)
                {
                    handle.Dispose();
                    throw new NotSupportedException("NotSupported_FileStreamOnNonFiles");
                }
            }

            return handle;
        }

        /// <summary>
        /// CreateFile
        /// 
        /// Copyright (c) Microsoft Corporation.  All rights reserved.
        /// https://referencesource.microsoft.com/#mscorlib/microsoft/win32/win32native.cs
        /// 
        /// https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-createfilea
        /// </summary>
        /// <param name="lpFileName">path</param>
        /// <param name="dwDesiredAccess">read, write, both or nothing</param>
        /// <param name="dwShareMode">read, write, both or nothing</param>
        /// <param name="securityAttrs">SECURITY_ATTRIBUTES struct</param>)
        /// <param name="dwCreationDisposition">exist or not</param>
        /// <param name="dwFlagsAndAttributes">file type</param>
        /// <param name="hTemplateFile">valid handle</param>
        /// <returns>SafeFileHandle</returns>
        [DllImport(KERNEL32, SetLastError = true, CharSet = CharSet.Auto, BestFitMapping = false)]
        [ResourceExposure(ResourceScope.Machine)]
        private static extern SafeFileHandle CreateFile(String lpFileName,
                    uint dwDesiredAccess, System.IO.FileShare dwShareMode,
                    SECURITY_ATTRIBUTES securityAttrs, System.IO.FileMode dwCreationDisposition,
                    uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        /// <summary>
        /// GetFileInformationByHandle
        /// 
        /// https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-getfileinformationbyhandle
        /// </summary>
        /// <param name="handle">handle</param>
        /// <param name="fileInformation">pointer file information</param>
        /// <returns>success</returns>
        [DllImport(KERNEL32, SetLastError = true, CharSet = CharSet.Auto, BestFitMapping = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetFileInformationByHandle(SafeFileHandle handle,
                                                             ref LPBY_HANDLE_FILE_INFORMATION fileInformation);

        /// <summary>
        /// FileTimeToSystemTime
        /// 
        /// https://docs.microsoft.com/en-us/windows/win32/api/timezoneapi/nf-timezoneapi-filetimetosystemtime
        /// </summary>
        /// <param name="lpFileTime">lpFileTime struct</param>
        /// <param name="lpSystemTime">lpSystemTime struct</param>
        /// <returns>success</returns>
        [DllImport(KERNEL32, SetLastError = true, CharSet = CharSet.Auto, BestFitMapping = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FileTimeToSystemTime(ref FILE_TIME lpFileTime,
                                                       ref SYSTEM_TIME lpSystemTime);
        #endregion dll
    }
}
