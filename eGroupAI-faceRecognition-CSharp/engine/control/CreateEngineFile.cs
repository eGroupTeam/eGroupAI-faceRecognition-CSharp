using eGroupAI_faceRecognition_CSharp.engine.entity;
using eGroupAI_faceRecognition_CSharp.engine.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static eGroupAI_faceRecognition_CSharp.engine.util.TxtUtil;

namespace eGroupAI_faceRecognition_CSharp.engine.control
{
    public class CreateEngineFile
    {
        public bool createTrainFaceTxt(String trainListPath, List<TrainFace> trainFaceList)
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
            if (new FileInfo(trainListPath).Exists)
            {
                flag = true;
            }
            return flag;
        }

    }
}
