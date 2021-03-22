using System;
using System.Collections.Generic;

using P_Thesaurus.Models.HistoryReader;

namespace P_Thesaurus.AppBusiness.HistoryReader
{
    /// <summary>
    /// IHistory interface
    /// </summary>
    /// <typeparam name="T">type of object</typeparam>
    public interface IHistory<T> : IDisposable
    {
        /// <summary>
        /// History field
        /// </summary>
        List<T> History { get; set; }

        /// <summary>
        /// Reader field
        /// </summary>
        JsonReader<List<T>> Reader { get; set; }

        /// <summary>
        /// AddEntry function
        /// </summary>
        /// <param name="entry">entry</param>
        void AddEntry(T entry);
    }
}
