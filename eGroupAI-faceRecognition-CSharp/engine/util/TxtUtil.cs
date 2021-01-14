using eGroupAI_faceRecognition_CSharp.library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.util
{
    public class TxtUtil
    {
        //private Logger LOGGER = LoggerFactory.getLogger(CmdUtil.class);

        public class Charsets
        {
            public static String BIG5 { get { return "Big5"; } }
            public static String UTF8 { get { return "UTF-8"; } }
            public static String ISO_8859_1 { get { return "ISO_8859_1"; } }

            private String value;

            Charsets(String value)
            {
                this.value = value;
            }

            public String getValue()
            {
                return value;
            }

            public void setValue(string val)
            {
                this.value = val;
            }
        }

        public bool create(String filePath, List<String> dataList)
        {
            // init func
            AttributeCheck attributeCheck = new AttributeCheck();
            // init variable
            //FileStream file = File.Create(filePath);

            if (attributeCheck.listNotNull_Zero(dataList))
            {
                Console.WriteLine("filePath=" + filePath);
                //Path path = Paths.get(filePath);
                if (!Directory.GetParent(filePath).Exists)
                {
                    Directory.CreateDirectory(filePath);
                }
                try
                {
                    for (int i = 0; i < dataList.Count; i++)
                    {
                        File.WriteAllText(filePath, dataList[i], Encoding.GetEncoding(Charsets.BIG5));
                    }
                }   
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    // TODO Auto-generated catch block
                    //e.printStackTrace();
                }
                //File getFile = new File(filePath);
                try
                {
                    while (File.Exists(filePath))
                    {
                        if (File.Exists(filePath))
                        {
                            Console.WriteLine("filePath=" + filePath + " - exist");
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("filePath=" + filePath + " - not exist");
                            Thread.Sleep(100);
                        }
                    }
                }
                catch (Exception e)
                {
                    // TODO Auto-generated catch block
                    //e.printStackTrace();
                }
            }
            else
            {
                return false;
            }
            return false;
        }


        public bool create(String filePath, List<String> dataList, Charsets charsets)
        {
            // init func
            AttributeCheck attributeCheck = new AttributeCheck();
            if (attributeCheck.listNotEmpty(dataList) && attributeCheck.stringsNotNull(new string[] { filePath }))
            {
                // init variable
                FileStream file = File.Create(filePath);
                
                if (!Directory.GetParent(filePath).Exists)
                {
                    Directory.CreateDirectory(filePath);
                }
                // Path path = Paths.get(filePath);
                //Path path = file.toPath();
                try
                {
                    // Create file
                    File.WriteAllText(filePath, dataList[0],Encoding.GetEncoding(charsets.getValue()));

                    // Check file exist
                    while (File.Exists(filePath))
                    {
                        if (File.Exists(filePath))
                        {
                            return true;
                        }
                        else
                        {
                            Thread.Sleep(10);
                        }
                    }
                }
                catch (IOException e)
                {
                    //LOGGER.error(new Gson().toJson(e));
                }
                catch (Exception e)
                {
                    //LOGGER.error(new Gson().toJson(e));
                    Thread.CurrentThread.Interrupt();
                }
            }
            return false;
        }

        public bool create(String filePath, String content, Charsets charsets)
        {
            // init func
            AttributeCheck attributeCheck = new AttributeCheck();
            if (attributeCheck.stringsNotNull(new string[] { filePath, content }))
            {
                FileStream file = File.Create(filePath);
                if (!Directory.GetParent(filePath).Exists)
                {
                    Directory.CreateDirectory(filePath);
                }
                
                try
                {
                    using (StreamWriter outputStreamWriter = new StreamWriter(file, Encoding.GetEncoding(charsets.getValue())))
                    {
                        {
                            outputStreamWriter.Write(content);
                            outputStreamWriter.Flush();
                            while (true)
                            {
                                if (File.Exists(filePath) && file.Length > 0)
                                {
                                    return true;
                                }
                                else
                                {
                                    Thread.Sleep(10);
                                }
                            }
                        }
                    }
                }
                catch (IOException e)
                {
                    //LOGGER.error(new Gson().toJson(e));
                }
                catch (Exception e)
                {
                    //LOGGER.error(new Gson().toJson(e));
                    Thread.CurrentThread.Interrupt();
                }
                
            }
            return false;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool createSingalForRecognition(String filePath, List<String> dataList)
        {
            AttributeCheck attributeCheck = new AttributeCheck();
            // init variable
            bool flag = false;
            if (attributeCheck.listNotEmpty(dataList) && attributeCheck.stringsNotNull(new string[] { filePath }))
            {
                FileStream file = File.Create(filePath);
                if (!Directory.GetParent(filePath).Exists)
                {
                    Directory.CreateDirectory(filePath);
                }

                try
                {
                    //using (BufferedStream bufferedWriter = new BufferedStream(file))
                    //{
                    //    foreach (String line in dataList)
                    //    {
                    //        bufferedWriter.Write(Encoding.ASCII.GetBytes(line) + "\n");
                    //    }
                    //}
                    using (StreamWriter writer = new StreamWriter(file))
                    {
                        foreach (String line in dataList)
                        {
                            writer.WriteLine(line);
                        }
                    }
                }
                catch (IOException e)
                {
                    //LOGGER.error(new Gson().toJson(e));
                }
                flag = true;
            }
            return flag;
        }


        public String read_content(String txtPath)
        {
            // init func
            AttributeCheck attributeCheck = new AttributeCheck();
            // init variable
            String contents = null;
            if (attributeCheck.stringsNotNull(txtPath))
            {
                // init variable
                //File txtFile = new File(txtPath);
                List<String> lines = null;
                if (File.Exists(txtPath))
                {
                    try
                    {
                        lines = File.ReadAllLines(txtPath).ToList();
                    }
                    catch (IOException e)
                    {
                        //LOGGER.error(new Gson().toJson(e));
                    }
                    if (attributeCheck.listNotEmpty(lines))
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        foreach (String line in lines)
                        {
                            if (stringBuilder.Length > 0)
                            {
                                stringBuilder.Append("\n");
                            }
                            stringBuilder.Append(line);
                        }
                        contents = stringBuilder.ToString();
                    }
                }

            }
            return contents;
        }

        public List<String> read_lineList(String txtPath, String charsets)
        {
            // init variable
            List<String> lineList = null;
            //final File file = new File(txtPath);

            if (File.Exists(txtPath))
            {
                try
                {
                    lineList = File.ReadAllLines(txtPath, Encoding.GetEncoding(charsets)).ToList();
                }
                catch (IOException e)
                {
                    //LOGGER.error(new Gson().toJson(e));
                }
            }
            return lineList;
        }

        public List<String> read_lineList(String txtPath)
        {
            // init variable
            List<String> lineList = null;
            //final File file = new File(txtPath);

            if (File.Exists(txtPath))
            {
                try
                {
                    lineList = File.ReadAllLines(txtPath).ToList();
                }
                catch (IOException e)
                {
                    //LOGGER.error(new Gson().toJson(e));
                }
            }
            return lineList;
        }
    }
}
