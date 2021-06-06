using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Win32.SafeHandles;
using P_Thesaurus.Models.WIN32;

namespace P_Thesaurus.AppBusiness.WIN32
{
    /// <summary>
    /// Folder class
    /// </summary>
    public class Folder : FolderObject
    {
        /// <summary>
        /// Get you a root folder
        /// 
        /// Only use is for folders like "C:\"
        /// </summary>
        /// <param name="letter">letter of the drive</param>
        /// <returns>root folder</returns>
        public static Folder GetRootFolder(char letter)
        {
            letter = char.ToUpper(letter);

            if (!Char.IsLetter(letter))
            {
                throw new ArgumentException("letter only can be a letter");
            }

            // path setting
            string path = $"{letter}" + ":";

            // get handle for folder
            SafeFileHandle safeFileHandle = FileAPI.CreateFileShortcut(path);

            LPBY_HANDLE_FILE_INFORMATION info = new LPBY_HANDLE_FILE_INFORMATION();

            // get info for folder
            FileAPI.GetFileInformationByHandle(safeFileHandle, ref info);

            // folder creation
            Folder folder = new Folder(null, path, info);

            folder._type = Type.Root;

            // re init because split diid not happen like excpeted in ObjectFolder ctor
            // this is ok because we "only" can call this 26 time, optimisation is not required
            folder.Name = $"{letter}:";

            return folder;
        }

        /// <summary>
        /// Get you a folder from a location
        /// </summary>
        /// <param name="path">path</param>
        /// <returns>just the folder, not the full tree</returns>
        public static Folder GetFolder(string path)
        {
            // get folder
            SafeFileHandle safeFileHandle = FileAPI.CreateFileShortcut(path);

            LPBY_HANDLE_FILE_INFORMATION info = new LPBY_HANDLE_FILE_INFORMATION();

            // get infos about folder
            FileAPI.GetFileInformationByHandle(safeFileHandle, ref info);

            // create object
            Folder folder = new Folder(null, path, info);

            return folder;
        }

        /// <summary>
        /// Enum type
        /// </summary>
        public enum Type : byte
        {
            Normal = 0,
            Root = 1 // for example, C:\ is a root directory
        }

        /// <summary>
        /// Parent folder field
        /// 
        /// Useful if you want to go back in the tree
        /// </summary>
        private Folder _parentFolder;

        /// <summary>
        /// Type field
        /// 
        /// Usualy always normal
        /// </summary>
        private Type _type;

        /// <summary>
        /// Folders list
        /// 
        /// Folders in this folder
        /// </summary>
        private List<Folder> _folders;

        /// <summary>
        /// Files field
        /// 
        /// Files in this folder
        /// </summary>
        private List<File> _files;

        /// <summary>
        /// scanned field
        /// </summary>
        private bool _scanned = false;

        /// <summary>
        /// Folders Property
        /// </summary>
        public List<Folder> Folders { get => _folders; set => _folders = value; }

        /// <summary>
        /// Files Property
        /// </summary>
        public List<File> Files { get => _files; set => _files = value; }

        /// <summary>
        /// Scanned field
        /// </summary>
        public bool Scanned { get => _scanned; set => _scanned = value; }

        /// <summary>
        /// Get all folder and files in the folder
        /// </summary>
        public List<FolderObject> FolderObjectsList
        {
            // a basic way to create a list of evrything the folder have
            get
            {
                List<FolderObject> flo = new List<FolderObject>();

                foreach (Folder item in Folders)
                {
                    flo.Add(item);
                }

                foreach (File item in Files)
                {
                    flo.Add(item);
                }

                return flo;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<FolderObject> FolderObjects
        {
            // a basic way to get all the folder content for a foreach loop
            get
            {
                foreach (Folder item in Folders)
                {
                    yield return item;
                }

                foreach (File item in Files)
                {
                    yield return item;
                }
            }
        }


        /// <summary>
        /// ParentFolder Property
        /// </summary>
        public Folder ParentFolder { get => _parentFolder; set => _parentFolder = value; }

        /// <summary>
        /// Type Property
        /// </summary>
        public Type FolderType { get => _type; }

        /// <summary>
        /// IsRootFolder Property
        /// </summary>
        public bool IsRootFolder { get => _type == Type.Root; }

        /// <summary>
        /// Default constructor
        /// 
        /// use for root folder creation
        /// </summary>
        private Folder(string path) : base (path)
        {
            this._folders = new List<Folder>();
            this._files = new List<File>();
        }

        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="parentFolder">parent folder</param>
        /// <param name="data">data</param>
        public Folder(Folder parentFolder, WIN32_FIND_DATA data) : base(data)
        {
            this._parentFolder = parentFolder;
            this._type = Type.Normal;
            this.ObjectPath = _parentFolder.ObjectPath + "\\" + this._data.FileName;

            this.Text = this.Name;

            this._folders = new List<Folder>();
            this._files = new List<File>();
        }

        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="parentFolder">parentFolder</param>
        /// <param name="filename">filename</param>
        /// <param name="info">LPBY_HANDLE_FILE_INFORMATION struct</param>
        public Folder(Folder parentFolder, string path, LPBY_HANDLE_FILE_INFORMATION info) : base(path, info)
        {
            this._parentFolder = parentFolder;
            this._type = Type.Normal;

            this._folders = new List<Folder>();
            this._files = new List<File>();

            this.ObjectPath = path;
        }
    }
}
