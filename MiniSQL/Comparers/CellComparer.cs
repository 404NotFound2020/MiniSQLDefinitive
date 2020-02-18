using MiniSQL.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Comparers
{
    public class CellComparer : IEqualityComparer<Cell>
    {
        public bool Equals(Cell x, Cell y)
        {
            return x.data.Equals(y.data);
        }

        public int GetHashCode(Cell obj)
        {
            throw new NotImplementedException();
        }
    }
}
