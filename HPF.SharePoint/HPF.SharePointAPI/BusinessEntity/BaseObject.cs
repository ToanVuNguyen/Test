using System;
using System.Collections.Generic;
using System.Text;

namespace HPF.SharePointAPI.BusinessEntity
{
    public class BaseObject
    {
        private string _name;        
        private byte[] _file;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public byte[] File
        {
            get { return _file; }
            set { _file = value; }
        }

        public BaseObject() { }
        public BaseObject(string name, byte[] file)
        {
            _name = name;            
            _file = file;
        }
    }
}
