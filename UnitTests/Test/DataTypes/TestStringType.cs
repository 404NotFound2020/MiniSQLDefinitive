using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Clases;
using MiniSQL.Constants;
using MiniSQL.DataTypes;

namespace UnitTests.Test.DataTypes
{
    [TestClass]
    public class TestStringType
    {
        [TestMethod]
        public void TestIsAValidDataType_StringfiedDouble_ReturnTrue()
        {
            string textDouble = "33.33";
            StringType stringType = CreateStringType();
            Assert.IsTrue(double.TryParse(textDouble, out double dbl));
            Assert.IsTrue(stringType.IsAValidDataType(textDouble));
        }

        [TestMethod]
        public void TestIsAValidDataType_StringfiedInt_ReturnTrue() 
        {
            string textInt = "30";
            StringType stringType = CreateStringType();
            Assert.IsTrue(int.TryParse(textInt, out int intg));
            Assert.IsTrue(stringType.IsAValidDataType(textInt));
        }

        public void TestIsAValidDataType_StringifiedString_ReturnTrue() 
        {
            string textString = "AAAAA";
            StringType stringType = CreateStringType();
            Assert.IsTrue(stringType.IsAValidDataType(textString));
        }

        public static StringType CreateStringType()
        {
            return (StringType)DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.StringTypeKey);
        }

    }
}
