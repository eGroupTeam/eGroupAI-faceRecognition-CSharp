using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.entity
{
    public class StartupInfo
    {
        private String item;
        private String status;
        private String time;

        public String getItem()
        {
            return item;
        }
        public void setItem(String item)
        {
            this.item = item;
        }
        public String getStatus()
        {
            return status;
        }
        public void setStatus(String status)
        {
            this.status = status;
        }
        public String getTime()
        {
            return time;
        }
        public void setTime(String time)
        {
            this.time = time;
        }
    }
}
