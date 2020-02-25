using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Classes;

namespace UnitTests.Test.Comparers
{
    [TestClass]
    public class TestCellComparer
    {

        
        [TestMethod]
        public void Equals_TwoCellWithSameData_ReturnTrue()
        {
            IEqualityComparer<Cell> cellComparer = CreateCellComparer();
            Cell cell1 = new Cell(null, "str", null);
            Cell cell2 = new Cell(null, "str", null);
            Assert.IsTrue(cellComparer.Equals(cell1, cell2));
        }

        public void Equals_TwoCellWithDiferentData_ReturnFalse()
        {
            IEqualityComparer<Cell> cellComparer = CreateCellComparer();
            Cell cell1 = new Cell(null, "str", null);
            Cell cell2 = new Cell(null, "str", null);
            Assert.IsFalse(cellComparer.Equals(cell1, cell2));
        }


        public static IEqualityComparer<Cell> CreateCellComparer() 
        {
            return Cell.GetCellComparer();        
        }

    }
}
