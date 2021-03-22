using System;
using System.Runtime.InteropServices;


namespace P_Thesaurus.Models.WIN32
{
    /// <summary>
    /// LPBY_HANDLE_FILE_INFORMATION struct
    /// made by me :)
    /// 
    /// https://docs.microsoft.com/en-us/windows/win32/api/fileapi/ns-fileapi-by_handle_file_information
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    [BestFitMapping(false)]
    public struct LPBY_HANDLE_FILE_INFORMATION
    {
        internal int dwFileAttributes;
        internal FILE_TIME ftCreationTime;
        internal FILE_TIME ftLastAccessTime;
        internal FILE_TIME ftLastWriteTime;
        internal int dwVolumeSerialNumber;
        internal int nFileSizeHigh;
        internal int nFileSizeLow;
        internal int nNumberOfLinks;
        internal int nFileIndexHigh;
        internal int nFileIndexLow;
    }
}
