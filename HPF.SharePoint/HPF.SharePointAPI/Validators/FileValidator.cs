using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint.Utilities;
using HPF.SharePointAPI;

namespace HPF.SharePointAPI.Validators
{
    public static class FileValidator
    {
        public static void Validate(UploadFileInfo file)
        {
            CommonValidator.ArgumentNotNull(file, "file");
            IsLegalFileName(file.FileName);
            CommonValidator.ArgumentNotNull(file.InputStream, "file.InputStream");
        }

        public static void IsLegalFileName(string fileName)
        {
            CommonValidator.ArgumentNotNullAndEmpty(fileName, "fileName");
            for (int index = 0; index < fileName.Length; index++)
            {
                if (!SPUrlUtility.IsLegalCharInUrl(fileName[index]))
                {
                    throw new ArgumentException("FileName is not legal");
                }
            }
        }
    }
}
