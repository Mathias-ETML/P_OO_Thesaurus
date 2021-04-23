using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_Thesaurus.AppBusiness.HistoryReader
{
    /// <summary>
    /// FtpHistoryEntry class
    /// </summary>
    public class FtpHistoryEntry : HistoryEntry
    {
        /// <summary>
        /// Username field
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password field
        /// </summary>
        public string Password { get; set; }
    }
}
