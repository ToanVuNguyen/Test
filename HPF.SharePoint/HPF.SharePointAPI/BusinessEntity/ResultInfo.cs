using System;
using System.Collections.Generic;
using System.Text;

namespace HPF.SharePointAPI.BusinessEntity
{
    /// <summary>
    /// Represent a result from upload and download
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultInfo<T>
    {
        private bool _successful;
        private Exception _error;
        private T _bizObject;

        public bool Successful
        {
            get { return _successful; }
            set { _successful = value; }
        }

        public Exception Error
        {
            get { return _error; }
            set { _error = value; }
        }

        public T BizObject
        {
            get { return _bizObject; }
            set { _bizObject = value; }
        }

        public ResultInfo()
        {
            _successful = true;
            _error = null;
            _bizObject = default(T);
        }
        public ResultInfo(bool successful, Exception error, T bizObject)
        {
            _successful = successful;
            _error = error;
            _bizObject = bizObject;
        }

        public ResultInfo(T bizObject)
        {
            _successful = true;
            _error = null;
            _bizObject = bizObject;
        }

        public ResultInfo(Exception error, T bizObject)
        {
            _successful = false;
            _error = error;
            _bizObject = bizObject;
        }
    }
}
