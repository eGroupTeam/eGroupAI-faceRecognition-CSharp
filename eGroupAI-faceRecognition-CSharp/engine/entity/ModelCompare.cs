using eGroupAI_faceRecognition_CSharp.library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.entity
{
    public class ModelCompare
    {
        //private static Logger LOGGER = LoggerFactory.getLogger(CmdUtil.class);
        private Double threshold;
        private String modelFaceDBPathA;
        private String modelFaceDBPathB;
        private String outputCsvPath;
        private StringBuilder cli;
        private List<String> commandList;
        private String disk;
        private String enginePath;
        // init func
        private AttributeCheck attributeCheck;

        public Double getThreshold()
        {
            return threshold;
        }

        public void setThreshold(Double threshold)
        {
            this.threshold = threshold;
        }

        public String getModelFaceDBPathA()
        {
            return modelFaceDBPathA;
        }

        public void setModelFaceDBPathA(String modelFaceDBPathA)
        {
            this.modelFaceDBPathA = modelFaceDBPathA;
        }

        public String getModelFaceDBPathB()
        {
            return modelFaceDBPathB;
        }

        public void setModelFaceDBPathB(String modelFaceDBPathB)
        {
            this.modelFaceDBPathB = modelFaceDBPathB;
        }

        public StringBuilder getCli()
        {
            return cli;
        }

        public void setCli(StringBuilder cli)
        {
            this.cli = cli;
        }

        public String getEnginePath()
        {
            return enginePath;
        }

        public void setEnginePath(String enginePath)
        {
            this.enginePath = enginePath;
        }

        public List<String> getCommandList()
        {
            if (attributeCheck == null)
            {
                attributeCheck = new AttributeCheck();
            }
            if (attributeCheck.stringsNotNull(new string[] { cli.ToString() }))
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

        public String getOutputCsvPath()
        {
            return outputCsvPath;
        }

        public void setOutputCsvPath(String outputCsvPath)
        {
            this.outputCsvPath = outputCsvPath;
        }

        public void generateCli()
        {
            if (attributeCheck == null)
            {
                attributeCheck = new AttributeCheck();
            }
            this.disk = enginePath.Substring(0, 1);
            if (attributeCheck.stringsNotNull(new string[] { enginePath, disk }))
            {
                cli = new StringBuilder("cd " + enginePath + " && " + disk + ": && ModelCompare " + threshold + " " + " \"" + modelFaceDBPathA + "\" \""
                    + modelFaceDBPathB + "\" \"" + outputCsvPath + "\"");
            }
            else
            {
                cli = null;
            }
            //LOGGER.info("RecognizeFace cli : " + cli);
            Console.WriteLine("RecognizeFace cli : " + cli);
        }
    }
}
