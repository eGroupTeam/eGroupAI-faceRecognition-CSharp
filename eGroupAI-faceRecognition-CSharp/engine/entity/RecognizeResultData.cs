using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.entity
{
    public class RecognizeResultData
    {
        private int imageCount;
        private String imagePerSec;
        private String totalTime;
        private String startTime;
        private String endTime;

        public int getImageCount()
        {
            return imageCount;
        }
        public void setImageCount(int imageCount)
        {
            this.imageCount = imageCount;
        }
        public String getImagePerSec()
        {
            return imagePerSec;
        }
        public void setImagePerSec(String imagePerSec)
        {
            this.imagePerSec = imagePerSec;
        }
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

    }
}
