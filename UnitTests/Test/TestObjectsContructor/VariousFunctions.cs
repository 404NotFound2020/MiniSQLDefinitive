using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Test.TestObjectsContructor
{
    class VariousFunctions
    {

        public static string GenerateRandomString(int size) 
        {
            string randomString = "";
            Random random = new Random();
            for (int i = 0; i < size; i++) {
               randomString = randomString + (char) random.Next(32, 126);            
            }

            return randomString;
        }




    }
}
