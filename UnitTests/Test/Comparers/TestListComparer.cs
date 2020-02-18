using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Comparers;

namespace UnitTests.Test.Comparers
{
    [TestClass]
    public class TestListComparer
    {
        [TestMethod]
        public void Equals_TwoSameList_ReturnTrue()
        {
            List<string> list1 = new List<string>();
            List<string> list2 = new List<string>();
            list1.Add("a");
            list2.Add("a");
            list1.Add("b");
            list2.Add("b");
            list1.Add("c");
            list2.Add("c");
            ListComparer<string> listComparer = new ListComparer<string>(EqualityComparer<string>.Default);
            Assert.IsTrue(listComparer.Equals(list1, list2));
        }

        [TestMethod]
        public void Equals_TwoDiferentList_ReturnFalse()
        {
            List<string> list1 = new List<string>();
            List<string> list2 = new List<string>();
            list1.Add("a");
            list2.Add("h");
            list1.Add("b");
            list2.Add("bw");
            list1.Add("c");
            list2.Add("c");
            ListComparer<string> listComparer = new ListComparer<string>(EqualityComparer<string>.Default);
            Assert.IsFalse(listComparer.Equals(list1, list2));
        }

    }
}
