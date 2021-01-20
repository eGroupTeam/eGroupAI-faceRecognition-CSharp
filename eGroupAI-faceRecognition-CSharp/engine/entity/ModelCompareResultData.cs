using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.entity
{
    public class ModelCompareResultData
    {
        private String totalTime;
        private String startTime;
        private String endTime;
        private String imagePerSec;
        private int samPersonCount;
        private int notSamPersonCount;
        public String getTotalTime()
        {
            return totalTime;
        }
        public void setTotalTime(String totalTime)
        {
            this.totalTime = totalTime;
        }
        public String getStartTime()
        {
            return startTime;
        }
        public void setStartTime(String startTime)
        {
            this.startTime = startTime;
        }
        public String getEndTime()
        {
            return endTime;
        }
        public void setEndTime(String endTime)
        {
            this.endTime = endTime;
        }
        public String getImagePerSec()
        {
            return imagePerSec;
        }
        public void setImagePerSec(String imagePerSec)
        {
            this.imagePerSec = imagePerSec;
        }
        public int getSamPersonCount()
        {
            return samPersonCount;
        }
        public void setSamPersonCount(int samPersonCount)
        {
            this.samPersonCount = samPersonCount;
        }
        public int getNotSamPersonCount()
        {
            return notSamPersonCount;
        }
        public void setNotSamPersonCount(int notSamPersonCount)
        {
            this.notSamPersonCount = notSamPersonCount;
        }

    }
}
