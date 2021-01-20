using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.util
{
    public class CopyUtil
    {
        public Boolean copyFile(FileInfo source, FileInfo dest)
        {
            if (source.Exists && source.Length > 0)
            {
                File.Copy(source.FullName, dest.FullName, true);
                return true;
                // StreamReader input = null;
                //     StreamWriter output = null;
                //try {
                //	input = new StreamReader(source.FullName);
                //     output = new StreamWriter(dest.FullName);
                //     byte[] buf = new byte[1024];
                //     int bytesRead;
                //	while ((bytesRead = input.Read(buf,)) > 0) {
                //		output.write(buf, 0, bytesRead);
                //	}
                //	//System.out.println("File copied from " + source + "  to " + dest);
                //	return true;
                //} catch (Exception e) {
                //	Console.WriteLine(e);
                //} finally {
                //	if (input != null)
                //		input.close();
                //	if (output != null)
                //		output.close();
                //}
            }
            return false;
        }

        /**
         * copy folder
         * @param source folder be copy
         * @param dest folder copy to
         * @throws IOException
         */
        public void copyFolder(string source, string dest)
        {
            if (Directory.Exists(source))
            {

                if (!Directory.Exists(dest))
                {
                    Directory.CreateDirectory(dest);
                    Console.WriteLine("Directory copied from " + source + "  to " + dest);
                }

                foreach (string f in Directory.GetFiles(source))
                {
                    File.Copy(f, dest + "\\" + f.Split('\\').Last());
                }
                //final String files[] = source.list();
                //File srcFile = null;
                //File destFile = null;
                //for (String file : files)
                //{
                //    /**
                //     * construct the src and dest file structure by adding the current folder
                //     */
                //    srcFile = new File(source, file);
                //    destFile = new File(dest, file);
                //    /** recursive copy the files or sub-folders */
                //    copyFolder(srcFile, destFile);
                //}
            }
            else
            {
                copyFile(new FileInfo(source), new FileInfo(dest));
            }
        }

    }
}
