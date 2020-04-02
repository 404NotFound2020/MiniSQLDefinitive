using MiniSQL.Classes;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.TableRestrictions
{
    public class PrimaryKey : ITableRestriction
    {
        private Table table;
        private Dictionary<string, Column> tableKey;

        public PrimaryKey(Table table) 
        {
            this.table = table;
            this.tableKey = new Dictionary<string, Column>();
        }

        public bool Evaluate(List<string> values)
        {
            return this.Evaluate<Column>(this.table.GetColumnEnumerator(), values.GetEnumerator(), (column) => { return column.columnName; });
        }

        public bool Evaluate(IEnumerable<string> columnNames, List<string> values)
        {
            return this.Evaluate<string>(columnNames.GetEnumerator(), values.GetEnumerator(), (columnName) => { return columnName; });
        }

        private bool Evaluate<T>(IEnumerator<T> tableColumnsEnumerator, IEnumerator<string> valuesEnumerator, Func<T, string> getColumnName) 
        {
            bool b = false;
            string columnName;
            int checkCounter = 0;
            while (tableColumnsEnumerator.MoveNext() && valuesEnumerator.MoveNext() && b == false && checkCounter < tableKey.Count)
            {
                columnName = getColumnName.Invoke(tableColumnsEnumerator.Current);
                if (this.tableKey.ContainsKey(columnName))
                {
                    b = !this.tableKey[columnName].ExistCells(valuesEnumerator.Current);
                    checkCounter = checkCounter + 1;
                }
            }
            return b;
        }

        public void AddKey(Column column) 
        {
            this.tableKey.Add(column.columnName, column);
        }

    }
}
