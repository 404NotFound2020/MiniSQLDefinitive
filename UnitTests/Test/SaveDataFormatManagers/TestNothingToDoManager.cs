using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.SaveDataFormatManagers;

namespace UnitTests.Test.SaveDataFormatManagers
{
    [TestClass]
    public class TestNothingToDoManager
    {
        [TestMethod]
        public void VerifyTheFunctionality()
        {
            NothingToDoManager nothingToDoManager = GetManager();
            string originalString = "aaaa";
            string stringToSave = nothingToDoManager.ParseToSave(originalString);
            Assert.AreEqual(originalString, nothingToDoManager.ParseFromLoad(stringToSave));
        }

        public static NothingToDoManager GetManager()
        {
            return NothingToDoManager.GetNothingToDoManager();
        }
    }
}