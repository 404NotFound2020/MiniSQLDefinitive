using MiniSQL.Classes;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.TableRestrictions
{
    public class ForeignKey
    {
        public Table table;
        private Dictionary<string, Tuple<Column, Column>> foreignKeys;

        public ForeignKey(Table table) 
        {
            this.table = table;
            this.foreignKeys = new Dictionary<string, Tuple<Column, Column>>();
        }

        public bool Evaluate(Dictionary<string, string> valuesAndColumns)
        {
            return this.Evaluate<string>(valuesAndColumns.Keys.GetEnumerator(), valuesAndColumns.Values.GetEnumerator(), (column) => { return column; });
        }

        public bool Evaluate(List<string> values)
        {
            return this.Evaluate<Column>(this.table.GetColumnEnumerator(), values.GetEnumerator(), (column) => { return column.columnName; });
        }

        public bool Evaluate<T>(IEnumerator<T> tableColumnsEnumerator, IEnumerator<string> valuesEnumerator, Func<T, string> getColumnName)
        {
            bool b = true;
            string columnName;
            while (tableColumnsEnumerator.MoveNext() && valuesEnumerator.MoveNext() && b == true)
            {
                columnName = getColumnName.Invoke(tableColumnsEnumerator.Current);
                if (this.foreignKeys.ContainsKey(columnName)) b = this.foreignKeys[columnName].Item2.ExistCells(valuesEnumerator.Current);
            }
            return b;
        }

        public void AddForeignKey(Column column, Column foreignKey) {
            this.foreignKeys.Add(column.columnName, new Tuple<Column, Column>(column, foreignKey));
            foreignKey.AddColumnThatReferenceThisOne(GetComposedKey(column), column);
        }

        public bool IsAForeignKey(string columnName) 
        {
            return this.foreignKeys.ContainsKey(columnName);
        }

        public void RemoveForeignKey(string columnName) 
        {
            Tuple<Column, Column> pair = this.foreignKeys[columnName];
            this.foreignKeys.Remove(columnName);
            pair.Item2.RemoveColumnThatReferenceThisOne(this.GetComposedKey(pair.Item1));
        }

        public IEnumerator<Tuple<Column, Column>> GetPairEnumerator() 
        {
            return this.foreignKeys.Values.GetEnumerator();
        }

        private string GetComposedKey(Column column) 
        {
            return column.table.tableName + "." + column.columnName;        
        }
    }
}
