using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.DataTypes;

namespace UnitTests.Test.DataTypes
{
    [TestClass]
    public class TestIntType
    {
        //Test names as https://docs.microsoft.com/es-es/dotnet/core/testing/unit-testing-best-practices
        [TestMethod]
        public void IsAValidDataType_StringifiedInt_ReturnTrue()
        {
            IntType intType = TestIntType.CreateIntType();
            string textNumber = "6";
            //Verify that the data for the test is well (because sometimes people could be so stupid that they do a test with fails)
            Assert.IsTrue(int.TryParse(textNumber, out int parsedNumber));
            Assert.IsTrue(intType.IsAValidDataType(textNumber));
        }

        [TestMethod]
        public void IsAValidDataType_StringifiedText_ReturnFalse() 
        {
            IntType intType = TestIntType.CreateIntType();
            string text = "iyg";
            Assert.IsFalse(int.TryParse(text, out int parsedNumber));
            Assert.IsFalse(intType.IsAValidDataType(text));
        }

        [TestMethod]
        public void IsAValidDataType_StringifiedDouble_ReturnFalse() 
        {
            IntType intType = TestIntType.CreateIntType();
            string textDouble = "6.8";
            Assert.IsFalse(int.TryParse(textDouble, out int parsedNumber));
            Assert.IsFalse(intType.IsAValidDataType(textDouble));
        }

        [TestMethod]
        public void TestGetIntType()
        { 
        
        }

        public static IntType CreateIntType() 
        {
            return IntType.GetIntType();
        }


    }
}
