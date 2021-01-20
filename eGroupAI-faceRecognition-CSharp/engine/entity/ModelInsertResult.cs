using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.entity
{
    public class ModelInsertResult
    {
        [JsonProperty]
        List<ModelInsertInfo> modelInsertInfoList = new List<ModelInsertInfo>();
        [JsonProperty]
        private Boolean isSuccess;
        [JsonProperty]
        private String faceReload;
        [JsonProperty]
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
