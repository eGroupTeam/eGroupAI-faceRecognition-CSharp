using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.entity
{
    public class Face
    {
        public enum FACEQUALITY
        {
            C1,
            C2,
            C3,
            C4,
            PASS
        }

        private String hasFound;

        private String personId;

        private List<SimilarFace> similarFaceList;

        private FrameFace frameFace;

        private String framePath;

        private String systemTime;

        private String videoTime;

        private String videoFrameNo;

        private String imageSourcePath;

        private String faceQuality;

        private String faceQualityBlurness;

        private String faceQualityLowLuminance;
        private String faceQualityHighLuminance;

        private String faceQualityHeadpose;

        private String faceQualityName;
        private int faceSize;
        private String currentFrameID;
        private String livenessHeadposeX;
        private String livenessHeadposeY;
        private String livenessHeadposeZ;
        private String livenessHeadposeClass;

        private String result;

        private String questionID;

        private String faceLabel;
        private String depthInfo;
    }
}
