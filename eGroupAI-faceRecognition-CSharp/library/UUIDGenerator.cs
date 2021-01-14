using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGroupAI_faceRecognition_CSharp.library
{
    public class UUIDGenerator
    {
        Guid uuid = Guid.NewGuid();

        public String getBase64UUID()
        {
            //Int64 base64 = new Int64();
            List<byte> bb = new List<byte>();
            bb.Add(uuid.ToByteArray().First());
            bb.Add(uuid.ToByteArray().Last());
            return bb.ToString().Replace("_", "").Replace("-", "");//base64.ToString().encodeBase64URLSafeString(bb.array()).replaceAll("_", "").replaceAll("-", "");
        }

        public String getUUID()
        {
            return uuid.ToString().Replace("-", "");
        }

        /**
         * 12位數衝突機率:1-e^(-(10^9)^2/62^12)=0.00030990773 16位數衝突機率: 1-e^(-(10^10)^2/62^16)=2.09764972e-9 *
         * 
         * @author daniel
         *
         * @param size
         * @return
         */
        public String getRadomUUID(int size)
        {
            Random random = new Random();
            char[] digits = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
        'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
        'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};
            char[] cs = new char[size];

            for (int i = 0; i < cs.Length; i++)
            {
                cs[i] = digits[random.Next(digits.Length)];
            }
            return new String(cs);
        }

        // public static void main(String args[]) {
        // Set<String> uuidSet = new HashSet<String>();
        // String uuid;
        // int conflictCount = 0;
        // for (int i = 0; i < 20000000; i++) {
        // uuid = getRadomID(12);
        // if (uuidSet.contains(uuid)) {
        // conflictCount++;
        // }
        // uuidSet.add(uuid);
        // System.out.println("i=" + i + ",uuid=" + uuid + ",conflictCount=" + conflictCount);
        // }
        // }

    }
}
