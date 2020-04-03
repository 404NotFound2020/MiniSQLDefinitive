using MiniSQL.Classes;
using MiniSQL.Comparers;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.TableRestrictions
{
    public class PrimaryKey
    {
        private Table table;
        private Dictionary<string, Column> tableKey;

        public PrimaryKey(Table table) 
        {
            this.table = table;
            this.tableKey = new Dictionary<string, Column>();
        }

        public bool Evaluate<T>(IEnumerator<T> tableColumnsEnumerator, IEnumerator<string> valuesEnumerator, Func<T, string> getColumnName) 
        {
            bool b = this.tableKey.Count == 0;
            string columnName;
            while (tableColumnsEnumerator.MoveNext() && valuesEnumerator.MoveNext() && b == false)
            {
                columnName = getColumnName.Invoke(tableColumnsEnumerator.Current);
                if (this.tableKey.ContainsKey(columnName))
                {
                    b = !this.tableKey[columnName].ExistCells(valuesEnumerator.Current);
                }
            }
            return b;
        }

        public void AddKey(Column column) 
        {
            this.tableKey.Add(column.columnName, column);
        }

        public IEnumerator<Column> GetKeyEnumerator() 
        {
            return this.tableKey.Values.GetEnumerator();
        }

        public static IEqualityComparer<PrimaryKey> GetPrimaryKeyComparer()
        {
            return new PrimaryKeyComparer();
        }

        private class PrimaryKeyComparer : IEqualityComparer<PrimaryKey>
        {
            public bool Equals(PrimaryKey x, PrimaryKey y)
            {
                return new DictionaryComparer<string, Column>(Column.GetColumnComparer()).Equals(x.tableKey, y.tableKey);
            }

            public int GetHashCode(PrimaryKey obj)
            {
                throw new NotImplementedException();
            }
        }

    }
}
