using MiniSQL.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Comparers
{
    public class RowComparer : IEqualityComparer<Row>
    {

        public bool Equals(Row x, Row y)
        {
            return new DictionaryComparer<string, Cell>(new CellComparer()).Equals(x.ReadCells(), y.ReadCells()); 
        }

        public int GetHashCode(Row obj)
        {
            throw new NotImplementedException();
        }
    }
}
