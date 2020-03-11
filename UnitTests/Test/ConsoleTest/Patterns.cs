using System;
using System.Text.RegularExpressions;
using ClientConsole.ConsoleStuff;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Test.ConsoleTest
{
    [TestClass]
    public class Patterns
    {
        [TestMethod]
        public void TestSelectPattern()
        {
            string selectPattern = (QueryVerifier.selectPattern);
            Regex regularExpression = new Regex(@selectPattern);
            //GOODS
            Assert.IsTrue(regularExpression.Matches("SELECT a FROM g;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("SELECT aaa FROM g;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("SELECT a,b FROM g;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("SELECT aa,be FROM gd;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM gs;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM ga;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM ga WHERE z=1;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM ga WHERE z>1;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM ga WHERE z<1;").Count == 1);
            //BADS
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM ga WHERE z<1").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM ga").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM ga WHEREz=1;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT* FROM ga WHERE z=1;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROMga WHERE z=1;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT *FROM ga WHERE z=1;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM gaWHERE z=1;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM ;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT *, FROM ga;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT *,as FROM ga;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT a, b FROM g;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT FROM g;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM ga WHERE <z=1;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM ga WHERE =z=1;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM ga WHERE >z=1;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM ga WHERE z=1").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM ga WHERE =1").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM ga WHERE >1").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM ga WHERE <1").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM ga WHERE a").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM ga WHERE >").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM ga WHERE <").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM ga WHERE =").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM ga(;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT a) FROM g;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT *").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT").Count == 0);
        }

        [TestMethod]
        public void TestInsertPattern() 
        {
            string insertPattern = (QueryVerifier.insertPattern);
            Regex regularExpression = new Regex(insertPattern);
            //GOODS
            Assert.IsTrue(regularExpression.Matches("INSERT INTO aaa VALUES(1);").Count == 1);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO aaa VALUES(1,2);").Count == 1);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO aaa(a) VALUES(1,2);").Count == 1); //<->
            Assert.IsTrue(regularExpression.Matches("INSERT INTO aaa(a,b) VALUES(1,2);").Count == 1);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO aaa(a,b) VALUES(1.2,2);").Count == 1);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO aaa(a,b) VALUES(aaaa,2);").Count == 1);
            //BADS
            Assert.IsTrue(regularExpression.Matches("INSERT INTO aaa").Count == 0);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO aaa() VALUES(1);").Count == 0);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO aaa VALUES(1,2)").Count == 0);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO aaa(a,) VALUES(1,2)").Count == 0);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO aaa(a,b) VALUES(1,)").Count == 0);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO aaa VALUES();").Count == 0);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO").Count == 0);
        }




    }
}
