using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.entity
{
    public class ModelInsertResult
    {
        List<ModelInsertInfo> modelInsertInfoList = new List<ModelInsertInfo>();
        private Boolean isSuccess;
        private String faceReload;
        private String reloadTime;

        public List<ModelInsertInfo> getModelInsertInfoList()
        {
            return modelInsertInfoList;
        }

        public void setModelInsertInfoList(List<ModelInsertInfo> modelInsertInfoList)
        {
            this.modelInsertInfoList = modelInsertInfoList;
        }


        public Boolean getIsSuccess()
        {
            return isSuccess;
        }

        public void setSuccess(Boolean isSuccess)
        {
            this.isSuccess = isSuccess;
        }

        public String getFaceReload()
        {
            return faceReload;
        }

        public void setFaceReload(String faceReload)
        {
            this.faceReload = faceReload;
        }

        public String getReloadTime()
        {
            return reloadTime;
        }

        public void setReloadTime(String reloadTime)
        {
            this.reloadTime = reloadTime;
        }

    }
}
