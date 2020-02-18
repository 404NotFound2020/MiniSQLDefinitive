using MiniSQL.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Comparers
{
    public class ColumnComparer : IEqualityComparer<Column>
    {
        public bool Equals(Column x, Column y)
        {
            if (!x.columnName.Equals(y.columnName))
                return false;
            if (!x.dataType.Equals(y.dataType))
                return false;
            return new DictionaryComparer<string, List<Cell>>(new ListComparer<Cell>(new CellComparer())).Equals(x.ReadCells(), y.ReadCells());
        }

        public int GetHashCode(Column obj)
        {
            throw new NotImplementedException();
        }
    }
}
