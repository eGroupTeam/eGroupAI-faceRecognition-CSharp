using eGroupAI_faceRecognition_CSharp.engine.control;
using eGroupAI_faceRecognition_CSharp.library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.entity
{
    public class ModelAppend : EngineFunc
    {
        private AttributeCheck attributeCheck;
        private String listPath;
        private String trainedBinaryPath;
        private String trainedFaceInfoPath;
        private List<String> modelBinaryList;
        private List<String> modelFaceInfoList;
        private StringBuilder cli;
        private List<String> commandList = new List<String>();
        private String disk;
        private String trainedFaceDBPath;
        private List<String> faceDBList;
        private String enginePath;
        private HashSet<String> faceDBHashset = new HashSet<String>();

        private Dictionary<String, String> modelHashmap = new Dictionary<String, String>();

        public String getListPath()
        {
            return listPath;
        }
        public void setListPath(String listPath)
        {
            this.listPath = listPath;
        }
        public String getTrainedBinaryPath()
        {
            return trainedBinaryPath;
        }
        public void setTrainedBinaryPath(String trainedBinaryPath)
        {
            this.trainedBinaryPath = trainedBinaryPath;
        }
        public String getTrainedFaceInfoPath()
        {
            return trainedFaceInfoPath;
        }
        public void setTrainedFaceInfoPath(String trainedFaceInfoPath)
        {
            this.trainedFaceInfoPath = trainedFaceInfoPath;
        }
        public StringBuilder getCli()
        {
            return cli;
        }
        public void setCli(StringBuilder cli)
        {
            this.cli = cli;
        }
        public List<String> getCommandList()
        {
            if (attributeCheck == null)
            {
                attributeCheck = new AttributeCheck();
            }
            //if (cli != null)
                if (attributeCheck.stringsNotNull(cli.ToString()))  //cd C:\QuickStart\eGroupAI_FaceEngine_CPU_Windows_V4.2.2 && C: && ModelAppend "C:\QuickStart\eGroupAI_FaceEngine_CPU_Windows_V4.2.2\eGroup\modelList.egroup.List" "C:\QuickStart\eGroupAI_FaceEngine_CPU_Windows_V4.2.2\eGroup.faceDB"
                {
                    commandList = new List<String>();
                    commandList.Add("cmd");
                    commandList.Add("/C");
                    commandList.Add(disk + ": && " + cli.ToString().Replace("/", "/"));
                }
            return commandList;
        }
        public void setCommandList(List<String> commandList)
        {
            this.commandList = commandList;
        }
        public String getDisk()
        {
            return disk;
        }
        public void setDisk(String disk)
        {
            this.disk = disk;
        }
        public void generateCli(String enginePath)
        {
            if (attributeCheck == null)
            {
                attributeCheck = new AttributeCheck();
            }
            this.disk = enginePath.Substring(0, 1);
            if (attributeCheck.stringsNotNull(new string[] { enginePath, disk, listPath, trainedFaceDBPath}))
            {
                cli = new StringBuilder("cd " + enginePath + " && " + disk + ": && ModelAppend \"" + listPath + "\" \"" + trainedFaceDBPath + "\"");
            }
            else
            {
                cli = null;
            }
            Console.WriteLine("cli=" + cli);
        }
        public List<String> getModelBinaryList()
        {
            if (attributeCheck == null)
            {
                attributeCheck = new AttributeCheck();
            }
            if (!attributeCheck.listNotNull_Zero(modelBinaryList))
            {
                modelBinaryList = new List<String>();
            }
            return modelBinaryList;
        }
        public void setModelBinaryList(List<String> modelBinaryList)
        {
            this.modelBinaryList = modelBinaryList;
        }
        public List<String> getModelFaceInfoList()
        {
            if (!attributeCheck.listNotNull_Zero(modelFaceInfoList))
            {
                modelFaceInfoList = new List<String>();
            }
            return modelFaceInfoList;
        }
        public void setModelFaceInfoList(List<String> modelFaceInfoList)
        {
            this.modelFaceInfoList = modelFaceInfoList;
        }
        public Dictionary<String, String> getModelHashmap()
        {
            return modelHashmap;
        }
        public void setModelHashmap(Dictionary<String, String> modelHashmap)
        {
            this.modelHashmap = modelHashmap;
            this.modelBinaryList = new List<String>(modelHashmap.Keys.ToList());
            this.modelFaceInfoList = new List<String>(modelHashmap.Values.ToList());
        }
        public String getEnginePath()
        {
            return enginePath;
        }
        public void setEnginePath(String enginePath)
        {
            this.enginePath = enginePath;
        }

        public String getTrainedFaceDBPath()
        {
            return trainedFaceDBPath;
        }

        public void setTrainedFaceDBPath(String trainedFaceDBPath)
        {
            this.trainedFaceDBPath = trainedFaceDBPath;
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

        public HashSet<String> getFaceDBHashset()
        {
            return faceDBHashset;
        }

        public void setFaceDBHashset(HashSet<String> faceDBHashset)
        {
            this.faceDBHashset = faceDBHashset;
            this.faceDBList = new List<String>(faceDBHashset);
        }
    }
}
