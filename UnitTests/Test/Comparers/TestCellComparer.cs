using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Classes;
using MiniSQL.Comparers;

namespace UnitTests.Test.Comparers
{
    [TestClass]
    public class TestCellComparer
    {

        //Equals(Cell x, Cell y)ExistTable_Exist_ReturnTrue()
        [TestMethod]
        public void Equals_TwoCellWithSameData_ReturnTrue()
        {
            CellComparer cellComparer = new CellComparer();
            Cell cell1 = new Cell(null, "str", null);
            Cell cell2 = new Cell(null, "str", null);
            Assert.IsTrue(cellComparer.Equals(cell1, cell2));
        }

        public void Equals_TwoCellWithDiferentData_ReturnFalse()
        {
            CellComparer cellComparer = new CellComparer();
            Cell cell1 = new Cell(null, "str", null);
            Cell cell2 = new Cell(null, "str", null);
            Assert.IsFalse(cellComparer.Equals(cell1, cell2));
        }


        public static CellComparer CreateCellComparer() 
        {
            return new CellComparer();        
        }

    }
}
