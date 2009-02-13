using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;

namespace HPF.SharePointAPI
{
    /// <summary>
    /// Represent an upload file
    /// </summary>
    public class UploadFileInfo
    {
        #region "Private Properties"
        private long _contentLength;
        private string _contentType;
        private string _fileName;
        private Stream _inputStream;

        private string _checkInComment;
        public bool _overwriteIfExists;
        #endregion

        #region "Public Properties"
        public long ContentLength
        {
            get { return _contentLength; }
            set { _contentLength = value; }
        }

        public string ContentType
        {
            get { return _contentType; }
            set { _contentType = value; }
        }

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public Stream InputStream
        {
            get { return _inputStream; }
            set { _inputStream = value; }
        }

        public string CheckInComment
        {
            get { return _checkInComment; }
            set { _checkInComment = value; }
        }

        public bool OverwriteIfExists
        {
            get { return _overwriteIfExists; }
            set { _overwriteIfExists = value; }
        }
        #endregion

        #region "Constructors"
        public UploadFileInfo()
        {
        }

        public UploadFileInfo(int contentLength, string contentType, 
            string fileName, Stream inputStream, string checkInComment, bool overwriteIfExists)
        {
            _contentLength = contentLength;
            _contentType = contentType;
            _fileName = fileName;
            _inputStream = inputStream;
            _checkInComment = checkInComment;
            _overwriteIfExists = overwriteIfExists;
        }

        public UploadFileInfo(HttpPostedFile postedFile, string checkInComment, bool overwriteIfExists)
        {
            _contentLength = postedFile.ContentLength;
            _contentType = postedFile.ContentType;
            _fileName = Path.GetFileName(postedFile.FileName);
            _inputStream = postedFile.InputStream;
            _checkInComment = checkInComment;
            _overwriteIfExists = overwriteIfExists;
        }

        public UploadFileInfo(string physicalFilePath, string checkInComment, bool overwriteIfExists)
        {
            FileInfo f = new FileInfo(physicalFilePath);            
            _contentLength = f.Length;
            _contentType = f.Extension;
            _fileName = Path.GetFileName(physicalFilePath);            
            _inputStream = f.OpenRead();
            _checkInComment = checkInComment;
            _overwriteIfExists = overwriteIfExists;
        }
        #endregion
    }
}
