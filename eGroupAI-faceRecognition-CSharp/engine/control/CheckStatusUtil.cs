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
using static eGroupAI_faceRecognition_CSharp.engine.util.TxtUtil;

namespace eGroupAI_faceRecognition_CSharp.engine.control
{
    public class CheckStatusUtil
    {
        //private static Logger LOGGER = LoggerFactory.getLogger(CmdUtil.class);
        public int waitCount = 0;
        public static class Check
        {
            public static String CHECK0 { get { return "初始化驗證"; } }
            public static String CHECK1 { get { return "金鑰驗證"; } }
            public static String CHECK2 { get { return "辨識引擎啟動參數"; } }
            public static String CHECK3 { get { return "辨識引擎運作程式"; } }
            public static String CHECK4 { get { return "硬體驗證"; } }
            public static String CHECK5 { get { return "儲存空間"; } }
            public static String CHECK6 { get { return "影像來源"; } }
            public static String CHECK7 { get { return "辨識引擎演算法程式"; } }
            public static String CHECK8 { get { return "模型檔案"; } }
            public static String CHECK9 { get { return "模型檔案載入"; } }
            public static String CHECK10 { get { return "輸出結果資料夾"; } }

            public static String value;
        }

        /**
         * 辨識引擎啟動驗證
         * 
         * @author daniel
         *
         * @param startupStatusPath :(enginePath+"/Status.Startup.eGroup)
         * @return 10項啟動檢查
         */
        public static List<StartupStatus> recognizeServerStartup4(String enginePath, String modelPath, String startupStatusPath, long waitTimeMs,
            RECOGNIZEMODE_ recognizeMode_)
        {
            // init func
            AttributeCheck attributeCheck = new AttributeCheck();
            // init variable
            List<StartupStatus> startupStatusList = new List<StartupStatus>();

            StartupStatus startupStatus = new StartupStatus();
            bool checkFlag = true;

            // // 檢查初始化設定-路徑
            if (attributeCheck.stringsNotNull(new string[] { enginePath, modelPath }))
            {
                // init variable
                String licenseKeyPath = enginePath + "\\license.key";
                String recognizeExePath;
                if (RECOGNIZEMODE_.getValue().Equals("liveness"))
                {
                    recognizeExePath = enginePath + "\\LivenessDetectionServer.exe";
                }
                else
                {
                    recognizeExePath = enginePath + "\\RecognizeFace.exe";
                }
                String trainExePath = enginePath + "\\TrainFace.exe";
                String modelAppendExePath = enginePath + "\\ModelAppend.exe";
                FileInfo engineFolder = new FileInfo(enginePath);
                FileInfo modelFolder = new FileInfo(modelPath);
                FileInfo licenseKeyFile = new FileInfo(licenseKeyPath);
                FileInfo recognizeExeFile = new FileInfo(recognizeExePath);
                FileInfo trainExeFile = new FileInfo(trainExePath);
                FileInfo modelAppendExeFile = new FileInfo(modelAppendExePath);

                // 檢查初始化設定-資料夾
                if (!(engineFolder.Exists && modelFolder.Exists && licenseKeyFile.Exists && licenseKeyFile.Exists && recognizeExeFile.Exists && trainExeFile.Exists && modelAppendExeFile.Exists))
                {
                    checkFlag = false;
                }
            }
            else
            {
                checkFlag = false;
            }
            startupStatus.setCheckId(0);
            startupStatus.setCheckFlag(checkFlag);
            startupStatus.setCheckName(Check.CHECK0);
            startupStatusList.Add(startupStatus);

            if (checkFlag && attributeCheck.stringsNotNull(startupStatusPath))
            {
                // init func
                TxtUtil txtUtil = new TxtUtil();
                // init variable
                long startTime = DateTime.Now.Millisecond, endTime2;
                FileInfo startupStatusFile = new FileInfo(startupStatusPath);
                int checkCount = 0;
                try
                {
                    while (true)
                    {
                        endTime2 = DateTime.Now.Millisecond;
                        if (((endTime2 - startTime) > waitTimeMs) || (startupStatusFile.Exists && startupStatusPath.Length > 0))
                        {
                            break;
                        }
                        Thread.Sleep(200);
                    }
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(e.ToString()));
                    Thread.CurrentThread.Interrupt();
                }

                // 取得啟動的Log結果
                if (startupStatusFile.Exists && startupStatusPath.Length > 0)
                {
                    List<String> startupStatusLineList = txtUtil.read_lineList(startupStatusPath, Charsets.BIG5);
                    if (attributeCheck.listNotEmpty(startupStatusLineList))
                    {
                        // 文字檔資料路徑
                        foreach (String startupStatusLine in startupStatusLineList)
                        {
                            // 如果目前驗證階段皆Pass，繼續驗證下一階段，如果Fail則中斷此迴圈
                            if (checkFlag)
                            {
                                Console.WriteLine("checkCount=" + checkCount + ",startupStatusLine=" + startupStatusLine);
                                // init variable
                                startupStatus = new StartupStatus();
                                String[] startupStatusArray = startupStatusLine.Split('\t');
                                switch (startupStatusArray.Length)
                                {
                                    case 3: // 階段處理成功後訊息陣列長度為3，內容範例 : Check0 Pass(or
                                            // Fail) 2019-11-04 15:00:56
                                        switch (checkCount)
                                        {
                                            case 0: // 驗證金鑰
                                                if (startupStatusArray[0].Equals("Check0"))
                                                {
                                                    if (startupStatusArray[1].Equals("Pass"))
                                                    {
                                                        checkFlag = true;
                                                        checkCount++;
                                                    }
                                                    else
                                                    {
                                                        checkFlag = false;
                                                    }
                                                    startupStatus.setCheckId(1);
                                                    startupStatus.setCheckName(Check.CHECK1);
                                                    startupStatus.setCheckFlag(checkFlag);
                                                    startupStatusList.Add(startupStatus);
                                                }
                                                break;
                                            case 1:
                                                if (startupStatusArray[0].Equals("Check1"))
                                                {
                                                    if (startupStatusArray[1].Equals("Pass"))
                                                    {
                                                        checkFlag = true;
                                                        checkCount++;
                                                    }
                                                    else
                                                    {
                                                        checkFlag = false;
                                                    }
                                                    startupStatus.setCheckId(2);
                                                    startupStatus.setCheckName(Check.CHECK2);
                                                    startupStatus.setCheckFlag(checkFlag);
                                                    startupStatusList.Add(startupStatus);
                                                }
                                                break;
                                            case 2:
                                                if (startupStatusArray[0].Equals("Check2"))
                                                {
                                                    if (startupStatusArray[1].Equals("Pass"))
                                                    {
                                                        checkFlag = true;
                                                        checkCount++;
                                                    }
                                                    else
                                                    {
                                                        checkFlag = false;
                                                    }
                                                    startupStatus.setCheckId(3);
                                                    startupStatus.setCheckName(Check.CHECK3);
                                                    startupStatus.setCheckFlag(checkFlag);
                                                    startupStatusList.Add(startupStatus);
                                                }
                                                break;
                                            case 3:
                                                if (startupStatusArray[0].Equals("Check3"))
                                                {
                                                    if (startupStatusArray[1].Equals("Pass"))
                                                    {
                                                        checkFlag = true;
                                                        checkCount++;
                                                    }
                                                    else
                                                    {
                                                        checkFlag = false;
                                                    }
                                                    startupStatus.setCheckId(4);
                                                    startupStatus.setCheckName(Check.CHECK4);
                                                    startupStatus.setCheckFlag(checkFlag);
                                                    startupStatusList.Add(startupStatus);
                                                }
                                                break;
                                            case 4:
                                                if (startupStatusArray[0].Equals("Check4"))
                                                {
                                                    if (startupStatusArray[1].Equals("Pass"))
                                                    {
                                                        checkFlag = true;
                                                        checkCount++;
                                                    }
                                                    else
                                                    {
                                                        checkFlag = false;
                                                    }
                                                    startupStatus.setCheckId(5);
                                                    startupStatus.setCheckName(Check.CHECK5);
                                                    startupStatus.setCheckFlag(checkFlag);
                                                    startupStatusList.Add(startupStatus);
                                                }
                                                break;
                                            case 5:
                                                if (startupStatusArray[0].Equals("Check5"))
                                                {
                                                    if (startupStatusArray[1].Equals("Pass"))
                                                    {
                                                        checkFlag = true;
                                                        checkCount++;
                                                    }
                                                    else
                                                    {
                                                        checkFlag = false;
                                                    }
                                                    startupStatus.setCheckId(6);
                                                    startupStatus.setCheckName(Check.CHECK6);
                                                    startupStatus.setCheckFlag(checkFlag);
                                                    startupStatusList.Add(startupStatus);
                                                }
                                                break;
                                            case 6:
                                                if (startupStatusArray[0].Equals("Check6"))
                                                {
                                                    if (startupStatusArray[1].Equals("Pass"))
                                                    {
                                                        checkFlag = true;
                                                        checkCount++;
                                                    }
                                                    else
                                                    {
                                                        checkFlag = false;
                                                    }
                                                    startupStatus.setCheckId(7);
                                                    startupStatus.setCheckName(Check.CHECK7);
                                                    startupStatus.setCheckFlag(checkFlag);
                                                    startupStatusList.Add(startupStatus);
                                                }
                                                break;
                                            case 7:
                                                if (startupStatusArray[0].Equals("Check7"))
                                                {
                                                    if (startupStatusArray[1].Equals("Pass"))
                                                    {
                                                        checkFlag = true;
                                                        checkCount++;
                                                    }
                                                    else
                                                    {
                                                        checkFlag = false;
                                                    }
                                                    startupStatus.setCheckId(8);
                                                    startupStatus.setCheckName(Check.CHECK8);
                                                    startupStatus.setCheckFlag(checkFlag);
                                                    startupStatusList.Add(startupStatus);
                                                }
                                                break;
                                            case 8:
                                                if (startupStatusArray[0].Equals("Check8"))
                                                {
                                                    if (startupStatusArray[1].Equals("Pass"))
                                                    {
                                                        checkFlag = true;
                                                        checkCount++;
                                                    }
                                                    else
                                                    {
                                                        checkFlag = false;
                                                    }
                                                    startupStatus.setCheckId(9);
                                                    startupStatus.setCheckName(Check.CHECK9);
                                                    startupStatus.setCheckFlag(checkFlag);
                                                    startupStatusList.Add(startupStatus);
                                                }
                                                break;
                                            default:
                                                break;
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            return startupStatusList;
        }

        /**
         * 辨識引擎啟動驗證
         * 
         * @author daniel
         *
         * @param startupStatusPath :(enginePath+"/Status.Startup.eGroup)
         * @return 11項啟動檢查
         */
        public List<StartupStatus> recognizeMainStartup4(String enginePath, String modelPath, String outputFacePath, String outputFramePath,
            String showTrainFacePath, String trainFacePath, String startupStatusPath, long waitTimeMs)
        {
            // init func
            AttributeCheck attributeCheck = new AttributeCheck();
            // init variable
            List<StartupStatus> startupStatusList = new List<StartupStatus>();
            StartupStatus startupStatus = new StartupStatus();
            bool checkFlag = true;

            // // 檢查初始化設定-路徑
            if (attributeCheck.stringsNotNull(new string[] { enginePath, modelPath, outputFacePath, outputFramePath, showTrainFacePath, trainFacePath }))
            {
                // init variable
                FileInfo engineFolder = new FileInfo(enginePath);
                FileInfo modelFolder = new FileInfo(modelPath);
                FileInfo outputFaceFolder = new FileInfo(outputFacePath);
                FileInfo outputFrameFolder = new FileInfo(outputFramePath);
                FileInfo showTrainFaceFolder = new FileInfo(showTrainFacePath);
                FileInfo trainFaceFolder = new FileInfo(trainFacePath);

                // 檢查初始化設定-資料夾
                if (!(engineFolder.Exists && modelFolder.Exists && outputFaceFolder.Exists && outputFrameFolder.Exists && showTrainFaceFolder.Exists
                    && trainFaceFolder.Exists))
                {
                    checkFlag = false;
                }
            }
            else
            {
                checkFlag = false;
            }
            startupStatus.setCheckId(0);
            startupStatus.setCheckFlag(checkFlag);
            startupStatus.setCheckName(Check.CHECK0);
            startupStatusList.Add(startupStatus);

            if (checkFlag)
            {
                if (attributeCheck.stringsNotNull(startupStatusPath))
                {
                    // init func
                    TxtUtil txtUtil = new TxtUtil();
                    // init variable
                    FileInfo startupStatusFile = new FileInfo(startupStatusPath);
                    int checkCount = 0;

                    try
                    {
                        while (true)
                        {
                            if ((startupStatusFile.Exists && startupStatusFile.Length > 0 ) || waitCount == 10)
                            {
                                break;
                            }
                            Thread.Sleep(200);
                        }
                    }
                    catch (ThreadInterruptedException e)
                    {
                        Console.WriteLine(JsonConvert.SerializeObject(e.ToString()));
                        Thread.CurrentThread.Interrupt();
                    }

                    // 取得啟動的Log結果
                    if (startupStatusFile.Exists && startupStatusFile.Length > 0)
                    {
                        List<String> startupStatusLineList = txtUtil.read_lineList(startupStatusPath, Charsets.BIG5);
                        if (attributeCheck.listNotEmpty(startupStatusLineList))
                        {
                            // 文字檔資料路徑
                            foreach (String startupStatusLine in startupStatusLineList)
                            {
                                // 如果目前驗證階段皆Pass，繼續驗證下一階段，如果Fail則中斷此迴圈
                                if (checkFlag)
                                {
                                    // init variable
                                    startupStatus = new StartupStatus();
                                    String[] startupStatusArray = startupStatusLine.Split('\t');
                                    switch (startupStatusArray.Length)
                                    {
                                        case 3: // 階段處理成功後訊息陣列長度為3，內容範例 : Check0 Pass(or
                                                // Fail) 2019-11-04 15:00:56
                                            switch (checkCount)
                                            {
                                                case 0: // 驗證金鑰
                                                    if (startupStatusArray[0].Equals("Check0"))
                                                    {
                                                        if (startupStatusArray[1].Equals("Pass"))
                                                        {
                                                            checkFlag = true;
                                                            checkCount++;
                                                        }
                                                        else
                                                        {
                                                            checkFlag = false;
                                                        }
                                                        startupStatus.setCheckId(1);
                                                        startupStatus.setCheckName(Check.CHECK1);
                                                        startupStatus.setCheckFlag(checkFlag);
                                                        startupStatusList.Add(startupStatus);
                                                    }
                                                    break;
                                                case 1:
                                                    if (startupStatusArray[0].Equals("Check1"))
                                                    {
                                                        if (startupStatusArray[1].Equals("Pass"))
                                                        {
                                                            checkFlag = true;
                                                            checkCount++;
                                                        }
                                                        else
                                                        {
                                                            checkFlag = false;
                                                        }
                                                        startupStatus.setCheckId(2);
                                                        startupStatus.setCheckName(Check.CHECK2);
                                                        startupStatus.setCheckFlag(checkFlag);
                                                        startupStatusList.Add(startupStatus);
                                                    }
                                                    break;
                                                case 2:
                                                    if (startupStatusArray[0].Equals("Check2"))
                                                    {
                                                        if (startupStatusArray[1].Equals("Pass"))
                                                        {
                                                            checkFlag = true;
                                                            checkCount++;
                                                        }
                                                        else
                                                        {
                                                            checkFlag = false;
                                                        }
                                                        startupStatus.setCheckId(3);
                                                        startupStatus.setCheckName(Check.CHECK3);
                                                        startupStatus.setCheckFlag(checkFlag);
                                                        startupStatusList.Add(startupStatus);
                                                    }
                                                    break;
                                                case 3:
                                                    if (startupStatusArray[0].Equals("Check3"))
                                                    {
                                                        if (startupStatusArray[1].Equals("Pass"))
                                                        {
                                                            checkFlag = true;
                                                            checkCount++;
                                                        }
                                                        else
                                                        {
                                                            checkFlag = false;
                                                        }
                                                        startupStatus.setCheckId(4);
                                                        startupStatus.setCheckName(Check.CHECK4);
                                                        startupStatus.setCheckFlag(checkFlag);
                                                        startupStatusList.Add(startupStatus);
                                                    }
                                                    break;
                                                case 4:
                                                    if (startupStatusArray[0].Equals("Check4"))
                                                    {
                                                        if (startupStatusArray[1].Equals("Pass"))
                                                        {
                                                            checkFlag = true;
                                                            checkCount++;
                                                        }
                                                        else
                                                        {
                                                            checkFlag = false;
                                                        }
                                                        startupStatus.setCheckId(5);
                                                        startupStatus.setCheckName(Check.CHECK5);
                                                        startupStatus.setCheckFlag(checkFlag);
                                                        startupStatusList.Add(startupStatus);
                                                    }
                                                    break;
                                                case 5:
                                                    if (startupStatusArray[0].Equals("Check5"))
                                                    {
                                                        if (startupStatusArray[1].Equals("Pass"))
                                                        {
                                                            checkFlag = true;
                                                            checkCount++;
                                                        }
                                                        else
                                                        {
                                                            checkFlag = false;
                                                        }
                                                        startupStatus.setCheckId(6);
                                                        startupStatus.setCheckName(Check.CHECK6);
                                                        startupStatus.setCheckFlag(checkFlag);
                                                        startupStatusList.Add(startupStatus);
                                                    }
                                                    break;
                                                case 6:
                                                    if (startupStatusArray[0].Equals("Check6"))
                                                    {
                                                        if (startupStatusArray[1].Equals("Pass"))
                                                        {
                                                            checkFlag = true;
                                                            checkCount++;
                                                        }
                                                        else
                                                        {
                                                            checkFlag = false;
                                                        }
                                                        startupStatus.setCheckId(7);
                                                        startupStatus.setCheckName(Check.CHECK7);
                                                        startupStatus.setCheckFlag(checkFlag);
                                                        startupStatusList.Add(startupStatus);
                                                    }
                                                    break;
                                                case 7:
                                                    if (startupStatusArray[0].Equals("Check7"))
                                                    {
                                                        if (startupStatusArray[1].Equals("Pass"))
                                                        {
                                                            checkFlag = true;
                                                            checkCount++;
                                                        }
                                                        else
                                                        {
                                                            checkFlag = false;
                                                        }
                                                        startupStatus.setCheckId(8);
                                                        startupStatus.setCheckName(Check.CHECK8);
                                                        startupStatus.setCheckFlag(checkFlag);
                                                        startupStatusList.Add(startupStatus);
                                                    }
                                                    break;
                                                case 8:
                                                    if (startupStatusArray[0].Equals("Check8"))
                                                    {
                                                        if (startupStatusArray[1].Equals("Pass"))
                                                        {
                                                            checkFlag = true;
                                                            checkCount++;
                                                        }
                                                        else
                                                        {
                                                            checkFlag = false;
                                                        }
                                                        startupStatus.setCheckId(9);
                                                        startupStatus.setCheckName(Check.CHECK9);
                                                        startupStatus.setCheckFlag(checkFlag);
                                                        startupStatusList.Add(startupStatus);
                                                    }
                                                    break;
                                                case 9:
                                                    if (startupStatusArray[0].Equals("Check9"))
                                                    {
                                                        if (startupStatusArray[1].Equals("Pass"))
                                                        {
                                                            checkFlag = true;
                                                            checkCount++;
                                                        }
                                                        else
                                                        {
                                                            checkFlag = false;
                                                        }
                                                        startupStatus.setCheckId(10);
                                                        startupStatus.setCheckName(Check.CHECK10);
                                                        startupStatus.setCheckFlag(checkFlag);
                                                        startupStatusList.Add(startupStatus);
                                                    }
                                                    break;
                                                default:
                                                    break;
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return startupStatusList;
        }

        /**
         * 訓練人臉驗證
         * 
         * @author daniel
         *
         * @param trainResultPath
         * @return
         */
        public TrainResult trainFace(String trainResultPath)
        {
            // init func
            AttributeCheck attributeCheck = new AttributeCheck();
            // init variable
            TrainResult trainResult = new TrainResult();
            if (attributeCheck.stringsNotNull(trainResultPath))
            {
                // init func
                TxtUtil txtUtil = new TxtUtil();
                // init variable
                FileInfo trainResultFile = new FileInfo(trainResultPath);

                try
                {
                    while (true)
                    {
                        if ((trainResultFile.Exists && trainResultFile.Length > 0) || waitCount == 10)
                        {
                            break;
                        }
                        Thread.Sleep(250);
                        Console.WriteLine("訓練進行中，請稍等......");
                        waitCount++;
                    }
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(e.ToString()));
                    Thread.CurrentThread.Interrupt();
                }

                // Get the trainFace result
                List<String> trainResultLineList = txtUtil.read_lineList(trainResultPath, Charsets.BIG5);
                if (attributeCheck.listNotEmpty(trainResultLineList))
                {
                    // init variable
                    List<TrainInfo> trainInfoList = new List<TrainInfo>();
                    TrainInfo trainInfo = new TrainInfo();

                    foreach (String trainResultLine in trainResultLineList)
                    {
                        String[] trainArray = trainResultLine.Split('\t');
                        switch (trainArray.Length)
                        {
                            case 9:
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
                                trainInfo.setFaceQuality(trainArray[4].ToLower().Contains("pass") ? true : false);
                                trainInfo.setFaceQualityBlurness(trainArray[5].ToLower().Contains("pass") ? true : false);
                                trainInfo.setFaceQualityLowLuminance(trainArray[6].ToLower().Contains("pass") ? true : false);
                                trainInfo.setFaceQualityHighLuminance(trainArray[7].ToLower().Contains("pass") ? true : false);
                                trainInfo.setFaceQualityHeadpose(trainArray[8].ToLower().Contains("pass") ? true : false);
                                trainInfoList.Add(trainInfo);
                                trainInfo = new TrainInfo();
                                break;
                            case 5:
                                if (trainArray[1].Equals("Fail"))
                                {
                                    trainResult.getFailFacePathList().Add(trainArray[2]);
                                }
                                trainInfo.setFacePath(trainArray[3]);
                                trainInfo.setStatus(trainArray[2]);
                                trainInfo.setTime(trainArray[0]);
                                trainInfo.setPersonId(trainArray[4]);
                                trainInfoList.Add(trainInfo);
                                trainInfo = new TrainInfo();
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
                                    trainResult.setProcessingTime(trainArray[0].ToString().Replace("Processing Time: ", "").Replace(" sec. ", ""));
                                }
                                else
                                {
                                    trainResult.setAvgPprocessingTime(trainArray[0].ToString().Replace("AVG processing speed = ", "").Replace(" image/sec", ""));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    trainResult.setTrainInfoList(trainInfoList);
                }
                else
                {
                    trainResult.setTrainResultFileExist(false);
                }
            }
            return trainResult;
        }

        /**
         * 模型合併驗證
         * 
         * @author daniel
         *
         * @param modelAppend_path : modelAppend.getEnginePath()+"/Status.ModelAppend.eGroup"
         * @return
         */
        public ModelAppendResult modelAppend(String modelAppendPath, long waitTimeMs)
        {
            // init func
            AttributeCheck attributeCheck = new AttributeCheck();
            // init variable
            ModelAppendResult modelAppendResult = new ModelAppendResult();

            if (attributeCheck.stringsNotNull(modelAppendPath))
            {
                // init func
                TxtUtil txtUtil = new TxtUtil();
                // init variable
                FileInfo modelAppendFile = new FileInfo(modelAppendPath);
                int waitCount = 0;

                try
                {
                    while (true)
                    {
                        if ((modelAppendFile.Exists && modelAppendFile.Length > 0) || waitCount == 5)
                        {
                            break;
                        }
                        waitCount++;
                        Thread.Sleep((int)(waitTimeMs / 5));
                    }
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(e));
                    Thread.CurrentThread.Interrupt();
                }

                // Get the model append log
                List<String> modelAppendLineList = txtUtil.read_lineList(modelAppendPath, Charsets.UTF8);
                if (attributeCheck.listNotEmpty(modelAppendLineList))
                {
                    // init variable
                    List<ModelAppendInfo> modelAppendInfoList = new List<ModelAppendInfo>();
                    ModelAppendInfo modelAppendInfo = new ModelAppendInfo();

                    foreach (String modelAppendLine in modelAppendLineList)
                    {
                        // init variable
                        String[] modelAppendArray = modelAppendLine.Split('\t');
                        switch (modelAppendArray.Length)
                        {
                            case 5:
                                if (modelAppendArray[2].Contains("DBSize"))
                                {
                                    modelAppendInfo.setDBSizeCheckStatus(modelAppendArray[1]);
                                    modelAppendInfo.setDBSize(int.Parse(modelAppendArray[2].Substring(7, modelAppendArray[2].Length).Trim()));
                                    modelAppendInfo.setDBFaceDBPath(modelAppendArray[3]);
                                    modelAppendInfo.setSucess(true);
                                    modelAppendInfoList.Add(modelAppendInfo);
                                    modelAppendInfo = new ModelAppendInfo();
                                }
                                break;
                            case 4:
                                if (modelAppendArray[1].Equals("Fail"))
                                {
                                    modelAppendInfo.setSucess(false);
                                    modelAppendInfo.setErrorMessage(modelAppendArray[2]);
                                    modelAppendInfoList.Add(modelAppendInfo);
                                    modelAppendInfo = new ModelAppendInfo();
                                }
                                if (modelAppendArray[2].Equals("Parsingfile"))
                                {
                                    modelAppendResult.setModelListPath(modelAppendArray[3].Replace("file: ", ""));
                                    modelAppendResult.setModelListCheckStatus(modelAppendArray[1]);
                                }
                                else if (modelAppendArray[2].Equals("WorkingFolder"))
                                {
                                    string str = modelAppendArray[3].Replace("Not enough space to working folder (", "").Substring(1, modelAppendArray[3].IndexOf("GB"));
                                    modelAppendInfo.setWorkingFolderCheckStatus(modelAppendArray[1]);

                                    //int i1 = modelAppendArray[3].IndexOf("GB");
                                    //string s1 = modelAppendArray[3].Replace("Not enough space to working folder (", "");
                                    //string s2 = modelAppendArray[3].Substring(1, modelAppendArray[3].IndexOf("GB")-1);
                                    modelAppendInfo.setWorkingFolderSize(Double.Parse(
                                    modelAppendArray[3].Replace("Not enough space to working folder (", "").Substring(1, modelAppendArray[3].IndexOf("GB") - 1)));
                                    modelAppendInfo.setWorkingFolderStatus(
                                        modelAppendArray[3].Substring((modelAppendArray[3].IndexOf("GB") + 3), (modelAppendArray[3].LastIndexOf("("))).Trim());
                                }
                                else if (modelAppendArray[2].Equals("OutputFaceDB"))
                                {
                                    modelAppendInfo.setOutputFaceDBCheckStatus(modelAppendArray[1]);
                                    modelAppendInfo.setOutputFaceDBSize(Double.Parse(
                                        modelAppendArray[3].Replace("Not enough space to Output Binary file (", "").Substring(1, modelAppendArray[3].IndexOf("GB") - 1)));
                                    modelAppendInfo.setOutputFaceDBStatus(
                                        modelAppendArray[3].Substring((modelAppendArray[3].IndexOf("GB") + 3), (modelAppendArray[3].LastIndexOf("("))).Trim());
                                }
                                break;
                            case 1:
                                if (modelAppendArray[0].StartsWith("Total faces: "))
                                {
                                    int i1 = modelAppendArray[0].IndexOf("Total faces: ") + 12;
                                    int i2 = modelAppendArray[0].IndexOf("in the appended new model");
                                    string s1 = modelAppendArray[0].Substring(i1, i2 - i1);
                                    modelAppendResult.setTotalFaceCount(int.Parse(s1));
                                }
                                else
                                {
                                    int i1 = modelAppendArray[0].IndexOf("/");
                                    int i2 = modelAppendArray[0].IndexOf(" of models appended failed".Trim());
                                    string s1 = modelAppendArray[0].Substring(i1 + 1, i2 - i1 - 1);
                                    modelAppendResult.setAppendPassCount(int.Parse(modelAppendArray[0].Substring(0, modelAppendArray[0].IndexOf("of models appended pass")).Trim()));
                                    modelAppendResult.setAppendFailCount(int.Parse(s1));
                                }
                                break;
                            default:
                                break;
                        }

                    }
                    // set the model append result info
                    modelAppendResult.setModelAppendInfoList(modelAppendInfoList);
                }
            }
            return modelAppendResult;
        }

        /**
         * 模型替換
         * 
         * @author daniel
         *
         * @param modelSwitchStatusPath
         * @param waitTimeMs
         * @return
         */
        public ModelSwitchResult modelSwitch(String modelSwitchStatusPath)
        {
            // init func
            AttributeCheck attributeCheck = new AttributeCheck();
            // init variable
            ModelSwitchResult modelSwitchResult = new ModelSwitchResult();

            if (attributeCheck.stringsNotNull(modelSwitchStatusPath))
            {
                // init variable
                FileInfo modelSwitchStatusFile = new FileInfo(modelSwitchStatusPath);
                // init func
                TxtUtil txtUtil = new TxtUtil();
                // init variable
                Boolean flag = true;

                try
                {
                    while (true)
                    {
                        if ((modelSwitchStatusFile.Exists && modelSwitchStatusFile.Length > 0) || waitCount == 10)
                        {
                            break;
                        }
                        waitCount++;
                        Thread.Sleep(200);
                    }
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(e));
                    Thread.CurrentThread.Interrupt();
                }
                List<String> modelSwitchLineList = txtUtil.read_lineList(modelSwitchStatusPath, Charsets.UTF8);
                String faceDBPath = "";
                if (attributeCheck.listNotEmpty(modelSwitchLineList))
                {
                    foreach (String modelSwitchLine in modelSwitchLineList)
                    {
                        String[] modelSwitchArray = modelSwitchLine.Split('\t');
                        if (modelSwitchArray.Length > 1)
                        {
                            if (modelSwitchArray[1].Equals("Fail"))
                            {
                                faceDBPath = modelSwitchArray[3].Replace("Reload FaceDB file ", "");
                                modelSwitchResult.setFaceDB(faceDBPath.Substring(faceDBPath.LastIndexOf("\\") + 1, faceDBPath.Length));
                                flag = false;
                            }
                            else if (modelSwitchArray[1].Equals("Pass") && modelSwitchArray.Length == 4)
                            {
                                faceDBPath = modelSwitchArray[3].Replace("Reload FaceDB file ", "");
                                modelSwitchResult.setFaceDB(faceDBPath.Substring(faceDBPath.LastIndexOf("\\") + 1, faceDBPath.Length));
                            }
                            else if (modelSwitchArray[1].Equals("Report"))
                            {
                                if (modelSwitchArray[3].StartsWith("Overall reload:"))
                                {
                                    modelSwitchResult.setFaceReload(modelSwitchArray[3]);
                                }
                                else
                                {
                                    modelSwitchResult.setReloadTime(modelSwitchArray[3]);
                                }
                            }
                        }
                    }
                }
                else
                {
                    flag = false;
                }
                modelSwitchResult.setSuccess(flag);
            }
            return modelSwitchResult;
        }

        /**
         * 模型熱傳
         * 
         * @author daniel
         *
         * @param modelInsertStatuspath (modelInsert.getEnginePath()+"\\Status.ModelInsert.eGroup")
         * @return
         */
        public ModelInsertResult modelInsert(String modelInsertStatusPath, long waitTimeMs)
        {
            // init func
            AttributeCheck attributeCheck = new AttributeCheck();
            // init variable
            ModelInsertResult modelInsertResult = new ModelInsertResult();
            if (attributeCheck.stringsNotNull(modelInsertStatusPath))
            {
                // init variable
                FileInfo modelInsertStatusFile = new FileInfo(modelInsertStatusPath);
                TxtUtil txtUtil = new TxtUtil();
                // init variable
                try
                {
                    while (true)
                    {
                        if ((modelInsertStatusFile.Exists && modelInsertStatusFile.Length > 0) || waitCount == 10)
                        {
                            break;
                        }
                        waitCount++;
                        Thread.Sleep((int)(waitTimeMs / 5));
                    }
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(e));
                    Thread.CurrentThread.Interrupt();
                }

                List<String> modelInsertLineList = txtUtil.read_lineList(modelInsertStatusPath, Charsets.UTF8);
                if (attributeCheck.listNotEmpty(modelInsertLineList))
                {
                    // init variable
                    List<ModelInsertInfo> modelInsertInfoList = new List<ModelInsertInfo>();
                    ModelInsertInfo modelInsertInfo;
                    String[] faceAndPeople;
                    String[] currentfaceAndPeople;
                    int face;
                    int people;
                    int currentDBFaceCout;
                    int currentDBPeopleCount;

                    foreach (String modelInsertLine in modelInsertLineList)
                    {
                        // init variable
                        modelInsertInfo = new ModelInsertInfo();
                        String[] modelInsertArray = modelInsertLine.Split('\t');

                        switch (modelInsertArray.Length)
                        {
                            case 3:
                                if (modelInsertArray[1].Equals("Pass"))
                                {
                                    modelInsertInfoList.Add(modelInsertInfo);
                                    modelInsertResult.setSuccess(true);
                                }
                                break;
                            case 5:
                                faceAndPeople = modelInsertArray[3].Replace("Overall insert:", "").Replace("faces/people", "").Trim().Split('/');
                                currentfaceAndPeople = modelInsertArray[4].Replace("CurrentDBFaceCout=", "").Replace("CurrentDBPeopleCount=", "").Trim().Split(' ');
                                face = int.Parse(faceAndPeople[0]);
                                people = int.Parse(faceAndPeople[1]);
                                currentDBFaceCout = int.Parse(currentfaceAndPeople[0]);
                                currentDBPeopleCount = int.Parse(currentfaceAndPeople[1]);

                                modelInsertInfo.setInsertFacesCount(face);
                                modelInsertInfo.setInsertPeopleCount(people);
                                modelInsertInfo.setCurrDBFaceCout(currentDBFaceCout);
                                modelInsertInfo.setCurrDBPeopleCount(currentDBPeopleCount);
                                modelInsertInfoList.Add(modelInsertInfo);
                                modelInsertResult.setSuccess(true);
                                break;
                            case 4:
                                if (modelInsertArray[3].StartsWith("Overall insert time: "))
                                {
                                    modelInsertInfo.setInsertProcessTime(modelInsertArray[3]
                                        .Substring(modelInsertArray[3].IndexOf("Overall insert time: "), modelInsertArray[3].IndexOf(" sec. ")).Trim());
                                }
                                modelInsertInfoList.Add(modelInsertInfo);
                                break;
                            default:
                                break;
                        }

                    }
                    modelInsertResult.setModelInsertInfoList(modelInsertInfoList);
                }
            }
            return modelInsertResult;
        }

    }
}