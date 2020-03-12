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
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM ga aaa WHERE z<1").Count == 0);
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
            Regex regularExpression = new Regex(@insertPattern);
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
            Assert.IsTrue(regularExpression.Matches("INSERT INTO a<aa(a,b) VALUES(1,2);").Count == 0);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO aaa(a>,b) VALUES(1,2);").Count == 0);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO aaa(a,b) a VALUES(1,2);").Count == 0);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO aaa(a,b) a VALUES(1=,2);").Count == 0);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO aa=a(a,b) a VALUES(1,2);").Count == 0);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO aaa(a,b) VALUES(aaaa,2) ;").Count == 0);
        }

        [TestMethod]
        public void TestDropPattern() 
        {
            string dropPattern = (QueryVerifier.dropPattern);
            Regex regularExpression = new Regex(@dropPattern);
            //GOODS
            Assert.IsTrue(regularExpression.Matches("DROP TABLE aaa;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("DROP TABLE aaaZA3;").Count == 1);
            //BADS
            Assert.IsTrue(regularExpression.Matches("DROP TABLE aaa").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DROP TABLE aaa=").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DROP TABLE aaa>").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DROP TABLE aaa<").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DROP TABLE aaa(").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DROP TABLE aaa)").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DROP TABLE aaa ;").Count == 0);
        }

        [TestMethod]
        public void TestDeletePattern() 
        {
            string deletePattern = (QueryVerifier.deletePattern);
            Regex regularExpression = new Regex(@deletePattern);
            //GOODS
            Assert.IsTrue(regularExpression.Matches("DELETE FROM A WHERE a=1;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM A;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a1;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a1 WHERE ab=1;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a1 WHERE ab>1;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a1 WHERE ab<1;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a1 WHERE ab=a;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a1 WHERE ab<a;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a1 WHERE ab>a;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a1 WHERE ab=1.2;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a1 WHERE ab<1.2;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a1 WHERE ab>1.2;").Count == 1);
            //BADS
            Assert.IsTrue(regularExpression.Matches("DELETE FROM A WHERE a=1").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM A WHER a=1;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM ;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM A WHERE a=;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM A WHERE a>;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM A WHERE a<;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM A WHERE <a;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM A WHERE >a;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM A WHERE =a;").Count == 0);
        }

        [TestMethod]
        public void TestUpdate()
        {
            string updatePattern = (QueryVerifier.updatePattern);
            Regex regularExpression = new Regex(@updatePattern);
            //GOODS
            Assert.IsTrue(regularExpression.Matches("UPDATE a SET b=1 WHERE b=2;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("UPDATE a SET b=1, c=1 WHERE b=2;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("UPDATE a SET b=1, c=1 WHERE b>2;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("UPDATE a SET b=1, c=1 WHERE b<2;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("UPDATE a SET b=1, c=1, h=a WHERE b<2;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("UPDATE a SET b=1.2 WHERE b<2;").Count == 1);
            //BADS
            Assert.IsTrue(regularExpression.Matches("UPDATE a SET b=1 WHERE b=2").Count == 0);
            Assert.IsTrue(regularExpression.Matches("UPDATE a SET b=1;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("UPDATE a SET b=1 WHERE").Count == 0);
            Assert.IsTrue(regularExpression.Matches("UPDATE a SET b=1, SET c=1 WHERE b=2;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("UPDATE a SET b=1, SET c=1 WHERE b;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("UPDATE a SET b WHERE b=2;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("UPDATE;").Count == 0);
        }

    }
}
