using System;
using System.Runtime.InteropServices;

namespace P_Thesaurus.Models.WIN32
{
    /// <summary>
    /// SYSTEM_TIME Struct
    /// Made by me:)
    /// 
    /// https://docs.microsoft.com/en-us/windows/win32/api/minwinbase/ns-minwinbase-systemtime
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    [BestFitMapping(false)]
    public struct SYSTEM_TIME
    {
        internal ushort wYear;
        internal ushort wMonth;
        internal ushort wDayOfWeek;
        internal ushort wDay;
        internal ushort wHour;
        internal ushort wMinute;
        internal ushort wSecond;
        internal ushort wMilliseconds;
    }
}
