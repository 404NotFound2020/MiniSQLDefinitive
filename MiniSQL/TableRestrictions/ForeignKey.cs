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
        private Dictionary<string, Tuple<Column, Column>> foreignKeys;

        public ForeignKey() 
        {
            this.foreignKeys = new Dictionary<string, Tuple<Column, Column>>();
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
