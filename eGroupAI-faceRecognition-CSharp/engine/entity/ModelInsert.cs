using eGroupAI_faceRecognition_CSharp.library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.entity
{
    public class ModelInsert
    {
        private String ListPath;
        private String enginePath;
        private List<String> modelBinaryList = new List<String>();
        private List<String> modelFaceInfoList = new List<String>();
        private List<String> faceDBList;
        // init func
        private AttributeCheck attributeCheck;


        public String getEnginePath()
        {
            return enginePath;
        }
        public void setEnginePath(String enginePath)
        {
            this.enginePath = enginePath;
        }
        public String getListPath()
        {
            return ListPath;
        }
        public void setListPath(String listPath)
        {
            ListPath = listPath;
        }
        public List<String> getModelBinaryList()
        {
            return modelBinaryList;
        }
        public void setModelBinaryList(List<String> modelBinaryList)
        {
            this.modelBinaryList = modelBinaryList;
        }
        public List<String> getModelFaceInfoList()
        {
            return modelFaceInfoList;
        }
        public void setModelFaceInfoList(List<String> modelFaceInfoList)
        {
            this.modelFaceInfoList = modelFaceInfoList;
        }

        public List<String> getFaceDBList()
        {
            if (attributeCheck == null)
            {
                attributeCheck = new AttributeCheck();
            }
            if (!attributeCheck.listNotEmpty(faceDBList))
            {
                faceDBList = new List<String>();
            }
            return faceDBList;
        }

        public void setFaceDBList(List<String> faceDBList)
        {
            this.faceDBList = faceDBList;
        }
    }
}
