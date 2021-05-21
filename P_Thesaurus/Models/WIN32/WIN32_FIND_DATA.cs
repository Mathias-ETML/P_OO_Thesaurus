using System;
using System.Runtime.InteropServices;

namespace P_Thesaurus.Models.WIN32
{
    /// <summary>
    /// WIN32_FIND_DATA struct
    /// 
    /// Copyright (c) Microsoft Corporation.  All rights reserved.
    /// https://referencesource.microsoft.com/#mscorlib/microsoft/win32/win32native.cs
    /// 
    /// https://docs.microsoft.com/en-us/windows/win32/api/minwinbase/ns-minwinbase-win32_find_dataa
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    [BestFitMapping(false)]
    public unsafe struct WIN32_FIND_DATA
    {
        /// <summary>
        /// FILE_ATTRIBUTE_DIRECTORY field
        /// </summary>
        private const int FILE_ATTRIBUTE_DIRECTORY = 0x00000010;

        internal int dwFileAttributes;
        [NonSerialized]
        internal FILE_TIME ftCreationTime;
        [NonSerialized]
        internal FILE_TIME ftLastAccessTime;
        [NonSerialized]
        internal FILE_TIME ftLastWriteTime;
        internal int nFileSizeHigh;
        internal int nFileSizeLow;

        // If the file attributes' reparse point flag is set, then
        // dwReserved0 is the file tag (aka reparse tag) for the 
        // reparse point.  Use this to figure out whether something is
        // a volume mount point or a symbolic link.
        internal int dwReserved0;
        internal int dwReserved1;

        [NonSerialized]
        private fixed char _cFileName[260];

        // We never use this, don't expose it to avoid accidentally allocating strings
        [NonSerialized]
        private fixed char _cAlternateFileName[14];

        internal string cFileName { get { fixed (char* c = _cFileName) return new string(c); } }

        /// <summary>
        /// Every directory enumeration returns "." and ".." and we don't want them.
        /// Use this to avoid allocating for this simple check.
        /// </summary>
        internal bool IsRelativeDirectory
        {
            get
            {
                fixed (char* c = _cFileName)
                {
                    char first = c[0];
                    if (first != '.')
                        return false;
                    char second = c[1];
                    return (second == '\0' || (second == '.' && c[2] == '\0'));
                }
            }
        }

        /// <summary>
        /// True if this represents a file.
        /// </summary>
        internal bool IsFile => (dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY) == 0;

        /// <summary>
        /// True if this is a directory and NOT one of the special "." and ".." directories.
        /// </summary>
        internal bool IsNormalDirectory => ((dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY) != 0) && !IsRelativeDirectory;
    }
}
