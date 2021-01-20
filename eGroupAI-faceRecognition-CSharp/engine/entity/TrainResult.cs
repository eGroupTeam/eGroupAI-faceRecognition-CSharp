using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.entity
{
    public class TrainResult
    {
        [JsonProperty]
        private List<String> passFacePathList;
        [JsonProperty]
        private List<String> failFacePathList;
        [JsonProperty]
        private int fileSize;
        [JsonProperty]
        private int faceSize;
        [JsonProperty]
        private String processingTime;
        [JsonProperty]
        private String avgPprocessingTime;
        [JsonProperty]
        private List<TrainInfo> trainInfoList;
        // programe control
        [JsonProperty]
        private bool trainResultFileExist = true;
        [JsonProperty]
        private bool trainCmdSuccess = true;
        [JsonProperty]
        private bool trainStatus = false;
        [JsonProperty]
        private int trainSize;
        
        public bool isTrainResultFileExist()
        {
            return trainResultFileExist;
        }
        public void setTrainResultFileExist(bool trainResultFileExist)
        {
            this.trainResultFileExist = trainResultFileExist;
        }
        public List<String> getPassFacePathList()
        {
            if (passFacePathList == null)
            {
                passFacePathList = new List<String>();
            }
            return passFacePathList;
        }
        public void setPassFacePathList(List<String> passFacePathList)
        {
            this.passFacePathList = passFacePathList;
        }
        public List<String> getFailFacePathList()
        {
            if (failFacePathList == null)
            {
                failFacePathList = new List<String>();
            }
            return failFacePathList;
        }
        public void setFailFacePathList(List<String> failFacePathList)
        {
            this.failFacePathList = failFacePathList;
        }
        public int getFileSize()
        {
            return fileSize;
        }
        public void setFileSize(int fileSize)
        {
            this.fileSize = fileSize;
        }
        public int getFaceSize()
        {
            return faceSize;
        }
        public void setFaceSize(int faceSize)
        {
            this.faceSize = faceSize;
        }

        public String getProcessingTime()
        {
            return processingTime;
        }
        public void setProcessingTime(String processingTime)
        {
            this.processingTime = processingTime;
        }
        public String getAvgPprocessingTime()
        {
            return avgPprocessingTime;
        }
        public void setAvgPprocessingTime(String avgPprocessingTime)
        {
            this.avgPprocessingTime = avgPprocessingTime;
        }
        public List<TrainInfo> getTrainInfoList()
        {
            if (trainInfoList == null)
            {
                trainInfoList = new List<TrainInfo>();
            }
            return trainInfoList;
        }
        public void setTrainInfoList(List<TrainInfo> trainInfoList)
        {
            this.trainInfoList = trainInfoList;
        }
        public bool isTrainCmdSuccess()
        {
            return trainCmdSuccess;
        }
        public void setTrainCmdSuccess(bool trainCmdSuccess)
        {
            this.trainCmdSuccess = trainCmdSuccess;
        }
        public bool isTrainStatus()
        {
            return trainStatus;
        }
        public void setTrainStatus(bool trainStatus)
        {
            this.trainStatus = trainStatus;
        }
        public int getTrainSize()
        {
            return trainSize;
        }
        public void setTrainSize(int trainSize)
        {
            this.trainSize = trainSize;
        }

    }
}
