using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.entity
{
    public class RFIDFace
    {
        private String hasFound;
        private int faceSize;
        private int faceConsecutiveFrame;
        private int faceDurationFrame;

        public String getHasFound()
        {
            return hasFound;
        }
        public void setHasFound(String hasFound)
        {
            this.hasFound = hasFound;
        }
        public int getFaceSize()
        {
            return faceSize;
        }
        public void setFaceSize(int faceSize)
        {
            this.faceSize = faceSize;
        }
        public int getFaceConsecutiveFrame()
        {
            return faceConsecutiveFrame;
        }
        public void setFaceConsecutiveFrame(int faceConsecutiveFrame)
        {
            this.faceConsecutiveFrame = faceConsecutiveFrame;
        }
        public int getFaceDurationFrame()
        {
            return faceDurationFrame;
        }
        public void setFaceDurationFrame(int faceDurationFrame)
        {
            this.faceDurationFrame = faceDurationFrame;
        }

    }
}
