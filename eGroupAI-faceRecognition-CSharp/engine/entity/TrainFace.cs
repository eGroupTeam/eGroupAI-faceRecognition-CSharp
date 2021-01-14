using eGroupAI_faceRecognition_CSharp.library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.entity
{
    public class TrainFace
    {
        //private static Logger LOGGER = LoggerFactory.getLogger(TrainFace.class);

        private bool isModelExist;
        private String trainListPath;
        private String modelPath;
        private StringBuilder cli;
        private List<String> commandList;
        private String disk;
        private TrainResult trainResult;

        // programe control
        private List<String> imagePathList;
        private String personId;
        private String imagePathJson;
        private bool uploadFace = false;
        private bool isGGPass = false;
        // blackwhite variable
        private int hasTrainFaceCount;
        private int train_successCount;
        private int train_failCount;
        private String enginePath;
        private List<String> trainResultList;
        private AttributeCheck attributeCheck;

        public AttributeCheck getAttributeCheck()
        {
            if (attributeCheck == null)
            {
                attributeCheck = new AttributeCheck();
            }
            return attributeCheck;
        }

        public void setAttributeCheck(AttributeCheck attributeCheck)
        {
            this.attributeCheck = attributeCheck;
        }

        public bool isModelExists()
        {
            return isModelExist;
        }

        public void setModelExist(bool isModelExist)
        {
            this.isModelExist = isModelExist;
        }

        public String getTrainListPath()
        {
            return trainListPath;
        }

        public void setTrainListPath(String trainListPath)
        {
            this.trainListPath = trainListPath;
        }

        public String getModelPath()
        {
            return modelPath;
        }

        public void setModelPath(String modelPath)
        {
            this.modelPath = modelPath;
        }

        public StringBuilder getCli()
        {
            return cli;
        }

        public void setCli(StringBuilder cli)
        {
            this.cli = cli;
        }

        public void generateCli()
        {
            if (attributeCheck == null)
            {
                attributeCheck = new AttributeCheck();
            }
            this.disk = this.enginePath.Substring(0, 1);
            if (attributeCheck.stringsNotNull(new String[] { this.enginePath, disk, trainListPath, modelPath }))
            {
                if (this.isModelExist)
                {
                    cli = new StringBuilder("cd " + this.enginePath + " && " + disk + ": && TrainFace " + (isGGPass == true ? " --eGroupGGPass " : "")
                        + " --append \"" + trainListPath + "\" \"" + modelPath + "\"");
                }
                else
                {
                    cli = new StringBuilder("cd " + this.enginePath + " && " + disk + ": && TrainFace " + (isGGPass == true ? " --eGroupGGPass " : "") + " \""
                        + trainListPath + "\" \"" + modelPath + "\"");
                }
            }
            else
            {
                cli = null;
            }
            //LOGGER.info("cli=" + cli);
        }

        public List<String> getCommandList()
        {
            if (attributeCheck == null)
            {
                attributeCheck = new AttributeCheck();
            }
            if (attributeCheck.stringsNotNull(new String[] { cli.ToString() }))
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

        public List<String> getImagePathList()
        {
            if (attributeCheck == null)
            {
                attributeCheck = new AttributeCheck();
            }
            if (!attributeCheck.listNotEmpty(imagePathList))
            {
                imagePathList = new List<String>();
            }
            return imagePathList;
        }

        public void setImagePathList(List<String> imagePathList)
        {
            this.imagePathList = imagePathList;
        }

        public String getImagePathJson()
        {
            return imagePathJson;
        }

        public void setImagePathJson(String imagePathJson)
        {
            this.imagePathJson = imagePathJson;
        }

        public String getPersonId()
        {
            return personId;
        }

        public void setPersonId(String personId)
        {
            this.personId = personId;
        }

        public bool isUploadFace()
        {
            return uploadFace;
        }

        public void setUploadFace(bool uploadFace)
        {
            this.uploadFace = uploadFace;
        }

        public int getHasTrainFaceCount()
        {
            return hasTrainFaceCount;
        }

        public void setHasTrainFaceCount(int hasTrainFaceCount)
        {
            this.hasTrainFaceCount = hasTrainFaceCount;
        }

        public String getEnginePath()
        {
            return enginePath;
        }

        public void setEnginePath(String enginePath)
        {
            this.enginePath = enginePath;
        }

        public int getTrain_successCount()
        {
            return train_successCount;
        }

        public void setTrain_successCount(int train_successCount)
        {
            this.train_successCount = train_successCount;
        }

        public int getTrain_failCount()
        {
            return train_failCount;
        }

        public void setTrain_failCount(int train_failCount)
        {
            this.train_failCount = train_failCount;
        }

        public List<String> getTrainResultList()
        {
            return trainResultList;
        }

        public void setTrainResultList(List<String> trainResultList)
        {
            this.trainResultList = trainResultList;
        }

        public TrainResult getTrainResult()
        {
            if (trainResult == null)
            {
                trainResult = new TrainResult();
            }
            return trainResult;
        }

        public void setTrainResult(TrainResult trainResult)
        {
            this.trainResult = trainResult;
        }

        public bool isGG_Pass()
        {
            return isGGPass;
        }

        public void setGGPass(bool isGGPass)
        {
            this.isGGPass = isGGPass;
        }
    }
}
