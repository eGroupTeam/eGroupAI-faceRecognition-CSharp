using eGroupAI_faceRecognition_CSharp.library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.entity
{
    public class RecognizeFace
    {
        public class RECOGNIZEMODE_
        {
            public static String LIVENESS { get { return "liveness"; } }
            public static String GENERAL { get { return "general"; } }
            public static String value;
            public static String getValue()
            {
                return value;
            }
        }

        private Double threshold;
        private String resolution;
        private String outputFramePath;
        private String outputFacePath;
        private String outputMotionFramePath;
        private String webcam;
        private String rtsp;
        private String videoPath;
        private String photoListPath;
        private int minimumFaceSize;
        private int threads;
        private String trainedFaceDBPath;
        private String jsonPath;
        private StringBuilder cli;
        private List<String> commandList;
        private String disk;
        private String enginePath;
        private bool isHideMainWindow = true;
        private bool isHideThreadWindow = true;
        private bool isTesting = false;
        private bool isIterationSearch = false;
        private bool isOnface = false;
        private int sampleRate;
        private String sectionId;
        private String mainResolution;
        // init program process
        private long responseTime;
        private bool isOutputFace;
        private bool isOutputFrame;
        // init func
        private AttributeCheck attributeCheck;

        public Double getThreshold()
        {
            return threshold;
        }

        public void setThreshold(Double threshold)
        {
            this.threshold = threshold;
        }

        public bool isHideMainWindow_()
        {
            return isHideMainWindow;
        }

        public void setHideMainWindow_(bool isHideMainWindow)
        {
            this.isHideMainWindow = isHideMainWindow;
        }

        public String getResolution()
        {
            return resolution;
        }

        public void setResolution(String resolution)
        {
            this.resolution = resolution;
        }

        public String getOutputFramePath()
        {
            return outputFramePath;
        }

        public void setOutputFramePath(String outputFramePath)
        {
            this.outputFramePath = outputFramePath;
        }

        public String getOutputFacePath()
        {
            return outputFacePath;
        }

        public void setOutputFacePath(String outputFacePath)
        {
            this.outputFacePath = outputFacePath;
        }

        public String getWebcam()
        {
            return webcam;
        }

        public void setWebcam(String webcam)
        {
            this.webcam = webcam;
        }

        public String getRtsp()
        {
            return rtsp;
        }

        public void setRtsp(String rtsp)
        {
            this.rtsp = rtsp;
        }

        public String getVideoPath()
        {
            return videoPath;
        }

        public void setVideoPath(String videoPath)
        {
            this.videoPath = videoPath;
        }

        public String getPhotoListPath()
        {
            return photoListPath;
        }

        public void setPhotoListPath(String photoListPath)
        {
            this.photoListPath = photoListPath;
        }

        public int getMinimumFaceSize()
        {
            return minimumFaceSize;
        }

        public void setMinimumFaceSize(int minimumFaceSize)
        {
            this.minimumFaceSize = minimumFaceSize;
        }

        public int getThreads()
        {
            return threads;
        }

        public void setThreads(int threads)
        {
            this.threads = threads;
        }

        public String getTrainedFaceDBPath()
        {
            return trainedFaceDBPath;
        }

        public void setTrainedFaceDBPath(String trainedFaceDBPath)
        {
            this.trainedFaceDBPath = trainedFaceDBPath;
        }
        public String getJsonPath()
        {
            return jsonPath;
        }

        public void setJsonPath(String jsonPath)
        {
            this.jsonPath = jsonPath;
        }

        public StringBuilder getCli()
        {
            return cli;
        }

        public void setCli(StringBuilder cli)
        {
            this.cli = cli;
        }

        public String getEnginePath()
        {
            return enginePath;
        }

        public void setEnginePath(String enginePath)
        {
            this.enginePath = enginePath;
        }

        public void generateCli()
        {
            if (attributeCheck == null)
            {
                attributeCheck = new AttributeCheck();
            }
            this.disk = enginePath.Substring(0, 1);
            if (attributeCheck.stringsNotNull(new string[] { enginePath, disk, trainedFaceDBPath, jsonPath }))//trainedFaceDBPath
            {
                String inputSource = " --cam " + webcam;
                if (attributeCheck.stringsNotNull(new string[] { rtsp }))
                {
                    inputSource = " --rtsp " + rtsp;
                }
                else if (attributeCheck.stringsNotNull(new string[] { videoPath }))
                {
                    inputSource = " --video " + videoPath;
                }
                else if (attributeCheck.stringsNotNull(new string[] { photoListPath }))
                {
                    inputSource = " --photo-list " + photoListPath;
                }

                //isHideMainWindow = false;
                //isHideThreadWindow = false;
                cli = new StringBuilder("cd " + enginePath + " && " + disk + ": && RecognizeFace " + "--threshold " + threshold + " "
                    + (isHideMainWindow == false ? " --show-main-window " : "") + (isHideThreadWindow == false ? " --show-thread-window " : "")
                    + (attributeCheck.stringsNotNull(new string[] { resolution }) ? " --resolution " + resolution + " " : "--resolution 720p ")
                    + (isOutputFrame == false || !attributeCheck.stringsNotNull(new string[] { outputFramePath }) ? "" : " --output-frame \"" + outputFramePath + "\" ")
                    + (isOutputFace == false || !attributeCheck.stringsNotNull(new string[] { outputFacePath }) ? "" : " --output-face \"" + outputFacePath + "\" ")
                    + (!attributeCheck.stringsNotNull(new string[] { outputFacePath }) ? "" : "--output-face \"" + outputFacePath + "\" ") + inputSource + " "
                    + (minimumFaceSize != 0 ? "--minimum-face-size " + minimumFaceSize + " " : "")
                    + (attributeCheck.stringsNotNull(new string[] { mainResolution }) ? "--output-window-resolution " + mainResolution + " " : "")
                    + (threads != 0 ? "--threads " + threads + " " : "--threads 1 ")
                    + (sampleRate != 0 ? "--sample-rate " + sampleRate + " " : "--sample-rate 5 ") + (isTesting == true ? "--no-imageqa " : "")
                    + (isIterationSearch == true ? "--enable-iteration-search " : "") + (isOnface == true ? "--one-face " : "") + " \"" + trainedFaceDBPath
                    + "\" \"" + jsonPath + "\"");

                //cli = new StringBuilder("cd " + enginePath + " && RecognizeFace " + "--threshold " + threshold + " --resolution 720p " + " --output-frame \"" + outputFramePath + "\" " + " --output-face \"" + outputFacePath + "\" " + inputSource + " "+ "--minimum-face-size " + minimumFaceSize + " --threads " + threads + " \"" + trainedFaceDBPath + "\" " + "\"" + jsonPath + "\"");
            }
            else
            {
                cli = null;
            }
            //LOGGER.info("RecognizeFace cli : " + cli);
        }

        public void getStopCli(RECOGNIZEMODE_ recognizeMode_)
        {
            if (attributeCheck == null)
            {
                attributeCheck = new AttributeCheck();
            }
            this.disk = enginePath.Substring(0, 1);
            if (attributeCheck.stringsNotNull(new string[] { enginePath, disk }))
            {
                if (!(recognizeMode_.ToString() == "liveness"))
                {
                    cli = new StringBuilder("cd " + enginePath + " && " + disk + ": && StopRecognize.bat");
                }
                else
                {
                    cli = new StringBuilder("cd " + enginePath + " && " + disk + ": && StopLiveness.bat");
                }
            }
            else
            {
                cli = null;
            }
            //LOGGER.info("RecognizeFace cli : " + cli);
        }

        public List<String> getCommandList()
        {
            if (attributeCheck == null)
            {
                attributeCheck = new AttributeCheck();
            }
            if (attributeCheck.stringsNotNull(new string[] { cli.ToString() }))
            {
                commandList = new List<String>();
                commandList.Add("cmd");
                commandList.Add("/C");
                commandList.Add(disk + ": && " + cli.ToString().Replace("/", "/"));
                //commandList.Add(cli.ToString().Replace("/", "//"));
            }
            return commandList;
        }

        public void setCommandList(List<String> commandList)
        {
            this.commandList = commandList;
        }

        public String getDisk()
        {
            return disk;
        }

        public void setDisk(String disk)
        {
            this.disk = disk;
        }

        public bool getIsHideThreadWindow()
        {
            return isHideThreadWindow;
        }

        public void setHideThreadWindow(bool isHideThreadWindow)
        {
            this.isHideThreadWindow = isHideThreadWindow;
        }

        public Int64 getResponseTime()
        {
            return responseTime;
        }

        public void setResponseTime(long responseTime)
        {
            this.responseTime = responseTime;
        }

        public String getSectionId()
        {
            return sectionId;
        }

        public void setSectionId(String sectionId)
        {
            this.sectionId = sectionId;
        }

        public int getSampleRate()
        {
            return sampleRate;
        }

        public void setSampleRate(int sampleRate)
        {
            this.sampleRate = sampleRate;
        }

        public String getOutputMotionFramePath()
        {
            return outputMotionFramePath;
        }

        public void setOutputMotionFramePath(String outputMotionFramePath)
        {
            this.outputMotionFramePath = outputMotionFramePath;
        }

        public String getMainResolution()
        {
            return mainResolution;
        }

        public void setMainResolution(String mainResolution)
        {
            this.mainResolution = mainResolution;
        }

        public bool getIsOutputFrame()
        {
            return isOutputFrame;
        }

        public void setOutputFrame(bool isOutputFrame)
        {
            this.isOutputFrame = isOutputFrame;
        }

        public bool getIsTesting()
        {
            return isTesting;
        }

        public void setTesting(bool isTesting)
        {
            this.isTesting = isTesting;
        }

        public bool getIsOnface()
        {
            return isOnface;
        }

        public void setOnface(bool isOnface)
        {
            this.isOnface = isOnface;
        }

        public bool getIsIterationSearch()
        {
            return isIterationSearch;
        }

        public void setIterationSearch(bool isIterationSearch)
        {
            this.isIterationSearch = isIterationSearch;
        }

        public bool getIsOutputFace()
        {
            return isOutputFace;
        }

        public void setOutputFace(bool isOutputFace)
        {
            this.isOutputFace = isOutputFace;
        }

    }
}