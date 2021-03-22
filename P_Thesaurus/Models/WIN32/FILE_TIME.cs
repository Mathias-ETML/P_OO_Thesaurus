using System.Runtime.InteropServices;

namespace P_Thesaurus.Models.WIN32
{
    /// <summary>
    /// FILE_TIME struct
    /// 
    /// Copyright (c) Microsoft Corporation.  All rights reserved.
    /// https://referencesource.microsoft.com/#mscorlib/microsoft/win32/win32native.cs
    /// 
    /// https://docs.microsoft.com/en-us/windows/win32/api/minwinbase/ns-minwinbase-filetime
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct FILE_TIME
    {
        internal uint ftTimeLow;
        internal uint ftTimeHigh;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileTime">fileTime ticks</param>
        public FILE_TIME(long fileTime)
        {
            ftTimeLow = (uint)fileTime;
            ftTimeHigh = (uint)(fileTime >> 32);
        }

        /// <summary>
        /// ToTicks function
        /// </summary>
        /// <returns>FILE_TIME to ticks</returns>
        public long ToTicks()
        {
            return ((long)ftTimeHigh << 32) + ftTimeLow;
        }
    }
}
