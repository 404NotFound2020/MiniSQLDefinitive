using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Test.TestObjectsContructor
{
    class VariousFunctions
    {
        private static int[] allowedChars = new int[] {48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122};
        private static Random random;
        public static string GenerateRandomString(int size) 
        {
            string randomString = "";
            if(random == null) random = new Random();           
            for (int i = 0; i < size; i++) {
               randomString = randomString + (char) allowedChars[random.Next(0, allowedChars.Length - 1)];            
            }

            return randomString;
        }




    }
}
