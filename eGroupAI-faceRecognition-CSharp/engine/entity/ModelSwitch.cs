using eGroupAI_faceRecognition_CSharp.engine.control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.entity
{
    public class ModelSwitch : EngineFunc
    {
        private String newModelPath;
        private String nowModelPath;
        private String switchFilePath;
        private String modelSwitchLogPath;
        private String enginePath;

        public String getNewModelPath()
        {
            return newModelPath;
        }
        public void setNewModelPath(String newModelPath)
        {
            this.newModelPath = newModelPath;
        }
        public String getNowModelPath()
        {
            return nowModelPath;
        }
        public void setNowModelPath(String nowModelPath)
        {
            this.nowModelPath = nowModelPath;
        }
        public String getSwitchFilePath()
        {
            return switchFilePath;
        }
        public void setSwitchFilePath(String switchFilePath)
        {
            this.switchFilePath = switchFilePath;
        }
        public String getModelSwitchLogPath()
        {
            return modelSwitchLogPath;
        }
        public void setModelSwitchLogPath(String modelSwitchLogPath)
        {
            this.modelSwitchLogPath = modelSwitchLogPath;
        }
        public String getEnginePath()
        {
            return enginePath;
        }
        public void setEnginePath(String enginePath)
        {
            this.enginePath = enginePath;
        }

    }
}