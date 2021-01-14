using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.entity
{
    public class FrameFace
    {
        private String frameFaceNo;
        private String frameFacePath;

        public String getFrameFaceNo()
        {
            return frameFaceNo;
        }
        public void setFrameFaceNo(String frameFaceNo)
        {
            this.frameFaceNo = frameFaceNo;
        }
        public String getFrameFacePath()
        {
            return frameFacePath;
        }
        public void setFrameFacePath(String frameFacePath)
        {
            this.frameFacePath = frameFacePath;
        }
    }
}
