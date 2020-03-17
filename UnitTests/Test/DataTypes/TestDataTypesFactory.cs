using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Clases;
using MiniSQL.Constants;
using MiniSQL.DataTypes;

namespace UnitTests.Test.DataTypes
{
    [TestClass]
    public class TestDataTypesFactory
    {

        [TestMethod]
        public void TestGetStringTypeDataType()
        {
            DataTypesFactory dataTypesFactory = GetDataTypesFactory();
            StringType stringType = StringType.GetStringType();
            Assert.AreEqual(stringType, dataTypesFactory.GetDataType(TypesKeyConstants.StringTypeKey));
        }

        [TestMethod]
        public void TestGetIntTypeDataType()
        {
            DataTypesFactory dataTypesFactory = GetDataTypesFactory();
            IntType intType = IntType.GetIntType();
            Assert.AreEqual(intType, dataTypesFactory.GetDataType(TypesKeyConstants.IntTypeKey));
        }

        [TestMethod]
        public void TestGetDoubleTypeDataType()
        {
            DataTypesFactory dataTypesFactory = GetDataTypesFactory();
            DoubleType doubleType = DoubleType.GetDoubleType();
            Assert.AreEqual(doubleType, dataTypesFactory.GetDataType(TypesKeyConstants.DoubleTypeKey));
        }

        public static DataTypesFactory GetDataTypesFactory()
        {
            return DataTypesFactory.GetDataTypesFactory();
        }

    }
}