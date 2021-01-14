using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.entity
{
    public class ModelAppendInfo
    {
        private String time;
        private String workingFolderCheckStatus;
        private String workingFolderStatus;
        private Double workingFolderSize;
        private String outputBinaryCheckStatus;
        private String outputBinaryStatus;
        private Double outputBinarySize;
        private String outputFaceInfoCheckStatus;
        private String outputFaceInfoStatus;
        private Double outputFaceInfoSize;
        private String DBSizeCheckStatus;
        private int DBSize;
        private String DBBinaryPath;
        private String DBFaceInfoPath;
        private Double outputFaceDBSize;
        private String outputFaceDBStatus;
        private String DBFaceDBPath;
        private Boolean isSucess;
        private String errorMessage;
        private String outputFaceDBCheckStatus;

        public String getTime()
        {
            return time;
        }
        public void setTime(String time)
        {
            this.time = time;
        }
        public String getWorkingFolderCheckStatus()
        {
            return workingFolderCheckStatus;
        }
        public void setWorkingFolderCheckStatus(String workingFolderCheckStatus)
        {
            this.workingFolderCheckStatus = workingFolderCheckStatus;
        }
        public String getWorkingFolderStatus()
        {
            return workingFolderStatus;
        }
        public void setWorkingFolderStatus(String workingFolderStatus)
        {
            this.workingFolderStatus = workingFolderStatus;
        }
        public Double getWorkingFolderSize()
        {
            return workingFolderSize;
        }
        public void setWorkingFolderSize(Double workingFolderSize)
        {
            this.workingFolderSize = workingFolderSize;
        }
        public String getOutputBinaryCheckStatus()
        {
            return outputBinaryCheckStatus;
        }
        public void setOutputBinaryCheckStatus(String outputBinaryCheckStatus)
        {
            this.outputBinaryCheckStatus = outputBinaryCheckStatus;
        }
        public String getOutputBinaryStatus()
        {
            return outputBinaryStatus;
        }
        public void setOutputBinaryStatus(String outputBinaryStatus)
        {
            this.outputBinaryStatus = outputBinaryStatus;
        }
        public Double getOutputBinarySize()
        {
            return outputBinarySize;
        }
        public void setOutputBinarySize(Double outputBinarySize)
        {
            this.outputBinarySize = outputBinarySize;
        }
        public String getOutputFaceInfoCheckStatus()
        {
            return outputFaceInfoCheckStatus;
        }
        public void setOutputFaceInfoCheckStatus(String outputFaceInfoCheckStatus)
        {
            this.outputFaceInfoCheckStatus = outputFaceInfoCheckStatus;
        }
        public String getOutputFaceInfoStatus()
        {
            return outputFaceInfoStatus;
        }
        public void setOutputFaceInfoStatus(String outputFaceInfoStatus)
        {
            this.outputFaceInfoStatus = outputFaceInfoStatus;
        }
        public Double getOutputFaceInfoSize()
        {
            return outputFaceInfoSize;
        }
        public void setOutputFaceInfoSize(Double outputFaceInfoSize)
        {
            this.outputFaceInfoSize = outputFaceInfoSize;
        }
        public String getDBSizeCheckStatus()
        {
            return DBSizeCheckStatus;
        }
        public void setDBSizeCheckStatus(String dBSizeCheckStatus)
        {
            DBSizeCheckStatus = dBSizeCheckStatus;
        }
        public int getDBSize()
        {
            return DBSize;
        }
        public void setDBSize(int dBSize)
        {
            DBSize = dBSize;
        }
        public String getDBBinaryPath()
        {
            return DBBinaryPath;
        }
        public void setDBBinaryPath(String dBBinaryPath)
        {
            DBBinaryPath = dBBinaryPath;
        }
        public String getDBFaceInfoPath()
        {
            return DBFaceInfoPath;
        }
        public void setDBFaceInfoPath(String dBFaceInfoPath)
        {
            DBFaceInfoPath = dBFaceInfoPath;
        }
        public Double getOutputFaceDBSize()
        {
            return outputFaceDBSize;
        }
        public void setOutputFaceDBSize(Double outputFaceDBSize)
        {
            this.outputFaceDBSize = outputFaceDBSize;
        }
        public String getDBFaceDBPath()
        {
            return DBFaceDBPath;
        }
        public void setDBFaceDBPath(String dBFaceDBPath)
        {
            DBFaceDBPath = dBFaceDBPath;
        }

        public Boolean getIsSucess()
        {
            return isSucess;
        }

        public void setSucess(Boolean isSucess)
        {
            this.isSucess = isSucess;
        }

        public String getErrorMessage()
        {
            return errorMessage;
        }
        public void setErrorMessage(String errorMessage)
        {
            this.errorMessage = errorMessage;
        }
        
        public String getOutputFaceDBCheckStatus()
        {
            return outputFaceDBCheckStatus;
        }
        public void setOutputFaceDBCheckStatus(String outputFaceDBCheckStatus)
        {
            this.outputFaceDBCheckStatus = outputFaceDBCheckStatus;
        }
        public String getOutputFaceDBStatus()
        {
            return outputFaceDBStatus;
        }
        public void setOutputFaceDBStatus(String outputFaceDBStatus)
        {
            this.outputFaceDBStatus = outputFaceDBStatus;
        }
    }
}
