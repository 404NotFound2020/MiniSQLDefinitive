using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Comparers;

namespace UnitTests.Test.Comparers
{
    [TestClass]
    public class TestDictionaryComparer
    {

        //Equals_TwoEqualDatabase_ReturnTrue
        [TestMethod]
        public void Equals_TwoSameDictionary_ReturnTrue()
        {
            Dictionary<string, string> dic1 = new Dictionary<string, string>();
            Dictionary<string, string> dic2 = new Dictionary<string, string>();
            dic1.Add("a", "aa");
            dic1.Add("b", "bb");
            dic1.Add("c", "cc");

            dic2.Add("a", "aa");
            dic2.Add("b", "bb");
            dic2.Add("c", "cc");
            DictionaryComparer<string, string> comparer = new DictionaryComparer<string, string>(EqualityComparer<string>.Default);
            Assert.IsTrue(comparer.Equals(dic1, dic2));
        }

        [TestMethod]
        public void Equals_TwoDiferentDictionary_ReturnFalse()
        {
            Dictionary<string, string> dic1 = new Dictionary<string, string>();
            Dictionary<string, string> dic2 = new Dictionary<string, string>();
            dic1.Add("a", "aa");
            dic2.Add("h", "kk");
            DictionaryComparer<string, string> comparer = new DictionaryComparer<string, string>(EqualityComparer<string>.Default);
            Assert.IsFalse(comparer.Equals(dic1, dic2));
        }

        [TestMethod]
        public void Equals_TwoDictionaryWithSameKeysButDiferentValues_ReturnFalse()
        {
            Dictionary<string, string> dic1 = new Dictionary<string, string>();
            Dictionary<string, string> dic2 = new Dictionary<string, string>();
            dic1.Add("a", "aa");
            dic2.Add("a", "kk");
            DictionaryComparer<string, string> comparer = new DictionaryComparer<string, string>(EqualityComparer<string>.Default);
            Assert.IsFalse(comparer.Equals(dic1, dic2));        
        }


    }
}
