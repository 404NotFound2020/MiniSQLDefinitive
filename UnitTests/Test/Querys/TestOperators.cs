using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Constants;
using MiniSQL.Querys;
using UnitTests.Test.TestObjectsContructor;

namespace UnitTests.Test.Querys
{
    [TestClass]
    public class TestOperators
    {
        [TestMethod]
        public void TestEqualOperator_WithTwoEqualInt_EvaluationReturnTrue() 
        {
            Operator op = OperatorFactory.GetOperatorFactory().GetOperator(OperatorKeys.EqualKey);
            int i = 1;
            int j = 1;
            Assert.AreEqual(i, j);
            Assert.IsTrue(op.evaluate(i, j));        
        }

        [TestMethod]
        public void TestEqualOperator_WithTwoDiferentInt_EvaluationReturnFalse()
        {
            Operator op = OperatorFactory.GetOperatorFactory().GetOperator(OperatorKeys.EqualKey);
            int i = 1;
            int j = i+1;
            Assert.AreNotEqual(i, j);
            Assert.IsFalse(op.evaluate(i, j));
        }

        [TestMethod]
        public void TestEqualOperator_WithTwoEqualString_EvaluationReturnTrue()
        {
            Operator op = OperatorFactory.GetOperatorFactory().GetOperator(OperatorKeys.EqualKey);
            string st1 = "a";
            string st2 = "a";
            Assert.AreEqual(st1, st2);
            Assert.IsTrue(op.evaluate(st1, st2));
        }

        [TestMethod]
        public void TestEqualOperator_WithTwoDiferentString_EvaluationReturnFalse()
        {
            Operator op = OperatorFactory.GetOperatorFactory().GetOperator(OperatorKeys.EqualKey);
            string st1 = "a";
            string st2 = st1 + "a";
            Assert.AreNotEqual(st1, st2);
            Assert.IsFalse(op.evaluate(st1, st2));
        }

        [TestMethod]
        public void TestEqualOperator_WithTwoEqualDouble_EvaluationReturnTrue()
        {
            Operator op = OperatorFactory.GetOperatorFactory().GetOperator(OperatorKeys.EqualKey);
            double d1 = 1.3;
            double d2 = 1.3;
            Assert.AreEqual(d1, d2);
            Assert.IsTrue(op.evaluate(d1, d2));
        }

        [TestMethod]
        public void TestEqualOperator_WithTwoDiferentDouble_EvaluationReturnFalse()
        {
            Operator op = OperatorFactory.GetOperatorFactory().GetOperator(OperatorKeys.EqualKey);
            double d1 = 1.3;
            double d2 = d1 + 1.3;
            Assert.AreNotEqual(d1, d2);
            Assert.IsFalse(op.evaluate(d1, d2));
        }

        [TestMethod]
        public void TestLessOperatorWithInt_FirstIntIsLesserThanSecondInt_EvaluationReturnTrue()
        {
            Operator op = OperatorFactory.GetOperatorFactory().GetOperator(OperatorKeys.LessKey);
            int i = 1;
            int j = 2;
            Assert.IsTrue(i < j);
            Assert.IsTrue(op.evaluate(i, j));
        }

        [TestMethod]
        public void TestLessOperatorWithInt_SecondIntIsLesserThanFirstInt_EvaluationReturnFalse()
        {
            Operator op = OperatorFactory.GetOperatorFactory().GetOperator(OperatorKeys.LessKey);
            int i = 2;
            int j = 1;
            Assert.IsTrue(!(i < j));
            Assert.IsFalse(op.evaluate(i, j));
        }

        [TestMethod]
        public void TestLessOperatorWithDouble_FirstDoubleIsLesserThanSecondDouble_EvaluationReturnTrue()
        {
            Operator op = OperatorFactory.GetOperatorFactory().GetOperator(OperatorKeys.LessKey);
            double d1 = 1.1;
            double d2 = 2.2;
            Assert.IsTrue(d1 < d2);
            Assert.IsTrue(op.evaluate(d1, d2));
        }

        [TestMethod]
        public void TestLessOperatorWithDouble_SecondDoubleIsLesserThanFirstDouble_EvaluationReturnTrue()
        {
            Operator op = OperatorFactory.GetOperatorFactory().GetOperator(OperatorKeys.LessKey);
            double d1 = 2.2;
            double d2 = 1.2;
            Assert.IsTrue(!(d1 < d2));
            Assert.IsFalse(op.evaluate(d1, d2));
        }

        [TestMethod]
        public void TestLessOperatorWithString_FirstStringIsLesserThanSecondString_ReturnTrue()
        {
            Operator op = OperatorFactory.GetOperatorFactory().GetOperator(OperatorKeys.LessKey);
            string st1 = "aa";
            string st2 = VariousFunctions.GenerateRandomString(st1.Length);
            while(!(st1.CompareTo(st2) == -1))
            {
                st2 = VariousFunctions.GenerateRandomString(st1.Length);
            }
            Assert.AreEqual(-1, st1.CompareTo(st2)); //I dont trust nothing
            Assert.IsTrue(op.evaluate(st1, st2));            
        }

        [TestMethod]
        public void TestLessOperatorWithString_SecondStringIsLesserThanFirstString_ReturnFalse()
        {
            Operator op = OperatorFactory.GetOperatorFactory().GetOperator(OperatorKeys.LessKey);
            string st1 = "aa";
            string st2 = VariousFunctions.GenerateRandomString(st1.Length);
            while (!(st2.CompareTo(st1) == -1))
            {
                st2 = VariousFunctions.GenerateRandomString(st1.Length);
            }
            Assert.AreEqual(-1, st2.CompareTo(st1)); 
            Assert.IsFalse(op.evaluate(st1, st2));
        }

        [TestMethod]
        public void TestHigherOperatorWithInt_FirstIntIsHigherThanSecondInt_EvaluationReturnTrue()
        {
            Operator op = OperatorFactory.GetOperatorFactory().GetOperator(OperatorKeys.HigherKey);
            int i = 2;
            int j = 1;
            Assert.IsTrue(i > j);
            Assert.IsTrue(op.evaluate(i, j));
        }

        [TestMethod]
        public void TestHigherOperatorWithInt_SecondIntIsHigherThanFirstInt_EvaluationReturnFalse()
        {
            Operator op = OperatorFactory.GetOperatorFactory().GetOperator(OperatorKeys.HigherKey);
            int i = 1;
            int j = 2;
            Assert.IsTrue(!(i > j)); //Tactical ! (are ! are tactical)
            Assert.IsFalse(op.evaluate(i, j));
        }

        [TestMethod]
        public void TestHigherOperatorWithDouble_FirstDoubleIsHigherThanSecondDouble_EvaluationReturnTrue()
        {
            Operator op = OperatorFactory.GetOperatorFactory().GetOperator(OperatorKeys.HigherKey);
            double d1 = 2.2;
            double d2 = 1.1;
            Assert.IsTrue(d1 > d2);
            Assert.IsTrue(op.evaluate(d1, d2));
        }

        [TestMethod]
        public void TestHigherOperatorWithDouble_SecondDoubleIsHigherThanFirstDouble_EvaluationReturnFalse()
        {
            Operator op = OperatorFactory.GetOperatorFactory().GetOperator(OperatorKeys.HigherKey);
            double d1 = 1.1;
            double d2 = 2.2;
            Assert.IsTrue(!(d1 > d2));
            Assert.IsFalse(op.evaluate(d1, d2));
        }

        [TestMethod]
        public void TestHigherOperatorWithString_FirstStringIsHigherThanSecondString_ReturnTrue()
        {
            Operator op = OperatorFactory.GetOperatorFactory().GetOperator(OperatorKeys.HigherKey);
            string st2 = "aa";
            string st1 = VariousFunctions.GenerateRandomString(st2.Length);
            while (!(st1.CompareTo(st2) == 1))
            {
                st1 = VariousFunctions.GenerateRandomString(st2.Length);
            }
            Assert.AreEqual(1, st1.CompareTo(st2));
            Assert.IsTrue(op.evaluate(st1, st2));
        }

        [TestMethod]
        public void TestHigherOperatorWithString_SecondStringIsHigherThanFirstString_ReturnFalse()
        {
            Operator op = OperatorFactory.GetOperatorFactory().GetOperator(OperatorKeys.HigherKey);
            string st2 = "ha";
            string st1 = VariousFunctions.GenerateRandomString(st2.Length);
            while (!(st2.CompareTo(st1) == 1))
            {
                st1 = VariousFunctions.GenerateRandomString(st2.Length);
            }
            Assert.AreEqual(1, st2.CompareTo(st1));
            Assert.IsFalse(op.evaluate(st1, st2));
        }

    }
}
