using Newtonsoft.Json;
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

        [JsonProperty]
        private String hasFound;

        [JsonProperty]
        private String personId;

        [JsonProperty]
        private List<SimilarFace> similarFaceList;

        [JsonProperty]
        private FrameFace frameFace;

        [JsonProperty]
        private String framePath;

        [JsonProperty]
        private String systemTime;

        [JsonProperty]
        private String videoTime;

        [JsonProperty]
        private String videoFrameNo;

        [JsonProperty]
        private String imageSourcePath;

        [JsonProperty]
        private String faceQuality;

        [JsonProperty]
        private String faceQualityBlurness;

        [JsonProperty]
        private String faceQualityLowLuminance;
        [JsonProperty]
        private String faceQualityHighLuminance;

        [JsonProperty]
        private String faceQualityHeadpose;

        [JsonProperty]
        private String faceQualityName;
        [JsonProperty]
        private int faceSize;
        [JsonProperty]
        private String currentFrameID;
        [JsonProperty]
        private String livenessHeadposeX;
        [JsonProperty]
        private String livenessHeadposeY;
        [JsonProperty]
        private String livenessHeadposeZ;
        [JsonProperty]
        private String livenessHeadposeClass;

        [JsonProperty]
        private String result;

        [JsonProperty]
        private String questionID;

        [JsonProperty]
        private String faceLabel;
        [JsonProperty]
        private String depthInfo;
    }
}
