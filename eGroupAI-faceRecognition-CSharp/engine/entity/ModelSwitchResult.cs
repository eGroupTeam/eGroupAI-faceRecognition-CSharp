using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.entity
{
    public class ModelSwitchResult
    {

        private bool isCheckBinaryPass;
        private bool isCheckFaceInfoPass;
        private bool isSuccess;
        private String faceReload;
        private String reloadTime;
        private String faceDB;

        public bool getIsCheckBinaryPass()
        {
            return isCheckBinaryPass;
        }
        public void setCheckBinaryPass(bool isCheckBinaryPass)
        {
            this.isCheckBinaryPass = isCheckBinaryPass;
        }
        public bool getIsCheckFaceInfoPass()
        {
            return isCheckFaceInfoPass;
        }
        public void setCheckFaceInfoPass(bool isCheckFaceInfoPass)
        {
            this.isCheckFaceInfoPass = isCheckFaceInfoPass;
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
        public bool getIsSuccess()
        {
            return isSuccess;
        }
        public void setSuccess(bool isSuccess)
        {
            this.isSuccess = isSuccess;
        }

        public String getFaceDB()
        {
            return faceDB;
        }
        public void setFaceDB(String faceDB)
        {
            this.faceDB = faceDB;
        }
    }
}
