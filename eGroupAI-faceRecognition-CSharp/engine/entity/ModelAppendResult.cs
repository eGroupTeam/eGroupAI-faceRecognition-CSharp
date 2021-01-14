using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.entity
{
    public class ModelAppendResult
    {
        private String modelListCheckStatus;
        private String modelListPath;
        private List<ModelAppendInfo> modelAppendInfoList;
        private int appendPassCount;
        private int appendFailCount;
        private int totalFaceCount;
        // programe control
        private bool appendCmdSuccess = true;

        public String getModelListCheckStatus()
        {
            return modelListCheckStatus;
        }
        public void setModelListCheckStatus(String modelListCheckStatus)
        {
            this.modelListCheckStatus = modelListCheckStatus;
        }
        public String getModelListPath()
        {
            return modelListPath;
        }
        public void setModelListPath(String modelListPath)
        {
            this.modelListPath = modelListPath;
        }
        public List<ModelAppendInfo> getModelAppendInfoList()
        {
            if (modelAppendInfoList == null)
            {
                modelAppendInfoList = new List<ModelAppendInfo>();
            }
            return modelAppendInfoList;
        }
        public void setModelAppendInfoList(List<ModelAppendInfo> modelAppendInfoList)
        {
            this.modelAppendInfoList = modelAppendInfoList;
        }
        public int getAppendPassCount()
        {
            return appendPassCount;
        }
        public void setAppendPassCount(int appendPassCount)
        {
            this.appendPassCount = appendPassCount;
        }
        public int getAppendFailCount()
        {
            return appendFailCount;
        }
        public void setAppendFailCount(int appendFailCount)
        {
            this.appendFailCount = appendFailCount;
        }
        public bool isAppendCmdSuccess()
        {
            return appendCmdSuccess;
        }
        public void setAppendCmdSuccess(bool appendCmdSuccess)
        {
            this.appendCmdSuccess = appendCmdSuccess;
        }
        public int getTotalFaceCount()
        {
            return totalFaceCount;
        }
        public void setTotalFaceCount(int totalFaceCount)
        {
            this.totalFaceCount = totalFaceCount;
        }

    }
}
