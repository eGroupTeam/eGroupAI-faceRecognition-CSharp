using eGroupAI_faceRecognition_CSharp.engine.entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.util
{
    public class CreateEngineFileUtil
    {
        public Boolean createTrainFaceTxt(String trainListPath, List<TrainFace> trainFaceList)
        {
            // init func
            Guid uuidGenerator = new Guid();
            TxtUtil txtUtil = new TxtUtil();
            Boolean flag = false;
            // Create training
            List<String> dataList = new List<String>();
            foreach (TrainFace getTrainFace in trainFaceList)
            {
                foreach (String imagePath in getTrainFace.getImagePathList())
                {
                    dataList.Add(imagePath + "\t" + getTrainFace.getPersonId() + "[No]" + uuidGenerator.ToString());
                }
            }
            txtUtil.create(trainListPath, dataList);
            if (File.Exists(trainListPath))
            {
                flag = true;
            }
            return flag;
        }

    }
}
