using eGroupAI_faceRecognition_CSharp.engine.control;
using eGroupAI_faceRecognition_CSharp.engine.entity;
using eGroupAI_faceRecognition_CSharp.engine.util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp
{
    class Program
    {
        //private static Logger LOGGER = LoggerFactory.getLogger(QuickStart.class);
        static EngineUtil engineUtil = new EngineUtil();
        static CreateEngineFileUtil createEngineFileUtil = new CreateEngineFileUtil();
        static FolderUtil folderUtil = new FolderUtil();
        static GetResultUtil getResultUtil = new GetResultUtil();

        static bool logDeleteFlag = false;
        static String quickStartPath = "C:\\QuickStart";//"D:\\eGroupAI\\QuickStart";
        static StringBuilder enginePath = new StringBuilder(quickStartPath + "\\eGroupAI_FaceEngine_CPU_Windows_V4.2.2");

        static StringBuilder faceDBPath = new StringBuilder(enginePath + "\\eGroup");
        static StringBuilder outputfacePath = new StringBuilder(enginePath + "\\outputFace");
        static StringBuilder outputframepath = new StringBuilder(enginePath + "\\outputFrame");
        static StringBuilder jsonFolderPath = new StringBuilder(enginePath + "\\json");

        FileStream dbFs = File.Create(faceDBPath.ToString());
        FileStream outFs = File.Create(outputfacePath.ToString());
        FileStream frameFs = File.Create(outputframepath.ToString());
        FileStream jsonFs = File.Create(jsonFolderPath.ToString());

        static StringBuilder resourcesPath = new StringBuilder(enginePath + "\\resources");
        static StringBuilder trainListPath = new StringBuilder(resourcesPath + "\\list.txt");

        static StringBuilder jerryFaceDBPath = new StringBuilder(faceDBPath + "\\jerry");
        static StringBuilder leonardFaceDBPath = new StringBuilder(faceDBPath + "\\leonard");
        static StringBuilder danielFaceDBPath = new StringBuilder(faceDBPath + "\\daniel");

        static StringBuilder jerryFaceImageFolderPath = new StringBuilder(resourcesPath + "\\jerry");
        static StringBuilder danielFaceImageFolderPath = new StringBuilder(resourcesPath + "\\daniel");
        static StringBuilder lenardFaceImageFolderPath = new StringBuilder(resourcesPath + "\\leonard");
        
        static StringBuilder trainResultLogPath = new StringBuilder(enginePath + "\\Status.TrainResultCPU.eGroup");
        static StringBuilder modelAppendListPath = new StringBuilder(faceDBPath + "\\modelList.egroup.List");
        static StringBuilder catchJsonName = new StringBuilder("output.cache.egroup");
        static StringBuilder allJsonName = new StringBuilder("output." + DateTime.Now + ".egroup");
        static StringBuilder modelInserFilePath = new StringBuilder(enginePath + "\\Singal_For_Model_Insert.txt");
        static StringBuilder videoPath = new StringBuilder("resources\\example.mp4");
        static String resolution = "720p";

        static void Main(string[] args)
        {
            if (!Directory.Exists(faceDBPath.ToString()))
            {
                Directory.CreateDirectory(faceDBPath.ToString());
            }

            if (!Directory.Exists(outputfacePath.ToString()))
            {
                Directory.CreateDirectory(outputfacePath.ToString());
            }

            if (!Directory.Exists(outputframepath.ToString()))
            {
                Directory.CreateDirectory(outputframepath.ToString());
            }

            if (!Directory.Exists(jsonFolderPath.ToString()))
            {
                Directory.CreateDirectory(jsonFolderPath.ToString());
            }
            training("jerry");

            Thread recognitionThread = new Thread(new ThreadStart(delegate
            {
                recognition(jerryFaceDBPath + ".faceDB");
            }));
            recognitionThread.Start();
            training("leonard");
            // 2.then Insert leonard Face Model to Face Model and get Recognition Resultat the same time
            modelInsert("leonard");
            //Monitor.Wait(recognitionThread);

            recognitionThread.Suspend();
            //(recognitionThread) {
            //    try
            //    {
            //        recognitionThread.wait();
            //    }
            //    catch (InterruptedException e)
            //    {
            //        LOGGER.error(new Gson().toJson(e));
            //    }
            //}
            training("daniel");
            // 2.Execute Model Append instructions (see Model Append Procedure for details)
            modelAppend();
        }

        public static void modelAppend()
        {
            // https://www.egroup.com.tw/en/docs/windows-cpu/v4.2.1/getting-started#0054007200610069006e0069006e0067
            // Set append faceDB list
            List<String> faceDBList = new List<String>();
            faceDBList.Add(jerryFaceDBPath + ".faceDB");
            faceDBList.Add(leonardFaceDBPath + ".faceDB");
            faceDBList.Add(danielFaceDBPath + ".faceDB");

            // Model Append
            ModelAppend modelAppend = new ModelAppend();
            modelAppend.setTrainedFaceDBPath(faceDBPath + ".faceDB");
            modelAppend.setFaceDBList(faceDBList);
            modelAppend.setListPath(modelAppendListPath.ToString());
            modelAppend.setEnginePath(enginePath.ToString());
            ModelAppendResult modelAppendResult = engineUtil.modelAppend(modelAppend, false, 2500);
            Console.WriteLine("modelInsertResult : " + JsonConvert.SerializeObject(modelAppendResult));
        }

        public static void modelInsert(String name)
        {
            // Set insert facedb list
            List<String> faceDBList = new List<String>();
            faceDBList.Add(getFaceDBPath(name) + ".faceDB");
            // Set model insert variable
            ModelInsert modelInsert = new ModelInsert();
            modelInsert.setEnginePath(enginePath.ToString());
            modelInsert.setFaceDBList(faceDBList);
            modelInsert.setListPath(modelInserFilePath.ToString());
            ModelInsertResult modelInsertResult = engineUtil.modelInsert(modelInsert, false, 3000);
            Console.WriteLine("modelInsertResult : " + JsonConvert.SerializeObject(modelInsertResult));
        }

        public static void recognition(String usedFaceDB)
        {
            // Set recognition variable
            RecognizeFace recognizeFace = new RecognizeFace();
            recognizeFace.setEnginePath(enginePath.ToString());
            recognizeFace.setTrainedFaceDBPath(usedFaceDB);
            recognizeFace.setOutputFacePath(outputfacePath.ToString());
            recognizeFace.setOutputFramePath(outputframepath.ToString());
            recognizeFace.setJsonPath(jsonFolderPath + "\\output");
            recognizeFace.setHideMainWindow_(false);
            recognizeFace.setOutputFrame(true);
            recognizeFace.setOutputFace(true);
            recognizeFace.setOnface(true);
            recognizeFace.setThreshold(0.6);
            recognizeFace.setResolution(resolution);
            recognizeFace.setVideoPath(videoPath.ToString());
            recognizeFace.setMinimumFaceSize(100);
            recognizeFace.setThreads(2);
            // Start recognition
            engineUtil.recognizeFace(recognizeFace);
            // Get all result after recognize is done
            List<Face> faceList = getResultUtil.cacheResult(jsonFolderPath.ToString(), catchJsonName.ToString());
            //LOGGER.info("Faces : " + new Gson().toJson(faceList));
        }
        
        public static String getFaceDBPath(String name)
        {
            String faceDB = faceDBPath.ToString();
            switch (name.ToLower())
            {
                case "daniel":
                    faceDB = danielFaceDBPath.ToString();
                    break;
                case "leonard":
                    faceDB = leonardFaceDBPath.ToString();
                    break;
                case "jerry":
                    faceDB = jerryFaceDBPath.ToString();
                    break;
                default:
                    break;
            }
            return faceDB;
        }

        public static List<String> getFaceImageFolder(String name)
        {
            List<String> imagePathList = new List<String>();
            switch (name.ToLower())
            {
                case "daniel":
                    imagePathList = Directory.GetDirectories(danielFaceImageFolderPath.ToString()).ToList<String>();
                    break;
                case "leonard":
                    imagePathList = Directory.GetDirectories(lenardFaceImageFolderPath.ToString()).ToList<String>();
                    break;
                case "jerry":
                    imagePathList = Directory.GetDirectories(jerryFaceImageFolderPath.ToString()).ToList<String>();
                    break;
                default:
                    break;
            }
            return imagePathList;
        }

        public static void training(String name)
        {
            // init variable
            List<TrainFace> trainFaceList = new List<TrainFace>();
            // Set training variable
            TrainFace trainFace = new TrainFace();
            trainFace.setTrainListPath(trainListPath.ToString());
            trainFace.setModelPath(getFaceDBPath(name));
            trainFace.setEnginePath(enginePath.ToString());
            trainFace.setPersonId(name);
            // Get image in folder and set training image
            trainFace.setImagePathList(getFaceImageFolder(name));
            // Add to trainFace list
            trainFaceList.Add(trainFace);
            // Create train face list
            createEngineFileUtil.createTrainFaceTxt(trainListPath.ToString(), trainFaceList);
            // Start training and get result
            TrainResult trainResult = engineUtil.trainFace(trainFace, logDeleteFlag);
            Console.WriteLine("trainResult=" + JsonConvert.SerializeObject(trainResult));
        }
    }
}
