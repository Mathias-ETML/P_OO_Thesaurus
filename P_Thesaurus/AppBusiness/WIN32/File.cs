using P_Thesaurus.Models.WIN32;

namespace P_Thesaurus.AppBusiness.WIN32
{
    public class File : FolderObject
    {
        /// <summary>
        /// Enum type
        /// </summary>
        public enum Type : byte
        {
            None = 0, // ok
            Text, // ok
            Office, // ok
            Image, // ok
            Music, // ok
            Video, // ok
            Compressed, // ok
            Executable, // ok
            Code, // ok
            Shortcut, // ok
            System, // ok
            Other // ok
        }

        /// <summary>
        /// _extension field
        /// </summary>
        private string _extension;

        /// <summary>
        /// _type field
        /// </summary>
        private Type _type;

        /// <summary>
        /// _parentFolder field
        /// </summary>
        private Folder _parentFolder;

        /// <summary>
        /// Custom constructor
        /// </summary>
        /// <param name="parentfolder">Compressed</param>
        /// <param name="data">data</param>
        public File(Folder parentfolder, WIN32_FIND_DATA data) : base(data)
        {
            this._parentFolder = parentfolder;

            if (_parentFolder.IsRootFolder)
            {
                this.Path = _parentFolder.Path + this._data.FileName;
            }
            else
            {
                this.Path = _parentFolder.Path + "\\" + this._data.FileName;
            }

            SetType();
        }

        /// <summary>
        /// set type
        /// </summary>
        protected void SetType()
        {
            string[] buffer = this.Name.Split('.');

            // check if we just have a FILE
            if (buffer.Length == 1)
            {
                this._type = Type.None;
                return;
            }

            // setting extension
            this._extension = '.' + buffer[buffer.Length - 1];

            // swiching
            switch (this._extension)
            {
                ////////////////////////////////////
                // Office
                case ".docx":
                case ".doc":
                case ".xlsx":
                case ".xls":
                case ".pptx":
                case ".ppt":
                case ".mdb":
                case ".accdb":
                    this._type = Type.Office;
                    break;

                ////////////////////////////////////
                // Text
                case ".txt":
                    this._type = Type.Text;
                    break;

                ////////////////////////////////////
                // Shortcut
                case ".lnk":
                    this._type = Type.Shortcut;
                    break;

                ////////////////////////////////////
                // System
                case ".sys":
                case ".dll":
                case ".tmp":
                case ".log":
                    this._type = Type.System;
                    break;

                ////////////////////////////////////
                // Executable
                case ".exe":
                    this._type = Type.Executable;
                    break;

                ////////////////////////////////////
                // Video
                case ".mp4":
                    this._type = Type.Video;
                    break;

                ////////////////////////////////////
                // Music
                case ".wav":
                case ".mp3":
                    this._type = Type.Music;
                    break;                
                    
                ////////////////////////////////////
                // Image
                case ".png":
                case ".jpg":
                case ".jpeg":
                case ".psd":
                case ".pdf":
                case ".svg":
                case ".bmp":
                    this._type = Type.Image;
                    break;

                ////////////////////////////////////
                // Compressed
                case ".zip":
                case ".7z":
                case ".rar":
                case ".tar":
                case ".iso":
                    this._type = Type.Compressed;
                    break;

                ////////////////////////////////////
                // Code
                case ".c":
                case ".cs":
                case ".cpp":
                case ".py":
                case ".java":
                case ".h":
                case ".hp":
                case ".hpp":

                case ".ts":
                case ".js":
                case ".html":
                case ".php":
                case ".css":

                case ".f":
                case ".fs":
                case ".asm":
                case ".s":
                case ".vb":
                case ".csh":
                case ".m":
                case ".mm":
                case ".C":
                case ".rb":
                case ".rbw":
                    this._type = Type.Code;
                    break;

                // else no idea
                default:
                    this._type = Type.Other;
                    break;
            }
        }
    }
}
