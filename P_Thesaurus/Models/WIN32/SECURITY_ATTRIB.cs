using System.Runtime.InteropServices;

namespace P_Thesaurus.Models.WIN32
{
    /// <summary>
    /// SECURITY_ATTRIBUTES class
    /// 
    /// Copyright (c) Microsoft Corporation.  All rights reserved.
    /// https://referencesource.microsoft.com/#mscorlib/microsoft/win32/win32native.cs
    /// 
    /// https://docs.microsoft.com/en-us/previous-versions/windows/desktop/legacy/aa379560(v=vs.85)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class SECURITY_ATTRIBUTES
    {
        internal int nLength = 0;
        // don't remove null, or this field will disappear in bcl.small
        internal unsafe byte* pSecurityDescriptor = null;
        internal int bInheritHandle = 0;
    }
}
