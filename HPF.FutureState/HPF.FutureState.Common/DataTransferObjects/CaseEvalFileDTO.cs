using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class CaseEvalFileDTO:BaseDTO
    {
        public int? CaseEvalFileId { get; set; }
        public int? CaseEvalHeaderId { get; set; }
        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set {_fileName = string.IsNullOrEmpty(value)?null:value;}
        }
        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = string.IsNullOrEmpty(value) ? null : value; }
        }
    }
}
