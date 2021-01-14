using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.entity
{
    public class TrainInfo
    {
        private String time;
        private String status;
        private String facePath;
        private String personId;
        private bool isFaceQuality = false;
        private bool isFaceQualityBlurness = false;
        private bool isFaceQualityLowLuminance = false;
        private bool isFaceQualityHighLuminance = false;
        private bool isFaceQualityHeadpose = false;

        public String getTime()
        {
            return time;
        }
        public void setTime(String time)
        {
            this.time = time;
        }
        public String getStatus()
        {
            return status;
        }
        public void setStatus(String status)
        {
            this.status = status;
        }
        public String getFacePath()
        {
            return facePath;
        }
        public void setFacePath(String facePath)
        {
            this.facePath = facePath;
        }
        public String getPersonId()
        {
            return personId;
        }
        public void setPersonId(String personId)
        {
            this.personId = personId;
        }

        public bool getIsFaceQuality()
        {
            return isFaceQuality;
        }
        public void setFaceQuality(bool isFaceQuality)
        {
            this.isFaceQuality = isFaceQuality;
        }
        public bool getIsFaceQualityBlurness()
        {
            return isFaceQualityBlurness;
        }
        public void setFaceQualityBlurness(bool isFaceQualityBlurness)
        {
            this.isFaceQualityBlurness = isFaceQualityBlurness;
        }
        public bool getIsFaceQualityLowLuminance()
        {
            return isFaceQualityLowLuminance;
        }
        public void setFaceQualityLowLuminance(bool isFaceQualityLowLuminance)
        {
            this.isFaceQualityLowLuminance = isFaceQualityLowLuminance;
        }
        public bool getIsFaceQualityHighLuminance()
        {
            return isFaceQualityHighLuminance;
        }
        public void setFaceQualityHighLuminance(bool isFaceQualityHighLuminance)
        {
            this.isFaceQualityHighLuminance = isFaceQualityHighLuminance;
        }
        public bool getIsFaceQualityHeadpose()
        {
            return isFaceQualityHeadpose;
        }
        public void setFaceQualityHeadpose(bool isFaceQualityHeadpose)
        {
            this.isFaceQualityHeadpose = isFaceQualityHeadpose;
        }

    }
}
