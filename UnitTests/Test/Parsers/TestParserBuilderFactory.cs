using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Test.Parsers
{
    [TestClass]
    public class TestParserBuilderFactory
    {
        [TestMethod]
        public void TestGetSaveDataFormatManager_XMLParserVersionString_ReturnXMLParserBuilder()
        {
            //For get the xml parser version variable do ParserVersions.XMLParserVersion
            //the if the class of the returned object is the XMLParserBuilder
            //use the assert.Equal with the typeOf funtion applied in the returned object and the class name (without '') (I think) (I dont empiricaly check this but 
            //the people say that typeof is the fuction for do this)
        }
    }
}
