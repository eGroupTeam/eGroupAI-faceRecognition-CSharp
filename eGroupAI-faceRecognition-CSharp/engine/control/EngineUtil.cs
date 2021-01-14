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
using static eGroupAI_faceRecognition_CSharp.engine.entity.RecognizeFace;

namespace eGroupAI_faceRecognition_CSharp.engine.control
{
    public class EngineUtil
    {
        //private static Logger LOGGER = LoggerFactory.getLogger(CmdUtil.class);

        public TrainResult trainFace(TrainFace trainFace, bool deleteTrainResultStatus)
        {
            // init func
            AttributeCheck attributeCheck = new AttributeCheck();
            CheckStatusUtil checkStatusUtil = new CheckStatusUtil();
            // init variabl
            TrainResult trainResult = new TrainResult();

            trainFace.generateCli();
            if (attributeCheck.listNotEmpty(trainFace.getCommandList()))
            {
                CmdUtil cmdUtil = new CmdUtil();
                String trainResultLogPath = trainFace.getEnginePath() + "\\Status.TrainResultCPU.eGroup";
                if (cmdUtil.cmdProcessBuilder(trainFace.getCommandList()))
                {
                    // init variable
                    trainResult = checkStatusUtil.trainFace(trainResultLogPath);
                    if (deleteTrainResultStatus)
                    {
                        try
                        {
                            Directory.Delete(Path.GetFileName(trainResultLogPath));
                        }
                        catch (IOException e)
                        {
                            //LOGGER.error(new Gson().toJson(e));
                        }
                    }
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

        public bool modelCompare(ModelCompare modelCompare)
        {
            bool flag = false;
            // init func
            modelCompare.generateCli();
            if (modelCompare.getCommandList() != null)
            {
                CmdUtil cmdUtil = new CmdUtil();
                flag = cmdUtil.cmdProcessBuilder(modelCompare.getCommandList());
            }
            return flag;
        }

        public bool stopRecognizeFace(RecognizeFace recognizeFace, RECOGNIZEMODE_ recognizeMode_)
        {
            bool flag = false;
            // init func
            recognizeFace.getStopCli(recognizeMode_);
            if (recognizeFace.getCommandList() != null)
            {
                CmdUtil cmdUtil = new CmdUtil();
                flag = cmdUtil.cmdProcessBuilder(recognizeFace.getCommandList());
            }
            return flag;
        }

        public bool recognizeFace(RecognizeFace recognizeFace)
        {
            bool flag = false;
            // init func
            recognizeFace.generateCli();
            //LOGGER.info("cli=" + recognizeFace.getCli());
            Console.WriteLine("cli=" + recognizeFace.getCli());
            if (recognizeFace.getCommandList() != null)
            {
                CmdUtil cmdUtil = new CmdUtil();
                flag = cmdUtil.cmdProcessBuilder(recognizeFace.getCommandList());
            }
            return flag;
        }

        public Dictionary<String, Boolean> recognizeFace(List<RecognizeFace> recognizeFaceList, Boolean waitRecognizeDone)
        {
            Dictionary<String, Boolean> hashMap = new Dictionary<String, Boolean>();
            Thread RECOGNITION_THREAD;

            if (waitRecognizeDone)
            {
                // init variable
                //Task executorService = new  Task(//= Task.ne.newFixedThreadPool(recognizeFaceList.size());
                List<Task<String>> resultList = new List<Task<String>>();

                for (int i = 0; i < recognizeFaceList.Count; i++)
                {
                    int index = i + 1;
                    RecognizeFace recognizeFace_fix = recognizeFaceList[i];
                    Task<String> future = new Task<String>(delegate
                   {
                       // init func
                       CmdUtil cmdUtil = new CmdUtil();
                       recognizeFace_fix.generateCli();
                       if (recognizeFace_fix.getCommandList() != null)
                       {
                           Boolean flag = cmdUtil.cmdProcessBuilder(recognizeFace_fix.getCommandList());
                           hashMap.Add(JsonConvert.SerializeObject(recognizeFace_fix), flag);
                       }
                       return "辨識執行續:" + index + "運作結束";
                   });
                    resultList.Add(future);
                }
                // Monitor execute thread status
                //executorService.shutdown();
                foreach (Task<String> fs in resultList)
                {
                    try
                    {
                        while (!fs.IsCompleted) ;
                        Console.WriteLine(fs.Result);
                        //LOGGER.debug(fs.get());
                    }
                    catch (ThreadInterruptedException e)
                    {
                        Console.WriteLine(JsonConvert.SerializeObject(e));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(JsonConvert.SerializeObject(e));
                    }
                }
            }
            else
            {
                foreach (RecognizeFace recognizeFace in recognizeFaceList)
                {
                    RecognizeFace recognizeFace_fix = recognizeFace;
                    RECOGNITION_THREAD = new Thread(new ThreadStart(delegate
   {
       // init func
       CmdUtil cmdUtil = new CmdUtil();
       recognizeFace_fix.generateCli();
       if (recognizeFace_fix.getCommandList() != null)
       {
           Boolean flag = cmdUtil.cmdProcessBuilder(recognizeFace_fix.getCommandList());
           hashMap.Add(JsonConvert.SerializeObject(recognizeFace_fix), flag);
       }
   }));
                    RECOGNITION_THREAD.Start();
                }
            }
            return hashMap;
        }

        public ModelInsertResult modelInsert(ModelInsert modelInsert, Boolean deleteModelInsertStatusFlag, long waitTimeMs)
        {
            AttributeCheck attributeCheck = new AttributeCheck();
            ModelInsertResult modelInsertResult = new ModelInsertResult();

            if (modelInsert != null && attributeCheck.listNotEmpty(modelInsert.getFaceDBList()) && attributeCheck.stringsNotNull(modelInsert.getListPath()))
            {
                // init func
                TxtUtil txtUtil = new TxtUtil();
                CheckStatusUtil checkStatusUtil = new CheckStatusUtil();
                // init variable
                int modelCount = modelInsert.getFaceDBList().Count;
                List<String> dataList = new List<String>();
                String modelInsertLog_path = modelInsert.getEnginePath() + "\\Status.ModelInsert.eGroup";
                FileInfo modelInsertLog_file = new FileInfo(modelInsertLog_path);

                for (int i = 0; i < modelCount; i++)
                {
                    dataList.Add(modelInsert.getFaceDBList()[i]);
                }
                txtUtil.create(modelInsert.getListPath(), dataList);//, Charsets.BIG5);
                modelInsertResult = checkStatusUtil.modelInsert(modelInsertLog_path, waitTimeMs);
                if (deleteModelInsertStatusFlag)
                {
                    modelInsertLog_file.Delete();
                }
            }
            return modelInsertResult;
        }

        public ModelAppendResult modelAppend(ModelAppend modelAppend, Boolean deleteModelAppendStatus, long waitTime)
        {
            // init func
            AttributeCheck attributeCheck = new AttributeCheck();
            // init variable
            ModelAppendResult modelAppendResult = new ModelAppendResult();
            if (modelAppend != null
                && attributeCheck.stringsNotNull(new string[] { modelAppend.getEnginePath(), modelAppend.getListPath(), modelAppend.getTrainedFaceDBPath() })
                && (attributeCheck.listNotEmpty(modelAppend.getFaceDBList()) || modelAppend.getFaceDBHashset().Count>0))
            {
                // init func
                TxtUtil txtUtil = new TxtUtil();
                CheckStatusUtil checkStatusUtil = new CheckStatusUtil();
                // init variable
                List<String> dataList = new List<String>();
                String modelAppendStatusPath = modelAppend.getEnginePath() + "\\Status.ModelAppend.eGroup";

                if (modelAppend.getFaceDBHashset().Count>0)
                {
                    foreach (String faceDBPath in modelAppend.getFaceDBHashset())
                    {
                        dataList.Add(faceDBPath);
                    }
                }
                else
                {
                    int modelCount = modelAppend.getFaceDBList().Count;
                    for (int i = 0; i < modelCount; i++)
                    {
                        dataList.Add(modelAppend.getFaceDBList()[i]);
                    }
                }
                txtUtil.create(modelAppend.getListPath(), dataList);//, Charsets.BIG5);
                modelAppend.generateCli(modelAppend.getEnginePath());
                if (modelAppend.getCommandList() != null)
                {
                    CmdUtil cmdUtil = new CmdUtil();
                    if (cmdUtil.cmdProcessBuilder(modelAppend.getCommandList()))
                    {
                        modelAppendResult = checkStatusUtil.modelAppend(modelAppendStatusPath, waitTime);

                        if (deleteModelAppendStatus)
                        {
                            try
                            {
                                File.Delete(modelAppendStatusPath);
                            }
                            catch (IOException e)
                            {
                                Console.WriteLine(JsonConvert.SerializeObject(e));
                            }
                        }
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
            else
            {
                modelAppendResult.setAppendCmdSuccess(false);
            }
            return modelAppendResult;
        }

    }
}