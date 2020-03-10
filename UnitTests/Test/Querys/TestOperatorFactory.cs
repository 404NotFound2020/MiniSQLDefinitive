using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Constants;
using MiniSQL.Querys;

namespace UnitTests.Test.Querys
{
    [TestClass]
    public class TestOperatorFactory
    {
        [TestMethod]
        public void TryingToGetEqualOperator()
        {
            OperatorFactory factory = GetOperatorFactory();
            Operator op = factory.GetOperator(OperatorKeys.EqualKey);
            Assert.IsTrue(op.IsSameOperator(Operator.equal));
        }

        [TestMethod]
        public void TryingToGetLessOperator()
        {
            OperatorFactory factory = GetOperatorFactory();
            Operator op = factory.GetOperator(OperatorKeys.LessKey);
            Assert.IsTrue(op.IsSameOperator(Operator.less));
        }

        [TestMethod]
        public void TryingToGetHigherOperator()
        {
            OperatorFactory factory = GetOperatorFactory();
            Operator op = factory.GetOperator(OperatorKeys.HigherKey);
            Assert.IsTrue(op.IsSameOperator(Operator.higher));
        }

        public static OperatorFactory GetOperatorFactory()
        {
            return OperatorFactory.GetOperatorFactory();
        }
    }
}
