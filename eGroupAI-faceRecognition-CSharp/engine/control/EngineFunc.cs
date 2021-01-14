using eGroupAI_faceRecognition_CSharp.engine.entity;
using eGroupAI_faceRecognition_CSharp.engine.util;
using eGroupAI_faceRecognition_CSharp.library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.control
{
    public class EngineFunc
    {
        public bool recognizeFace(RecognizeFace recognizeFace)
        {
            bool flag = false;
            // init func 
            recognizeFace.generateCli();
            if (recognizeFace.getCommandList() != null)
            {
                CmdUtil cmdUtil = new CmdUtil();
                flag = cmdUtil.cmdProcessBuilder(recognizeFace.getCommandList());
            }
            return flag;
        }


        public TrainResult trainFace(TrainFace trainFace)
        {
            // init variabl
            TrainResult trainResult = new TrainResult();

            trainFace.generateCli();
            if (trainFace.getCommandList() != null)
            {
                CmdUtil cmdUtil = new CmdUtil();
                if (cmdUtil.cmdProcessBuilder(trainFace.getCommandList()))
                {
                    // init variabl
                    String logTrainResultLog_path = trainResult.getPassFacePathList()[0];
                    trainResult = trainFace_check(logTrainResultLog_path);
                }
                else
                {
                    trainResult.setTrainCmdSuccess(false);
                }
            }
            else
            {
                trainResult.setTrainCmdSuccess(false);
            }
            return trainResult;
        }


        private TrainResult trainFace_check(String trainResultLog_path)
        {
            // init func
            AttributeCheck attributeCheck = new AttributeCheck();
            TxtUtil txtUtil = new TxtUtil();
            // init variable
            long time1, time2;
            time1 = DateTime.Now.Millisecond;
            TrainResult trainResult = new TrainResult();
            List<TrainInfo> trainInfoList = new List<TrainInfo>();

            // Get the trainFace result
            List<String> trainResultLineList = txtUtil.read_lineList(trainResultLog_path);
            if (attributeCheck.listNotNull_Zero(trainResultLineList))
            {
                TrainInfo trainInfo = new TrainInfo();

                foreach (String trainResultLine in trainResultLineList)
                {
                    String[] trainArray = trainResultLine.Split(new string[] { "\t" }, StringSplitOptions.None);
                    trainInfo = new TrainInfo();
                    switch (trainArray.Length)
                    {
                        case 4:
                            Console.WriteLine("trainArray[1]=" + trainArray[1]);
                            if (trainArray[1].Equals("Pass"))
                            {
                                trainResult.getPassFacePathList().Add(trainArray[2]);
                            }
                            else
                            {
                                trainResult.getFailFacePathList().Add(trainArray[2]);
                            }
                            trainInfo.setFacePath(trainArray[2]);
                            trainInfo.setStatus(trainArray[1]);
                            trainInfo.setTime(trainArray[0]);
                            trainInfo.setPersonId(trainArray[3]);
                            trainInfoList.Add(trainInfo);
                            break;
                        case 2:
                            if (trainArray[1].Equals("faces were trained in the list file"))
                            {
                                trainResult.setFaceSize(int.Parse(trainArray[0].Replace(":", "").ToString()));
                            }
                            else if (trainArray[1].Equals("list file size"))
                            {
                                trainResult.setFileSize(int.Parse(trainArray[0].Replace(":", "").ToString()));
                            }
                            break;
                        case 1:
                            if (trainArray[0].StartsWith("Processing Time"))
                            {
                                trainResult.setProcessingTime(trainArray[0].ToString());
                            }
                            else
                            {
                                trainResult.setAvgPprocessingTime(trainArray[0].ToString());
                            }
                            break;
                        default:
                            break;
                    }
                }
                trainResult.setTrainInfoList(trainInfoList);

                Console.WriteLine("trainResult="); //+JsonConvert.SerializeObject(trainResult));
                time2 = DateTime.Now.Millisecond;
                Console.WriteLine("read trainResult log：" + (time2 - time1) + "ms");
            }
            else
            {
                trainResult.setTrainResultFileExist(false);
            }
            return trainResult;
        }

        public static ModelAppendResult modelAppend(ModelAppend modelAppend)
        {
            // init func
            AttributeCheck attributeCheck = new AttributeCheck();
            // init variable
            ModelAppendResult modelAppendResult = new ModelAppendResult();
            if (modelAppend != null && attributeCheck.stringsNotNull(new string[] { modelAppend.getEnginePath(), modelAppend.getListPath(), modelAppend.getTrainedBinaryPath(), modelAppend.getTrainedFaceInfoPath() })
                    && (attributeCheck.listNotNull_Zero(modelAppend.getModelBinaryList())  && attributeCheck.listNotNull_Zero(modelAppend.getModelFaceInfoList())  || modelAppend.getModelHashmap().Count > 0))
            {

                Console.WriteLine("modelAppend.getTrainedBinaryPath()=" + modelAppend.getTrainedBinaryPath());
                // init func
                TxtUtil txtUtil = new TxtUtil();
                // init variable
                List<String> dataList = new List<String>();

                if (modelAppend.getModelHashmap().Count > 0)
                {
                    foreach (KeyValuePair<String, String> item in modelAppend.getModelHashmap())
                    {
                        dataList.Add(item.Key + "	" + item.Value);
                    }
                }
                else
                {
                    int modelCount = modelAppend.getModelBinaryList().Count;
                    for (int i = 0; i < modelCount; i++)
                    {
                        dataList.Add(modelAppend.getModelBinaryList()[i] + "	" + modelAppend.getModelFaceInfoList()[i]);
                    }
                }
                txtUtil.create(modelAppend.getListPath(), dataList);

                modelAppend.generateCli(modelAppend.getEnginePath());
                if (modelAppend.getCommandList() != null)
                {
                    CmdUtil cmdUtil = new CmdUtil();
                    if (cmdUtil.cmdProcessBuilder(modelAppend.getCommandList()))
                    {
                        modelAppendResult = modelAppend_check(modelAppend);
                    }
                    else
                    {
                        modelAppendResult.setAppendCmdSuccess(false);
                    }
                }
                else
                {
                    modelAppendResult.setAppendCmdSuccess(false);
                }
            }
            return modelAppendResult;
        }

        private static ModelAppendResult modelAppend_check(ModelAppend modelAppend)
        {
            // init func
            AttributeCheck attributeCheck = new AttributeCheck();
            TxtUtil txtUtil = new TxtUtil();

            // init variable
            String modelAppend_path = modelAppend.getEnginePath() + "/Log.ModelAppend.eGroup";
            FileStream modelAppend_file =  File.Create(modelAppend_path);
            int waitCount = 0;
            long time1 = DateTime.Now.Millisecond, time2;
            ModelAppendResult modelAppendResult = new ModelAppendResult();
            List<ModelAppendInfo> modelAppendInfoList = new List<ModelAppendInfo>();
            // Check whether modelAppend log exist, wait 1.5 sec
            try
            {
                while (true)
                {
                    if (File.Exists(modelAppend_path) || waitCount == 3)
                    {
                        break;
                    }
                    waitCount++;
                    Thread.Sleep(300);
                }
            }
            catch (Exception e)
            {
                // TODO Auto-generated catch block
                //e.printStackTrace();
            }

            // Get the model append log
            List<String> modelAppendLineList = txtUtil.read_lineList(modelAppend_path);
            Console.WriteLine("modelAppendLineList=" +  JsonConvert.SerializeObject(modelAppendLineList));
            if (attributeCheck.listNotNull_Zero(modelAppendLineList))
            {

                ModelAppendInfo modelAppendInfo = new ModelAppendInfo();
                foreach (String modelAppendLine in modelAppendLineList)
                {
                    String[] modelAppendArray = modelAppendLine.Split(new string[] { "\\t" }, StringSplitOptions.None);
                    Console.WriteLine("modelAppendArray=" + JsonConvert.SerializeObject(modelAppendArray));
                    switch (modelAppendArray.Length)
                    {
                        case 5:
                            if (modelAppendArray[2].Contains("DBSize"))
                            {
                                Console.WriteLine("DBSize=" + modelAppendArray[3].Substring(6, modelAppendArray[3].Length));
                                modelAppendInfo.setDBSizeCheckStatus(modelAppendArray[1]);
                                modelAppendInfo.setDBSize(int.Parse(modelAppendArray[2].Substring(7, modelAppendArray[2].Length).Trim()));
                                modelAppendInfo.setDBBinaryPath(modelAppendArray[3]);
                                modelAppendInfo.setDBFaceInfoPath(modelAppendArray[4]);
                                modelAppendInfoList.Add(modelAppendInfo);
                                modelAppendInfo = new ModelAppendInfo();
                            }
                            break;
                        case 4:
                            Console.WriteLine("modelAppendArray[2]=" + modelAppendArray[1]);
                            if (modelAppendArray[2].Equals("Parsing"))
                            {
                                modelAppendResult.setModelListPath(modelAppendArray[3].Replace("file: ", ""));
                                modelAppendResult.setModelListCheckStatus(modelAppendArray[1]);
                            }
                            else if (modelAppendArray[2].Equals("WorkingFolder"))
                            {
                                Console.WriteLine("modelAppendArray[3].Substring(1, (modelAppendArray[3].IndexOf(GB)))=" + modelAppendArray[3].Substring(1, (modelAppendArray[3].IndexOf("GB"))));
                                modelAppendInfo.setWorkingFolderCheckStatus(modelAppendArray[1]);
                                modelAppendInfo.setWorkingFolderSize(Double.Parse(modelAppendArray[3].Substring(1, (modelAppendArray[3].IndexOf("GB")))));
                                modelAppendInfo.setWorkingFolderStatus(modelAppendArray[3].Substring((modelAppendArray[3].IndexOf("GB") + 3), (modelAppendArray[3].LastIndexOf("("))).Trim());
                            }
                            else if (modelAppendArray[2].Equals("OutputBinary"))
                            {
                                Console.WriteLine("modelAppendArray[3].Substring(1, (modelAppendArray[3].IndexOf(GB)))=" + modelAppendArray[3].Substring(1, (modelAppendArray[3].IndexOf("GB"))));
                                modelAppendInfo.setOutputBinaryCheckStatus(modelAppendArray[1]);
                                modelAppendInfo.setOutputBinarySize(Double.Parse(modelAppendArray[3].Substring(1, (modelAppendArray[3].IndexOf("GB")))));
                                modelAppendInfo.setOutputBinaryStatus(modelAppendArray[3].Substring((modelAppendArray[3].IndexOf("GB") + 3), (modelAppendArray[3].LastIndexOf("("))).Trim());
                            }
                            else if (modelAppendArray[2].Equals("OutputFaceInfo"))
                            {
                                Console.WriteLine("modelAppendArray[3].Substring(1, (modelAppendArray[3].IndexOf(GB)))=" + modelAppendArray[3].Substring(1, (modelAppendArray[3].IndexOf("GB"))));
                                modelAppendInfo.setOutputFaceInfoCheckStatus(modelAppendArray[1]);
                                modelAppendInfo.setOutputFaceInfoSize(Double.Parse(modelAppendArray[3].Substring(1, (modelAppendArray[3].IndexOf("GB")))));
                                modelAppendInfo.setOutputFaceInfoStatus(modelAppendArray[3].Substring((modelAppendArray[3].IndexOf("GB") + 3), (modelAppendArray[3].LastIndexOf("("))).Trim());
                            }
                            break;
                        case 1:
                            if (modelAppendArray[0].StartsWith("Total faces: "))
                            {
                                Console.WriteLine(modelAppendArray[0].Substring(modelAppendArray[0].IndexOf("Total faces: "), modelAppendArray[0].IndexOf("in the appended new model")).Trim());
                                modelAppendResult.setTotalFaceCount(int.Parse(modelAppendArray[0].Substring(modelAppendArray[0].IndexOf("Total faces: ") + 12, modelAppendArray[0].IndexOf("in the appended new model")).Trim()));
                            }
                            else
                            {
                                Console.WriteLine(modelAppendArray[0].Substring(0, modelAppendArray[0].IndexOf("of models appended pass")));
                                Console.WriteLine(modelAppendArray[0].Substring(modelAppendArray[0].IndexOf("/") + 1, modelAppendArray[0].IndexOf("of models appended failed")));
                                modelAppendResult.setAppendPassCount(int.Parse(modelAppendArray[0].Substring(0, modelAppendArray[0].IndexOf("of models appended pass")).Trim()));
                                modelAppendResult.setAppendFailCount(int.Parse(modelAppendArray[0].Substring(modelAppendArray[0].IndexOf("/") + 1, modelAppendArray[0].IndexOf("of models appended failed")).Trim()));
                            }
                            break;
                        default:
                            break;
                    }

                }
                // delete the model append log file
                modelAppend_file.Dispose();
                // set the model append  result info
                modelAppendResult.setModelAppendInfoList(modelAppendInfoList);
                // calculate process time
                Console.WriteLine("modelAppendResult=" +JsonConvert.SerializeObject(modelAppendResult));
                time2 = DateTime.Now.Millisecond;
                Console.WriteLine("read modelAppend log：" + (time2 - time1) + "ms");
            }
            return modelAppendResult;
        }

        public ModelSwitchResult modelSwitch(ModelSwitch modelSwitch)
        {
            // init func
            AttributeCheck attributeCheck = new AttributeCheck();
            // init variable
            bool flag = false;
            ModelSwitchResult modelSwitchResult = new ModelSwitchResult();
            Console.WriteLine("modelSwitch.getNewModelPath()=" + modelSwitch.getNewModelPath());
            Console.WriteLine("modelSwitch.getSwitchFilePath()=" + modelSwitch.getSwitchFilePath());
            Console.WriteLine("modelSwitch.getEnginePath()=" + modelSwitch.getEnginePath());
            Console.WriteLine("modelSwitch.getModelSwitchLogPath()=" + modelSwitch.getModelSwitchLogPath());
            if (modelSwitch != null && attributeCheck.stringsNotNull(new string[] { modelSwitch.getNewModelPath(), modelSwitch.getSwitchFilePath(), modelSwitch.getEnginePath(), modelSwitch.getModelSwitchLogPath() }))
            {
                // init variable
                String newModelBinary_path = modelSwitch.getNewModelPath() + ".binary";
                String newModelFaceInfo_path = modelSwitch.getNewModelPath() + ".faceInfo";
                FileInfo newModelBinary_file = new FileInfo(newModelBinary_path);
                FileInfo newModelFaceInfo_file = new FileInfo(newModelFaceInfo_path);
                // Check Model Files 
                Console.WriteLine("newModelBinary_path=" + newModelBinary_path);
                Console.WriteLine("newModelFaceInfo_path=" + newModelFaceInfo_path);
                Console.WriteLine("newModelBinary_file.exists()=" + newModelBinary_file.Exists);
                Console.WriteLine("newModelFaceInfo_file.exists()=" + newModelFaceInfo_file.Exists);
                if (newModelBinary_file.Exists && newModelFaceInfo_file.Exists)
                {
                    // Model 
                    List<String> dataList = new List<String>();
                    dataList.Add(newModelBinary_path);
                    dataList.Add(newModelFaceInfo_path);

                    // init func
                    TxtUtil txtUtil = new TxtUtil();
                    Console.WriteLine("modelSwitch.getSwitchFilePath()--=" + modelSwitch.getSwitchFilePath());
                    flag = txtUtil.create(modelSwitch.getSwitchFilePath(), dataList);
                    Console.WriteLine("modelSwitch.getSwitchFilePath()-flag=" + flag);
                    FileInfo file;
                    try
                    {
                        while (true)
                        {
                            if (flag)
                            {
                                file = new FileInfo(modelSwitch.getModelSwitchLogPath());
                                if (file.Exists)
                                {
                                    modelSwitchResult = modelSwitch_check(modelSwitch);
                                    flag = modelSwitchResult.getIsSuccess();
                                    break;
                                }
                                Console.WriteLine("Model is Switching...");
                                Thread.Sleep(1000);
                            }
                            else
                            {
                                flag = false;
                                break;
                            }
                        }
                    }
                    catch (ThreadInterruptedException e)
                    {
                        // TODO Auto-generated catch block
                        Console.WriteLine(e.ToString());
                    }
                }
            }
            modelSwitchResult.setSuccess(flag);
            return modelSwitchResult;
        }

        private ModelSwitchResult modelSwitch_check(ModelSwitch modelSwitch)
        {
            long time1, time2;
            time1 = DateTime.Now.Millisecond;
            // init variable
            bool flag = true;
            TxtUtil txtUtil = new TxtUtil();
            AttributeCheck attributeCheck = new AttributeCheck();
            ModelSwitchResult modelSwitchResult = new ModelSwitchResult();
            List<String> modelSwitchLineList = txtUtil.read_lineList(modelSwitch.getModelSwitchLogPath());
            if (attributeCheck.listNotNull_Zero(modelSwitchLineList))
            {
                foreach (String modelSwitchLine in modelSwitchLineList)
                {
                    String[] modelSwitchArray = modelSwitchLine.Split('\t');
                    if (modelSwitchArray[1].Equals("Pass"))
                    {
                        if (modelSwitchArray[2].Equals("CheckBinary"))
                        {
                            modelSwitchResult.setCheckBinaryPass(true);
                        }
                        else if (modelSwitchArray[2].Equals("CheckFaceInfo"))
                        {
                            modelSwitchResult.setCheckFaceInfoPass(true);
                        }
                    }
                    else if (modelSwitchArray[1].Equals("Report"))
                    {
                        if (modelSwitchArray[3].StartsWith("Overall reload time:"))
                        {
                            modelSwitchResult.setFaceReload(modelSwitchArray[3]);
                        }
                        else
                        {
                            modelSwitchResult.setReloadTime(modelSwitchArray[3]);
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
                time2 = DateTime.Now.Millisecond;
                Console.WriteLine("read modelSwitchResult log：" + (time2 - time1) + "ms");
            }
            else
            {
                flag = false;
            }
            modelSwitchResult.setSuccess(flag);
            Console.WriteLine("modelSwitchResult=" +JsonConvert.SerializeObject(modelSwitchResult));
            return modelSwitchResult;
        }

        public static ModelInsertResult modelInsert(ModelInsert modelInsert)
        {
            // init func
            AttributeCheck attributeCheck = new AttributeCheck();
            // init variable
            ModelInsertResult modelInsertResult = new ModelInsertResult();

            if (modelInsert != null && attributeCheck.listNotNull_Zero(modelInsert.getModelBinaryList()) && attributeCheck.listNotNull_Zero(modelInsert.getModelFaceInfoList())
                    && attributeCheck.stringsNotNull(modelInsert.getListPath()))
            {
                Console.WriteLine("modelInsert=" +JsonConvert.SerializeObject(modelInsert));
                // init func
                TxtUtil txtUtil = new TxtUtil();
                // init variable
                int modelCount = modelInsert.getModelBinaryList().Count;
                List<String> dataList = new List<String>();
                String modelInsertLog_path = modelInsert.getEnginePath() + "\\Log.ModelInsert.eGroup";
                FileInfo modelInsertLog_file = new FileInfo(modelInsertLog_path);

                for (int i = 0; i < modelCount; i++)
                {
                    dataList.Add(modelInsert.getModelBinaryList()[i] + "	" + modelInsert.getModelFaceInfoList()[i]);
                }
                txtUtil.create(modelInsert.getListPath(), dataList);

                modelInsertResult = modelInsert_check(modelInsert);
                modelInsertLog_file.Delete();
            }
            return modelInsertResult;
        }

        private static ModelInsertResult modelInsert_check(ModelInsert modelInsert)
        {
            // init func
            AttributeCheck attributeCheck = new AttributeCheck();
            TxtUtil txtUtil = new TxtUtil();
            // init variable
            String modelInsertLog_path = modelInsert.getEnginePath() + "\\Log.ModelInsert.eGroup";
            FileInfo modelInsertLog_file = new FileInfo(modelInsertLog_path);
            long time1 = DateTime.Now.Millisecond, time2;
            ModelInsertResult modelInsertResult = new ModelInsertResult();
            int waitCount = 0;
            // Check whether modelAppend log exist, wait 1.5 sec
            try
            {
                while (true)
                {
                    if (modelInsertLog_file.Exists || waitCount == 3)
                    {
                        break;
                    }
                    waitCount++;
                    Thread.Sleep(300);
                }
            }
            catch (ThreadInterruptedException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
            }

            List<String> modelInsertLineList = txtUtil.read_lineList(modelInsertLog_path);
            if (attributeCheck.listNotNull_Zero(modelInsertLineList))
            {
                // init variable
                List<ModelInsertInfo> modelInsertInfoList = new List<ModelInsertInfo>();
                ModelInsertInfo modelInsertInfo = new ModelInsertInfo();

                foreach (String modelInsertLine in modelInsertLineList)
                {
                    String[] modelInsertArray = modelInsertLine.Split('\t');
                    Console.WriteLine("modelInsertArray=" +JsonConvert.SerializeObject(modelInsertArray));
                    switch (modelInsertArray.Length)
                    {
                        case 5:
                            Console.WriteLine("modelInsertArray[2]=" + modelInsertArray[1]);
                            if (modelInsertArray[2].Equals("InsertFace"))
                            {
                                modelInsertInfo.setDatetimeString(modelInsertArray[0]);
                                modelInsertInfo.setInsertModelStatus(modelInsertArray[1]);
                                modelInsertInfo.setInsertPeopleCount(int.Parse(modelInsertArray[3].Substring(0, modelInsertArray[3].IndexOf("/")).Trim()));
                                modelInsertInfo.setInsertFacesCount(int.Parse(modelInsertArray[3].Substring(modelInsertArray[3].IndexOf("/") + 1, modelInsertArray[3].IndexOf(" of faces/people were inserted")).Trim()));
                                modelInsertInfo.setCurrDBFaceCout(int.Parse(modelInsertArray[4].Substring(modelInsertArray[4].IndexOf("CurrPeopleCount=") + 16, modelInsertArray[4].IndexOf(" CurrFaceCount=")).Trim()));
                                modelInsertInfo.setCurrDBPeopleCount(int.Parse(modelInsertArray[4].Substring(modelInsertArray[4].IndexOf("CurrFaceCount=") + 14, modelInsertArray[4].Length).Trim()));
                            }
                            break;
                        case 4:
                            if (modelInsertArray[3].StartsWith("Overall insert time: "))
                            {
                                Console.WriteLine(modelInsertArray[3].Substring(modelInsertArray[3].IndexOf("Overall insert time: "), modelInsertArray[3].IndexOf(" sec. ")).Trim());
                                modelInsertInfo.setInsertProcessTime(modelInsertArray[3].Substring(modelInsertArray[3].IndexOf("Overall insert time: "), modelInsertArray[3].IndexOf(" sec. ")).Trim());
                            }
                            modelInsertInfoList.Add(modelInsertInfo);
                            modelInsertInfo = new ModelInsertInfo();
                            break;
                        default:
                            break;
                    }

                }
                modelInsertResult.setModelInsertInfoList(modelInsertInfoList);
                modelInsertLog_file.Delete();
                Console.WriteLine("modelInsertResult=" +JsonConvert.SerializeObject(modelInsertResult));
                time2 = DateTime.Now.Millisecond;
                Console.WriteLine("read modelAppend log：" + (time2 - time1) + "ms");
            }
            return modelInsertResult;
        }

    }
}
