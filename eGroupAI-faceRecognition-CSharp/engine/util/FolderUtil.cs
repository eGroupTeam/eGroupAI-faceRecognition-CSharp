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
