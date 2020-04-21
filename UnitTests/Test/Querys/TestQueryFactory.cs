using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Querys;
using UnitTests.Test.TestObjectsContructor;

namespace UnitTests.Test.Querys
{
    [TestClass]
    public class TestQueryFactory
    {
        [TestMethod]
        public void RequestSelectQuery() 
        {
            Assert.AreEqual(GetQueryFactory().GetQuery(ObjectConstructor.GetSelectRequest(), ObjectConstructor.GetFakeUserThread()).GetType(), typeof(Select));
        }

        [TestMethod]
        public void RequestInsertQuery()
        {
            Assert.AreEqual(GetQueryFactory().GetQuery(ObjectConstructor.GetInsertRequest(), ObjectConstructor.GetFakeUserThread()).GetType(), typeof(Insert));
        }

        [TestMethod]
        public void RequestDropTableQuery()
        {
            Assert.AreEqual(GetQueryFactory().GetQuery(ObjectConstructor.GetDropTableRequest(), ObjectConstructor.GetFakeUserThread()).GetType(), typeof(Drop));
        }

        [TestMethod]
        public void RequestCreateTableQuery()
        {
            Assert.AreEqual(GetQueryFactory().GetQuery(ObjectConstructor.GetCreateTableRequest(), ObjectConstructor.GetFakeUserThread()).GetType(), typeof(Create));
        }

        [TestMethod]
        public void RequestDeleteTableQuery()
        {
            Assert.AreEqual(GetQueryFactory().GetQuery(ObjectConstructor.GetDeleteRequest(), ObjectConstructor.GetFakeUserThread()).GetType(), typeof(Delete));
        }

        [TestMethod]
        public void RequestUpdateTableQuery()
        {
            Assert.AreEqual(GetQueryFactory().GetQuery(ObjectConstructor.GetUpdateRequest(), ObjectConstructor.GetFakeUserThread()).GetType(), typeof(Update));
        }

        public static QueryFactory GetQueryFactory() 
        {
            QueryFactory queryFactory = QueryFactory.GetQueryFactory();
            queryFactory.SetSysteme(ObjectConstructor.CreateDummySysteme());
            return queryFactory;
        }

    }
}
