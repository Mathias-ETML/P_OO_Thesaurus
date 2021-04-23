using System;
using P_Thesaurus.Models.JSON;

namespace P_Thesaurus.Models.HistoryReader
{
    /// <summary>
    /// JsonReader class
    /// </summary>
    /// <typeparam name="T">type</typeparam>
    public class JsonReader<T> : JsonReaderBase, IDisposable
    {
        /// <summary>
        /// path field
        /// </summary>
        private string _path;

        /// <summary>
        /// item field (readed object)
        /// </summary>
        private T _item;

        /// <summary>
        /// readed field
        /// </summary>
        private bool _readed = false;

        /// <summary>
        /// disposedValue field
        /// </summary>
        private bool _disposedValue = false;


        /// <summary>
        /// custom constructor
        /// </summary>
        /// <param name="path">path</param>
        public JsonReader(string path)
        {
            this._path = path;
        }

        /// <summary>
        /// Read function
        /// </summary>
        /// <returns>readed json file</returns>
        public T Read()
        {
            // check if we readed the file
            if (!_readed)
            {
                this._item = DeserializeFile<T>(this._path);
                this._readed = true;
            }

            return _item;
        }

        /// <summary>
        /// Reload function
        /// </summary>
        /// <returns>readed json file</returns>
        public T Reload()
        {
            this._readed = false;

            return Read();
        }

        /// <summary>
        /// Write in file
        /// </summary>
        /// <returns>success</returns>
        public bool Write(T item)
        {
            this._item = item;
            return SerializeFile<T>(item, _path);
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

                }

                _path = null;

                _disposedValue = true;
            }
        }

        /// <summary>
        /// JsonReader destructor
        /// </summary>
        ~JsonReader()
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
