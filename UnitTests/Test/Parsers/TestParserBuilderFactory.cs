using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Constants;
using MiniSQL.Parsers;

namespace UnitTests.Test.Parsers
{
    [TestClass]
    public class TestParserBuilderFactory
    {
        [TestMethod]
        public void TestGetSaveDataFormatManager_XMLParserVersionString_ReturnXMLParserBuilder()
        {

            ParserBuilderFactory.GetParserBuilderFactory().GetParserBuilder(ParserVersions.XMLParserVersion);
            Assert.AreEqual(ParserBuilderFactory.GetParserBuilderFactory().GetParserBuilder(ParserVersions.XMLParserVersion).GetType(), typeof(XMLParserBuilder));

        }
    }
}
