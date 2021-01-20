using eGroupAI_faceRecognition_CSharp.engine.entity;
using eGroupAI_faceRecognition_CSharp.engine.util;
using eGroupAI_faceRecognition_CSharp.library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.control
{
    public class GetResultUtil
    {
        public List<Face> allResult(String jsonFolderPath, String jsonName, int startIndex, Boolean isDynamicJson, long waiteJsonMs)
        {
            // init func
            CopyUtil copyUtil = new CopyUtil();
            AttributeCheck attributeCheck = new AttributeCheck();

            // init variable
            Type faceListType = typeof(List<Face>);
            List<Face> faceList = new List<Face>();

            // Get retrieve result
            FileInfo sourceJson = new FileInfo(jsonFolderPath.ToString() + "/" + jsonName + ".json");
            StringBuilder jsonFileName = new StringBuilder(jsonFolderPath + "/" + jsonName + "_copy.json");
            FileInfo destJson = new FileInfo(jsonFileName.ToString());

            try
            {
                Thread.Sleep((int)waiteJsonMs);
            }
            catch (ThreadInterruptedException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e);
            }

            if (sourceJson.Exists && sourceJson.Length > 0)
            {
                // init func
                TxtUtil txtUtil = new TxtUtil();
                // init variable
                String jsonContent;
                jsonContent = txtUtil.read_content(jsonFileName.ToString());

                try
                {
                    copyUtil.copyFile(sourceJson, destJson);
                }
                catch (IOException e)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(e.Message));
                }

                // If has data
                if (attributeCheck.stringsNotNull(jsonContent))
                {
                    // Get last one object
                    if (isDynamicJson)
                    {
                        int endIndex = jsonContent.LastIndexOf("}\n\t,");
                        if (endIndex == -1)
                        {
                            endIndex = jsonContent.LastIndexOf("}\n]");
                        }
                        String json;
                        // Reorganization json
                        if (endIndex != -1 && startIndex != endIndex && startIndex < endIndex)
                        {
                            if (startIndex > 0)
                            {
                                json = "[" + jsonContent.ToString().Substring(startIndex + 2, endIndex) + "}]";
                            }
                            else
                            {
                                json = jsonContent.ToString().Substring(startIndex, endIndex) + "}]";
                            }
                            if (attributeCheck.stringsNotNull(json))
                            {
                                faceList = (List<Face>)JsonConvert.DeserializeObject(json, faceListType);
                                //faceList.get(faceList.size() - 1).setEndIndex(endIndex + 2);
                            }
                        }
                    }
                    else
                    {
                        // If has data
                        if (attributeCheck.stringsNotNull(jsonContent.ToString()))
                        {
                            faceList = (List<Face>)JsonConvert.DeserializeObject(jsonContent.ToString(), faceListType);
                        }
                    }
                }
            }

            return faceList;
        }

        /**
         * Get Retrieve result json
         * 
         * @author eGroupAI Team
         *
         * @param jsonPath
         * @param startIndex
         * @return
         */
        public List<Face> cacheResult(String jsonFolderPath, String jsonName)
        {
            // init func
            AttributeCheck attributeCheck = new AttributeCheck();
            // init variable
            List<Face> faceList = null;

            if (attributeCheck.stringsNotNull(new string[] { jsonFolderPath, jsonName }))
            {
                CopyUtil copyUtil = new CopyUtil();

                // init variable
                Type faceListType = typeof(List<Face>);

                // Get retrieve result
                FileInfo sourceJson = new FileInfo(jsonFolderPath + "\\" + jsonName + ".json");
                StringBuilder jsonFileName = new StringBuilder(jsonFolderPath + "\\" + jsonName + "_copy.json");
                FileInfo destJson = new FileInfo(jsonFileName.ToString());
                //if (!destJson.Exists)
                //{
                //    destJson.Create();
                //    destJson = new FileInfo(jsonFileName.ToString());
                //}
                if (sourceJson.Exists )//&& sourceJson.Length != destJson.Length)
                {
                    // init func
                    TxtUtil txtUtil = new TxtUtil();
                    // init variable
                    String jsonContent;
                    try
                    {
                        copyUtil.copyFile(sourceJson, destJson);
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine(JsonConvert.SerializeObject(e.Message));
                    }
                    jsonContent = txtUtil.read_content(jsonFileName.ToString());

                    // If has data
                    if (attributeCheck.stringsNotNull(jsonContent))
                    {
                        // Get last one object
                        int endIndex = jsonContent.LastIndexOf("}\n]");
                        if (endIndex == -1)
                        {
                            endIndex = jsonContent.LastIndexOf("}\n\t,");
                        }
                        if (endIndex > 0)
                        {
                            String json = jsonContent.Substring(0, endIndex) + "}]";
                            faceList = (List<Face>)JsonConvert.DeserializeObject(json, faceListType);
                        }
                    }
                }
            }
            return faceList;
        }

        /**
         * Get Retrieve result json
         * 
         * @author eGroupAI Team
         *
         * @param jsonPath
         * @param startIndex
         * @return
         */
        public List<Face> serverPhotoResult(String jsonPath, String jsonName, Boolean deleteJson)
        {
            // init variable
            Type faceListType = typeof(List<Face>);
            List<Face> faceList = new List<Face>();

            // Get retrieve result
            String sourceJson = jsonPath.ToString() + "\\" + jsonName + ".json";
            FileInfo sourceJson_file = new FileInfo(sourceJson);
            if (sourceJson_file.Exists)
            {
                // init func
                TxtUtil txtUtil = new TxtUtil();
                AttributeCheck attributeCheck = new AttributeCheck();
                // init variable
                //Path sourceJson_filePath = Paths.get(sourceJson);
                StringBuilder jsonFileName = new StringBuilder(jsonPath + "\\" + jsonName + ".json");

                String jsonContent = txtUtil.read_content(jsonFileName.ToString());

                if (attributeCheck.stringsNotNull(jsonContent))
                {
                    faceList = (List<Face>)JsonConvert.DeserializeObject(jsonContent, faceListType);
                }
                if (deleteJson)
                {
                    try
                    {
                        File.Delete(sourceJson);
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine(JsonConvert.SerializeObject(e.Message));
                    }
                }
            }
            return faceList;
        }

    }
}