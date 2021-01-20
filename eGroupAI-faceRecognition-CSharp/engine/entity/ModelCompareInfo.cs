using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.entity
{
    public class ModelCompareInfo
    {
        private String filePath1;
        private String filePath2;
        private String isSamPerson;
        private String confidence;

        public String getFilePath1()
        {
            return filePath1;
        }
        public void setFilePath1(String filePath1)
        {
            this.filePath1 = filePath1;
        }
        public String getFilePath2()
        {
            return filePath2;
        }
        public void setFilePath2(String filePath2)
        {
            this.filePath2 = filePath2;
        }
        public String getIsSamPerson()
        {
            return isSamPerson;
        }
        
        public void setIsSamPerson(String isSamPerson)
        {
            this.isSamPerson = isSamPerson;
        }
        public String getConfidence()
        {
            return confidence;
        }
        public void setConfidence(String confidence)
        {
            this.confidence = confidence;
        }
    }
}