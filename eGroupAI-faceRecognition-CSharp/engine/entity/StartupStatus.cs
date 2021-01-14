using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.entity
{
    public class StartupStatus
    {
        private int checkId;
        private String checkName;
        private bool checkFlag;

        public int getCheckId()
        {
            return checkId;
        }
        public void setCheckId(int checkId)
        {
            this.checkId = checkId;
        }
        public String getCheckName()
        {
            return checkName;
        }
        public void setCheckName(String checkName)
        {
            this.checkName = checkName;
        }
        public bool isCheckFlag()
        {
            return checkFlag;
        }
        public void setCheckFlag(bool checkFlag)
        {
            this.checkFlag = checkFlag;
        }

    }
}
