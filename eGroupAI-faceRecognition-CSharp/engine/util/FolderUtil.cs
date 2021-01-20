using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.util
{
    public class FolderUtil
    {
        public List<String> listName(DirectoryInfo folder)
        {
            List<String> fileNameList = new List<String>();
            if (folder.Exists)
            {
                // init variable
                FileInfo[] fileNames = folder.GetFiles();

                foreach (DirectoryInfo dir in folder.GetDirectories())
                {
                    fileNameList.AddRange(listName(dir));
                }

                foreach (FileInfo file in fileNames)
                {
                    fileNameList.Add(file.Name);
                }
            }
            return fileNameList;
        }

        public List<FileInfo> listFile(DirectoryInfo folder)
        {
            List<FileInfo> fileNameList = new List<FileInfo>();
            if (folder.Exists)
            {
                foreach (DirectoryInfo dir in folder.GetDirectories())
                {
                    fileNameList.AddRange(listFile(dir));
                }
                // init variable
                FileInfo[] fileNames = folder.GetFiles();

                foreach (FileInfo file in fileNames)
                {
                    fileNameList.Add(file);
                }
            }
            return fileNameList;
        }

        public List<String> listPath(DirectoryInfo folder)
        {
            List<String> fileNameList = new List<String>();
            if (folder.Exists)
            {
                // init variable
                FileInfo[] fileNames = folder.GetFiles();

                foreach (DirectoryInfo dir in folder.GetDirectories())
                {
                    fileNameList.AddRange(listPath(dir));
                }

                foreach (FileInfo file in fileNames)
                {
                    {
                        fileNameList.Add(file.FullName);
                    }
                }
            }
            return fileNameList;
        }

        public Boolean checkEmpty(String folderPath)
        {
            //FileInfo file = new FileInfo(folderPath);
            if (Directory.Exists(folderPath) && Directory.GetFiles(folderPath).Length > 0)
            {
                Console.WriteLine("Directory '" + folderPath+ "' is not empty!");
                return false;
            }
            return true;
        }

        public Boolean clean(String folderPath)
        {
            //FileInfo file = new FileInfo(folderPath);
            if (Directory.Exists(folderPath) && Directory.GetFiles(folderPath).Length > 0)
            {
                Console.WriteLine("Directory '" + folderPath + "' is not empty!");
                return false;
            }
            return true;
        }
    }
}
