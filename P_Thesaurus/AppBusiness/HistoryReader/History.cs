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
    public class History<T> : IDisposable
    {
        /// <summary>
        /// maximum entry field
        /// </summary>
        public const int MAXIMUM_ENTRY_COUNT = 25;

        /// <summary>
        /// reader field
        /// </summary>
        private JsonReader<List<T>> _reader;

        /// <summary>
        /// entrie field
        /// </summary>
        private List<T> _entries;

        /// <summary>
        /// Reload on write function
        /// </summary>
        private bool _reloadOnWrite;

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
            this._reloadOnWrite = false;
        }

        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="file">path to the file</param>
        /// <param name="reloadOnWrite">reload when you write in the json reader</param>
        public History(string file, bool reloadOnWrite)
        {
            this._reader = new JsonReader<List<T>>(file);
            this._entries = new List<T>();
            this._reloadOnWrite = reloadOnWrite;
        }

        /// <summary>
        /// AddEntry function
        /// </summary>
        /// <param name="entry">entry</param>
        public void AddEntry(T entry)
        {
            // check if we need to cascade list
            if (_entries.Count >= MAXIMUM_ENTRY_COUNT)
            {
                this._entries = new List<T>(_entries.GetRange(1, 24));
            }

            this._entries.Add(entry);

            if (_reloadOnWrite)
            {
                Write();
            }
        }

        /// <summary>
        /// Read function
        /// </summary>
        /// <returns>list of elements</returns>
        public List<T> Read()
        {
            this._entries = _reader.Reload();

            return this._entries;
        }

        /// <summary>
        /// Write function
        /// </summary>
        public bool Write()
        {
            bool success = _reader.Write(_entries);

            if (success)
            {
                _entries = Reader.Reload();
            }

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
