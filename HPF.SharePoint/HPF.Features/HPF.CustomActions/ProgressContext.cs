using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections;

namespace HPF.CustomActions
{
    public class ProgressContext
    {
        #region "const"
        public const string PROGRESS_DATA = "ProgressData = {";
        #endregion

        #region "members"
        private static string _progressUniqueIdentifier;
        private Hashtable _progressCounters = new Hashtable();
        private object _progressLock = new object();
        #endregion

        #region "constructor"
        private ProgressContext() { _progressUniqueIdentifier = Guid.NewGuid().ToString(); }
        #endregion

        #region "public"
        public static ProgressContext Current
        {
            get
            {
                ProgressContext progressContext = GetProgressContext(HttpContext.Current);
                if (progressContext == null)
                {
                    progressContext = SetProgressContext(HttpContext.Current);
                }
                return progressContext;
            }
        }

        public string this[string key]
        {
            get
            {
                lock (_progressLock)
                {
                    return _progressCounters[key] as string;
                }
            }
            set
            {
                lock (_progressLock)
                {
                    _progressCounters[key] = value;
                }
            }
        }

        public string SerializeToJSonObject()
        {
            StringBuilder script = new StringBuilder(PROGRESS_DATA);
            foreach(string key in _progressCounters.Keys)
            {
                script.AppendFormat("{0}:'{1}',", key, _progressCounters[key]);
            }
            if (_progressCounters.Count > 0)
                script.Length -= 1;
            else
                RemoveProgressContext();
            script.Append("};");
            return script.ToString();
        }
        public void RemoveProgressContext()
        {            
            HttpContext.Current.Application.Remove(ProgressContextIdentifier);
        }
        #endregion

        #region "Helpers"        
        private static ProgressContext GetProgressContext(HttpContext context)
        {
            return context.Application[ProgressContextIdentifier] 
                as ProgressContext;
        }        

        private static ProgressContext SetProgressContext(HttpContext context)
        {
            ProgressContext progressContext = new ProgressContext();
            context.Application[ProgressContextIdentifier] = progressContext;
            return progressContext;
        }

        private static string ProgressContextIdentifier
        {
            get { return "HPFProgressContext_" + _progressUniqueIdentifier; }
        }
        #endregion
    }
}
