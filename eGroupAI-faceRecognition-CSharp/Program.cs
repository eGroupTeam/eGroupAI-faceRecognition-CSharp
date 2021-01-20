using eGroupAI_faceRecognition_CSharp.engine.control;
using eGroupAI_faceRecognition_CSharp.engine.entity;
using eGroupAI_faceRecognition_CSharp.engine.util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
        // init variable
        static Boolean logDeleteFlag = false;
        // init path
        static String quickStartPath = "C:\\QuickStart";
        static StringBuilder enginePath = new StringBuilder(quickStartPath + "\\eGroupAI_FaceEngine_CPU_Windows_V4.2.2");
        static StringBuilder faceDBPath = new StringBuilder(enginePath + "\\eGroup");
        static StringBuilder resourcesPath = new StringBuilder(enginePath + "\\resources");
        static StringBuilder trainResultLogPath = new StringBuilder(enginePath + "\\Status.TrainResultCPU.eGroup");
        static StringBuilder trainListPath = new StringBuilder(resourcesPath + "\\list.txt");
        static StringBuilder modelAppendListPath = new StringBuilder(faceDBPath + "\\modelList.egroup.List");
        static StringBuilder outputfacePath = new StringBuilder(enginePath + "\\outputFace");
        static StringBuilder outputframepath = new StringBuilder(enginePath + "\\outputFrame");
        static StringBuilder jsonFolderPath = new StringBuilder(enginePath + "\\json");
        static StringBuilder catchJsonName = new StringBuilder("output.cache.egroup");
        // static StringBuilder allJsonName = new StringBuilder("output." + LocalDate.now() + ".egroup");
        static StringBuilder modelInserFilePath = new StringBuilder(enginePath + "\\Singal_For_Model_Insert.txt");
        static StringBuilder videoPath = new StringBuilder("resources\\example.mp4");
        static String resolution = "720p";
        // init file
        static FileInfo outputfaceFile = new FileInfo(outputfacePath.ToString());
        static FileInfo faceDBFile = new FileInfo(faceDBPath.ToString());
        static FileInfo outputframeFile = new FileInfo(outputframepath.ToString());
        static FileInfo jsonFolderFile = new FileInfo(jsonFolderPath.ToString());
        // init person path
        static StringBuilder jerryFaceDBPath = new StringBuilder(faceDBPath + "\\jerry");
        static StringBuilder leonardFaceDBPath = new StringBuilder(faceDBPath + "\\leonard");
        static StringBuilder danielFaceDBPath = new StringBuilder(faceDBPath + "\\daniel");
        // init person image
        static StringBuilder jerryFaceImageFolderPath = new StringBuilder(resourcesPath + "\\jerry");
        static StringBuilder danielFaceImageFolderPath = new StringBuilder(resourcesPath + "\\daniel");
        static StringBuilder lenardFaceImageFolderPath = new StringBuilder(resourcesPath + "\\leonard");
        static DirectoryInfo jerryFaceImageFolder = new DirectoryInfo(jerryFaceImageFolderPath.ToString());
        static DirectoryInfo danielFaceImageFolder = new DirectoryInfo(danielFaceImageFolderPath.ToString());
        static DirectoryInfo leonardFaceImageFolder = new DirectoryInfo(lenardFaceImageFolderPath.ToString());

        /**
 * 
 * Get face face DB path by person name
 * 
 * @author eGroupAI Team
 *
 * @param name
 * @return
 */
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

        /**
         * Get face image path in folder by person name
         * 
         * @author eGroupAITeam
         *
         * @param name
         * @return
         */
        public static List<String> getFaceImageFolder(String name)
        {
            List<String> imagePathList = new List<String>();
            switch (name.ToLower())
            {
                case "daniel":
                    imagePathList = folderUtil.listPath(danielFaceImageFolder);
                    break;
                case "leonard":
                    imagePathList = folderUtil.listPath(leonardFaceImageFolder);
                    break;
                case "jerry":
                    imagePathList = folderUtil.listPath(jerryFaceImageFolder);
                    break;
                default:
                    break;
            }
            return imagePathList;
        }

        /**
     * Example: Recognized with Face Model and get Result（JSON）. Document: https://reurl.cc/Y6r9Ya
     * 
     * @author eGroupAI Team
     *
     * @param usedFaceDB
     */
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
            Console.WriteLine("Faces : " + JsonConvert.SerializeObject(faceList));
        }


        /**
 * Input Face, Create Face Model. Document : https: // reurl.cc/Y6r94a
 * 
 * @author eGroupAI Team
 *
 * @param name
 */
        public static void training(String name)
        {
            // https://www.egroup.com.tw/en/docs/windows-cpu/v4.2.1/getting-started#0054007200610069006e0069006e0067
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


        /**
         * Example: Input Face(Untrained Face) and Trained immediately, then Insert Face Model to Recognition and get Result at the same time. Document:
         * https://reurl.cc/EzMpQm
         * 
         * @author eGroupAI Team
         *
         * @param name
         */
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


        /**
         * Example: Append person Face Model into all face DB. Document: https://reurl.cc/EzMpQm
         * 
         * @author eGroupAI Team
         *
         */
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        static void Main(string[] args)
        {
            if (!faceDBFile.Exists)
            {
                Directory.CreateDirectory(faceDBFile.FullName);
            }
            if (!outputfaceFile.Exists)
            {
                Directory.CreateDirectory(outputfaceFile.FullName);
            }
            if (!outputframeFile.Exists)
            {
                Directory.CreateDirectory(outputframeFile.FullName);
            }
            if (!jsonFolderFile.Exists)
            {
                Directory.CreateDirectory(jsonFolderFile.FullName);
            }
            // ================================================== Step1 : Training========================================================= //
            // Example: Input jerry's Face, Create jerry's Face Model.
            // Document: https: // reurl.cc/Y6r94a
            training("jerry");

            // ==================================================Step2 : Recognition======================================================== //
            // Example: Input jerry Face, Recognized with jerry’s Face Model and get Result（JSON）.
            // Document: https://reurl.cc/Y6r9Ya
            Thread recognitionThread = new Thread(new ThreadStart(delegate
            {
                recognition(jerryFaceDBPath + ".faceDB");
            }));
            recognitionThread.Start();

            // =================================================Step3 : ModelInsert========================================================= //
            // Document: https://reurl.cc/EzMpQm
            // 1.Example: Input leonard Face(Untrained Face) and Trained immediately
            training("leonard");
            // 2.then Insert leonard Face Model to Face Model and get Recognition Resultat the same time
            modelInsert("leonard");

            // ================================================wait recognition thread done================================================== //
            //recognitionThread.Interrupt();
            //recognitionThread.Suspend();
            //[MethodImpl(MethodImplOptions.Synchronized)]
            //Monitor.Wait(recognitionThread, 1255);

            recognitionThread.Join();
            
            //waitthread(recognitionThread);
            //{
            //    try
            //    {
            //        recognitionThread.wait();
            //    }
            //    catch (InterruptedException e)
            //    {
            //        LOGGER.error(new Gson().toJson(e));
            //    }
            //}

            // ==================================================Step4 : Model Append======================================================== //
            // Example: Append daniel and leonard Face Model into all face DB.
            // Document: https://reurl.cc/EzMpQm
            // 1.Execute train face instructions (see Training Procedure for details)
            training("daniel");
            // 2.Execute Model Append instructions (see Model Append Procedure for details)
            modelAppend();
            // 3.Recognition - Example: Recognized with all face DB and get Result（JSON）.
            recognitionThread = new Thread(new ThreadStart(delegate
            {
                recognition(faceDBPath + ".faceDB");
            }));
            recognitionThread.Start();
        }
    }
}