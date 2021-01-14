using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.engine.entity
{
    public class SimilarFace
    {
        private String personFaceId;
        private String personFacePath;
        private String similarity;

        public String getPersonFaceId()
        {
            return personFaceId;
        }
        public void setPersonFaceId(String personFaceId)
        {
            this.personFaceId = personFaceId;
        }
        public String getPersonFacePath()
        {
            return personFacePath;
        }
        public void setPersonFacePath(String personFacePath)
        {
            this.personFacePath = personFacePath;
        }
        public String getSimilarity()
        {
            return similarity;
        }
        public void setSimilarity(String similarity)
        {
            this.similarity = similarity;
        }

    }
}