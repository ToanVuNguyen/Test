using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.Threading;

namespace HPF.CustomActions
{
    public delegate void UpdateProgressStatus(double percentage);
    public static class ZipUtilities
    {
        // Methods
        private static ArrayList GenerateFileList(string Dir)
        {
            ArrayList list = new ArrayList();
            bool flag = true;
            foreach (string str in Directory.GetFiles(Dir))
            {
                list.Add(str);
                flag = false;
            }
            if (flag && (Directory.GetDirectories(Dir).Length == 0))
            {
                list.Add(Dir + "/");
            }
            foreach (string str2 in Directory.GetDirectories(Dir))
            {
                foreach (object obj2 in GenerateFileList(str2))
                {
                    list.Add(obj2);
                }
            }
            return list;
        }

        public static void UnZipFiles(string zipPathAndFile, string outputFolder, string password, bool deleteZipFile)
        {
            ZipEntry entry;
            ZipInputStream stream = new ZipInputStream(File.OpenRead(zipPathAndFile));
            if ((password != null) && (password != string.Empty))
            {
                stream.Password = password;
            }
            while ((entry = stream.GetNextEntry()) != null)
            {
                string path = outputFolder;
                string fileName = Path.GetFileName(entry.Name);
                if (path != "")
                {
                    Directory.CreateDirectory(path);
                }
                if ((fileName != string.Empty) && (entry.Name.IndexOf(".ini") < 0))
                {
                    string str3 = (path + @"\" + entry.Name).Replace(@"\ ", @"\");
                    string directoryName = Path.GetDirectoryName(str3);
                    if (!Directory.Exists(directoryName))
                    {
                        Directory.CreateDirectory(directoryName);
                    }
                    FileStream stream2 = File.Create(str3);
                    int count = 0x800;
                    byte[] buffer = new byte[0x800];
                    while (true)
                    {
                        count = stream.Read(buffer, 0, buffer.Length);
                        if (count <= 0)
                        {
                            break;
                        }
                        stream2.Write(buffer, 0, count);
                    }
                    stream2.Close();
                }
            }
            stream.Close();
            if (deleteZipFile)
            {
                File.Delete(zipPathAndFile);
            }
        }

        public static void ZipFiles(string inputFolderPath, string outputPathAndFile, string password, uint rowLimit, UpdateProgressStatus updateProgressAction)
        {
            ArrayList list = GenerateFileList(inputFolderPath);
            int count = Directory.GetParent(inputFolderPath).ToString().Length + 1;
            ZipOutputStream stream2 = new ZipOutputStream(File.Create(inputFolderPath + @"\" + outputPathAndFile));
            if ((password != null) && (password != string.Empty))
            {
                stream2.Password = password;
            }
            stream2.SetLevel(9);

            int index = 0;
            foreach (string str2 in list)
            {
                ZipEntry entry = new ZipEntry(str2.Remove(0, count));
                stream2.PutNextEntry(entry);
                if (!str2.EndsWith("/"))
                {
                    FileStream stream = File.OpenRead(str2);
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    stream2.Write(buffer, 0, buffer.Length);
                }
                if (++index % rowLimit == 0) Thread.Sleep(500);
                updateProgressAction(20 / list.Count);
            }
            stream2.Finish();
            stream2.Close();
        }
    }
}
