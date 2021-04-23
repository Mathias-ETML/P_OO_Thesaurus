using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_Thesaurus.AppBusiness.HistoryReader
{
    /// <summary>
    /// ISecurityCritical interface
    /// </summary>
    public interface ISecurityCritical
    {
        /// <summary>
        /// Password field
        /// </summary>
        string Password { get; set; }
    }
}
