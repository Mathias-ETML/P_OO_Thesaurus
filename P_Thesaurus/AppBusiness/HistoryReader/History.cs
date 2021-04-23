using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P_Thesaurus.Models.HistoryReader;

namespace P_Thesaurus.AppBusiness.HistoryReader
{
    /// <summary>
    /// History class
    /// </summary>
    public class History<T>
    {
        /// <summary>
        /// reader field
        /// </summary>
        private JsonReader<List<T>> _reader;

        /// <summary>
        /// entrie field
        /// </summary>
        private List<T> _entries;

        /// <summary>
        /// disposed value
        /// </summary>
        private bool _disposedValue = false;

        /// <summary>
        /// Reader field
        /// </summary>
        public JsonReader<List<T>> Reader { get => _reader; }

        /// <summary>
        /// History field
        /// </summary>
        //List<T> IHistory<T>.History { get => _entries; }

        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="file">path to the file</param>
        public History(string file)
        {
            this._reader = new JsonReader<List<T>>(file);
            this._entries = new List<T>();
        }

        /// <summary>
        /// AddEntry function
        /// </summary>
        /// <param name="entry">entry</param>
        public void AddEntry(T entry)
        {
            this._entries.Add(entry);
        }

        /// <summary>
        /// Read function
        /// </summary>
        /// <returns>list of elements</returns>
        public List<T> Read()
        {
            return _reader.Read();
        }

        /// <summary>
        /// Write function
        /// </summary>
        public bool Write()
        {
            bool success = _reader.Write(_entries);
            _entries = Reader.Reload();

            return success;
        }

        #region IDisposable Support

        /// <summary>
        /// Dispose function
        /// </summary>
        /// <param name="disposing">disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _reader.Dispose();
                }

                _entries = null;

                _disposedValue = true;
            }
        }

        /// <summary>
        /// destructor
        /// </summary>
        ~History()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose function
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
