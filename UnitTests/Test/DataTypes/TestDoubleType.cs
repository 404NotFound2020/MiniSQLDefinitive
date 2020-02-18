using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Constants;
using MiniSQL.DataTypes;
using MiniSQL.Interfaces;

namespace UnitTests.Test.DataTypes
{
    [TestClass]
    public class TestDoubleType
    {

        [TestMethod]
        public void IsValidDataType_StringifiedDouble_ReturnTrue()
        {
            DoubleType doubleType = CreateDoubleType();
            string textDouble = "6.8";
            Assert.IsTrue(double.TryParse(textDouble, out double parsedNumber));
            Assert.IsTrue(doubleType.IsAValidDataType(textDouble));
        }

        [TestMethod]
        public void IsValidDataType_StringifiedText_ReturnFalse()
        {
            DoubleType doubleType = CreateDoubleType();
            string textNoDouble = "ewe";
            Assert.IsFalse(double.TryParse(textNoDouble, out double parsedNumber));
            Assert.IsFalse(doubleType.IsAValidDataType(textNoDouble));
        }

        [TestMethod]
        public void IsValidDataType_StringifiedInt_ReturnTrue()
        {
            DoubleType doubleType = CreateDoubleType();
            string textNoDouble = "1";
            Assert.IsTrue(double.TryParse(textNoDouble, out double parsedNumber));
            Assert.IsTrue(doubleType.IsAValidDataType(textNoDouble));
        }

        public static DoubleType CreateDoubleType() 
        {
            return (DoubleType)DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.DoubleTypeKey);
        }

    }
}
