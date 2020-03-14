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
            Assert.AreEqual(GetQueryFactory().GetQuery(ObjectConstructor.GetSelectRequest()).GetType(), typeof(Select));
        }

        [TestMethod]
        public void RequestInsertQuery()
        {
            Assert.AreEqual(GetQueryFactory().GetQuery(ObjectConstructor.GetInsertRequest()).GetType(), typeof(Insert));
        }

        [TestMethod]
        public void RequestDropTableQuery()
        {
            Assert.AreEqual(GetQueryFactory().GetQuery(ObjectConstructor.GetDropTableRequest()).GetType(), typeof(Drop));
        }

        [TestMethod]
        public void RequestCreateTableQuery()
        {
            Assert.AreEqual(GetQueryFactory().GetQuery(ObjectConstructor.GetCreateTableRequest()).GetType(), typeof(Create));
        }

        [TestMethod]
        public void RequestDeleteTableQuery()
        {
            Assert.AreEqual(GetQueryFactory().GetQuery(ObjectConstructor.GetDeleteRequest()).GetType(), typeof(Delete));
        }

        [TestMethod]
        public void RequestUpdateTableQuery()
        {
            Assert.AreEqual(GetQueryFactory().GetQuery(ObjectConstructor.GetUpdateRequest()).GetType(), typeof(Update));
        }

        public static QueryFactory GetQueryFactory() 
        {
            QueryFactory queryFactory = QueryFactory.GetQueryFactory();
            queryFactory.SetContainer(new DatabaseContainer());
            return queryFactory;
        }

    }
}
