using System;
using System.Text.RegularExpressions;
using MiniSQL.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Test.ConsoleTest
{
    [TestClass]
    public class Patterns
    {
        [TestMethod]
        public void TestSelectPattern()
        {
            string selectPattern = (RequestAndRegexConstants.selectPattern);
            Regex regularExpression = new Regex(@selectPattern);
            //GOODS
            Assert.IsTrue(regularExpression.Matches("SELECT a FROM a.g;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("SELECT aaa FROM a.g;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("SELECT a,b FROM a.g;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("SELECT aa,be FROM a.gd;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM a.gs;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM a.ga;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM a.ga WHERE z=1;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM a.ga WHERE z>1;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM a.ga WHERE z<1;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("SELECT a FROM g;").Count == 1);
            //BADS
            // Assert.IsTrue(regularExpression.Matches("SELECT a FROM g;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM a.ga WHERE z<1").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM a.ga aaa WHERE z<1").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM a.ga").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM a.ga WHEREz=1;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT* FROM a.ga WHERE z=1;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROMga WHERE z=1;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT *FROM a.ga WHERE z=1;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM a.gaWHERE z=1;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM ;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT *, FROM a.ga;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT *,as FROM a.ga;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT a, b FROM g;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT FROM g;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM a.ga WHERE <z=1;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM a.ga WHERE =z=1;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM a.ga WHERE >z=1;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM a.ga WHERE z=1").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM a.ga WHERE =1").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM a.ga WHERE >1").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM a.ga WHERE <1").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM a.ga WHERE a").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM a.ga WHERE >").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM a.ga WHERE <").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM a.ga WHERE =").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT * FROM a.ga(;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT a) FROM g;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT *").Count == 0);
            Assert.IsTrue(regularExpression.Matches("SELECT").Count == 0);
        }

        [TestMethod]
        public void TestInsertPattern() 
        {
            string insertPattern = (RequestAndRegexConstants.insertPattern);
            Regex regularExpression = new Regex(@insertPattern);
            //GOODS
            Assert.IsTrue(regularExpression.Matches("INSERT INTO a.aaa VALUES(1);").Count == 1);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO a.aaa VALUES(1,2);").Count == 1);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO aaa VALUES(1);").Count == 1);
            //BADS
            Assert.IsTrue(regularExpression.Matches("INSERT INTO a.aaa(a) VALUES(1,2);").Count == 0); //<->
            Assert.IsTrue(regularExpression.Matches("INSERT INTO a.aaa(a,b) VALUES(1,2);").Count == 0);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO a.aaa(a,b) VALUES(1.2,2);").Count == 0);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO a.aaa(a,b) VALUES(aaaa,2);").Count == 0);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO a.aaa").Count == 0);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO a.aaa() VALUES(1);").Count == 0);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO a.aaa VALUES(1,2)").Count == 0);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO a.aaa(a,) VALUES(1,2)").Count == 0);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO a.aaa(a,b) VALUES(1,)").Count == 0);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO a.aaa VALUES();").Count == 0);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO").Count == 0);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO a<aa(a,b) VALUES(1,2);").Count == 0);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO a.aaa(a>,b) VALUES(1,2);").Count == 0);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO a.aaa(a,b) a VALUES(1,2);").Count == 0);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO a.aaa(a,b) a VALUES(1=,2);").Count == 0);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO a.aa=a(a,b) a VALUES(1,2);").Count == 0);
            Assert.IsTrue(regularExpression.Matches("INSERT INTO a.aaa(a,b) VALUES(aaaa,2) ;").Count == 0);
           // Assert.IsTrue(regularExpression.Matches("INSERT INTO aaa VALUES(1);").Count == 0);
        }

        [TestMethod]
        public void TestDropPattern() 
        {
            string dropPattern = (RequestAndRegexConstants.dropPattern);
            Regex regularExpression = new Regex(@dropPattern);
            //GOODS
            Assert.IsTrue(regularExpression.Matches("DROP TABLE a.aaa;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("DROP TABLE a.aaaZA3;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("DROP TABLE aaaZA3;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("DROP TABLE aaaZA3;").Count == 1);
            //BADS
            Assert.IsTrue(regularExpression.Matches("DROP TABLE a.aaa").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DROP TABLE a.aaa=").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DROP TABLE a.aaa>").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DROP TABLE a.aaa<").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DROP TABLE a.aaa(").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DROP TABLE a.aaa)").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DROP TABLE a.aaa ;").Count == 0);
            //Assert.IsTrue(regularExpression.Matches("DROP TABLE aaaZA3;").Count == 0);
        }

        [TestMethod]
        public void TestDeletePattern() 
        {
            string deletePattern = (RequestAndRegexConstants.deletePattern);
            Regex regularExpression = new Regex(@deletePattern);
            //GOODS
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a.A WHERE a=1;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a.A;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a.a1;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a.a1 WHERE ab=1;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a.a1 WHERE ab>1;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a.a1 WHERE ab<1;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a.a1 WHERE ab=a;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a.a1 WHERE ab<a;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a.a1 WHERE ab>a;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a.a1 WHERE ab=1.2;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a.a1 WHERE ab<1.2;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a.a1 WHERE ab>1.2;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM A WHERE a=1;").Count == 1);
            //BADS
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a.A WHERE a=1").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a.A WHER a=1;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM ;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a.A WHERE a=;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a.A WHERE a>;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a.A WHERE a<;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a.A WHERE <a;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a.A WHERE >a;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("DELETE FROM a.A WHERE =a;").Count == 0);
        }

        [TestMethod]
        public void TestUpdate()
        {
            string updatePattern = (RequestAndRegexConstants.updatePattern);
            Regex regularExpression = new Regex(@updatePattern);
            //GOODS
            Assert.IsTrue(regularExpression.Matches("UPDATE a.a SET b=1 WHERE b=2;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("UPDATE a.a SET b=1, c=1 WHERE b=2;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("UPDATE a.a SET b=1, c=1 WHERE b>2;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("UPDATE a.a SET b=1, c=1 WHERE b<2;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("UPDATE a.a SET b=1, c=1, h=a WHERE b<2;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("UPDATE a.a SET b=1.2 WHERE b<2;").Count == 1);
            Assert.IsTrue(regularExpression.Matches("UPDATE a SET b=1 WHERE b=2;").Count == 1);
            //BADS
            Assert.IsTrue(regularExpression.Matches("UPDATE a.a SET b=1 WHERE b=2").Count == 0);
            Assert.IsTrue(regularExpression.Matches("UPDATE a.a SET b=1;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("UPDATE a.a SET b=1 WHERE").Count == 0);
            Assert.IsTrue(regularExpression.Matches("UPDATE a.a SET b=1, SET c=1 WHERE b=2;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("UPDATE a.a SET b=1, SET c=1 WHERE b;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("UPDATE a.a SET b WHERE b=2;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("UPDATE;").Count == 0);
        }

        [TestMethod]
        public void TestCreate() {
            string createPattern = (RequestAndRegexConstants.createPattern);
            Regex regularExpression = new Regex(@createPattern);
            //GOODS
            Assert.IsTrue(regularExpression.Matches("CREATE TABLE a.aaa (acaa TEXT);").Count == 1);
            Assert.IsTrue(regularExpression.Matches("CREATE TABLE a.aaa (acaa DOUBLE);").Count == 1);
            Assert.IsTrue(regularExpression.Matches("CREATE TABLE a.aaa (acaa INT);").Count == 1);
            Assert.IsTrue(regularExpression.Matches("CREATE TABLE a.aaa (acaa INT, b TEXT);").Count == 1);
            Assert.IsTrue(regularExpression.Matches("CREATE TABLE a.aaa (acaa INT, b DOUBLE);").Count == 1);
            Assert.IsTrue(regularExpression.Matches("CREATE TABLE a.aaa (acaa INT, b INT);").Count == 1);
            Assert.IsTrue(regularExpression.Matches("CREATE TABLE a.aaa (acaa INT, b INT, c DOUBLE);").Count == 1);
            Assert.IsTrue(regularExpression.Matches("CREATE TABLE aaa (acaa TEXT);").Count == 1);
            //BADS
            Assert.IsTrue(regularExpression.Matches("CREATE TABLE a.aaa (acaa TEXT)").Count == 0);
            Assert.IsTrue(regularExpression.Matches("CREATE TABLE a.aaa (acaa YF);").Count == 0);
            Assert.IsTrue(regularExpression.Matches("CREATE TABLE a.aaa (acaa);").Count == 0);
            Assert.IsTrue(regularExpression.Matches("CREATE TABLE (acaa TEXT);").Count == 0);
            Assert.IsTrue(regularExpression.Matches("CREATE TABLE a.aaa (acaa TEXT, sss f);").Count == 0);
            Assert.IsTrue(regularExpression.Matches("CREATE TABLE a.aaa (acaa TEXT, sss);").Count == 0);
            Assert.IsTrue(regularExpression.Matches("CREATE TABLE a.aaa (acaa TEXT,);").Count == 0);
            Assert.IsTrue(regularExpression.Matches("CREATE TABLE a.aaa (acaa TEXT, );").Count == 0);
            Assert.IsTrue(regularExpression.Matches("CREATE TABLE a.aaa acaa TEXT;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("CREATE TABLE a.aaa;").Count == 0);
            Assert.IsTrue(regularExpression.Matches("CREATE TABLE;").Count == 0);
            //Assert.IsTrue(regularExpression.Matches("CREATE TABLE aaa (acaa TEXT);").Count == 0);
        }
    }
}
