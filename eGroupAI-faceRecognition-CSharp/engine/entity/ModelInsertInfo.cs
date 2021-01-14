using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.entity
{
    public class ModelInsertInfo
    {
        private String datetimeString;
        private String insertModelStatus;
        private int insertFacesCount;
        private int insertPeopleCount;
        private int CurrDBFaceCout;
        private int CurrDBPeopleCount;
        private String insertProcessTime;

        public String getDatetimeString()
        {
            return datetimeString;
        }
        public void setDatetimeString(String datetimeString)
        {
            this.datetimeString = datetimeString;
        }
        public String getInsertModelStatus()
        {
            return insertModelStatus;
        }
        public void setInsertModelStatus(String insertModelStatus)
        {
            this.insertModelStatus = insertModelStatus;
        }
        public int getInsertFacesCount()
        {
            return insertFacesCount;
        }
        public void setInsertFacesCount(int insertFacesCount)
        {
            this.insertFacesCount = insertFacesCount;
        }
        public int getInsertPeopleCount()
        {
            return insertPeopleCount;
        }
        public void setInsertPeopleCount(int insertPeopleCount)
        {
            this.insertPeopleCount = insertPeopleCount;
        }
        public int getCurrDBFaceCout()
        {
            return CurrDBFaceCout;
        }
        public void setCurrDBFaceCout(int currDBFaceCout)
        {
            CurrDBFaceCout = currDBFaceCout;
        }
        public int getCurrDBPeopleCount()
        {
            return CurrDBPeopleCount;
        }
        public void setCurrDBPeopleCount(int currDBPeopleCount)
        {
            CurrDBPeopleCount = currDBPeopleCount;
        }
        public String getInsertProcessTime()
        {
            return insertProcessTime;
        }
        public void setInsertProcessTime(String insertProcessTime)
        {
            this.insertProcessTime = insertProcessTime;
        }

    }
}
