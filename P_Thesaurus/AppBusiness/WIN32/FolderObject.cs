using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Microsoft.Win32.SafeHandles;
using P_Thesaurus.Models.WIN32;

namespace P_Thesaurus.AppBusiness.WIN32
{
    /// <summary>
    /// Folder object class
    /// </summary>
    public class FolderObject : TreeNode
    {
        /// <summary>
        /// Struct for the folder data
        /// </summary>
        public struct Data
        {
            public string FileName;
            public string Path;
            public int Size;
            public DateTime CreateTime;
            public DateTime AccessTime;
            public DateTime ModifyTime;
        }

        /// <summary>
        /// Data field
        /// </summary>
        protected Data _data;

        /// <summary>
        /// FolderData Property
        /// </summary>
        public Data FolderData { get => _data; }

        /// <summary>
        /// Path Property
        /// </summary>
        public string Path { get => _data.Path; set => _data.Path = value; }

        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="data">FileAPI.WIN32_FIND_DATA struct</param>
        public FolderObject(WIN32_FIND_DATA data)
        {
            _data = new Data()
            {
                FileName = data.cFileName,
                Size = data.nFileSizeLow + data.nFileSizeHigh,
                CreateTime = FileAPI.FileTimeToDateTime(ref data.ftCreationTime),
                AccessTime = FileAPI.FileTimeToDateTime(ref data.ftLastAccessTime),
                ModifyTime = FileAPI.FileTimeToDateTime(ref data.ftLastWriteTime)
            };

            this.Name = data.cFileName;

            this.Text = data.cFileName;
        }

        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="path">path</param>
        /// <param name="info">LPBY_HANDLE_FILE_INFORMATION struct</param>
        public FolderObject(string path, LPBY_HANDLE_FILE_INFORMATION info)
        {
            string[] paths = path.Split(System.IO.Path.DirectorySeparatorChar);

            _data = new Data
            {
                FileName = paths[paths.Length - 1],
                Path = path,
                Size = info.nFileSizeHigh,
                CreateTime = FileAPI.FileTimeToDateTime(ref info.ftCreationTime),
                AccessTime = FileAPI.FileTimeToDateTime(ref info.ftLastAccessTime),
                ModifyTime = FileAPI.FileTimeToDateTime(ref info.ftLastWriteTime)
            };

            this.Name = _data.FileName;

            this.Text = _data.FileName;
        }

        /// <summary>
        /// Default protected constructor
        /// </summary>
        protected FolderObject(string path)
        {
            _data = new Data
            {
                Path = path
            };
        }
    }
}
