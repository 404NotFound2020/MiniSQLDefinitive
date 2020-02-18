using MiniSQL.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Comparers
{
    public class TableComparer : IEqualityComparer<Table>
    {
        public bool Equals(Table x, Table y)
        {
            if (!x.tableName.Equals(y.tableName))
                return false;
            return new ListComparer<Column>(new ColumnComparer()).Equals(x.GetColumnList(), y.GetColumnList());
        }

        public int GetHashCode(Table obj)
        {
            throw new NotImplementedException();
        }
    }
}
