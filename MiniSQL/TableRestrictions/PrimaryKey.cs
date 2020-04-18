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
        private Dictionary<string, Row> keys;

        public PrimaryKey(Table table) 
        {
            this.table = table;
            this.tableKey = new Dictionary<string, Column>();
            this.keys = new Dictionary<string, Row>();
        }
        
        //For update maybe
        public bool Evaluate(Row row) {
            return !this.keys.ContainsKey(this.GetRowKey<Row>((row), (r, key) => { return r.GetCell(key).data; }));
        }
        
        public bool Evaluate(Dictionary<string, string> values) {
            return !this.keys.ContainsKey(this.GetRowKey<Dictionary<string, string>>(values, (dic, key)=> { return dic[key]; }));
        }

        public bool CanUpdate(Dictionary<string, string> values)
        {
            bool b = true;
            IEnumerator<string> tableKeysEnumerator = this.tableKey.Keys.GetEnumerator();
            while (tableKeysEnumerator.MoveNext() && b) b = !values.ContainsKey(tableKeysEnumerator.Current);
            return b;
        }

        public void AddUsedKey(Row row)
        {
            if (this.tableKey.Count > 0) { this.keys.Add(this.GetRowKey<Row>((row), (r, key) => { return r.GetCell(key).data; }), row); }                            
        }

        public void RemoveUsedKey(Row row)
        {
            if (this.tableKey.Count > 0) { this.keys.Remove(this.GetRowKey<Row>((row), (r, key) => { return r.GetCell(key).data; })); }
        }

        private string GetRowKey<T>(T t, Func<T, string, string> getCellValue)
        {
            string rowKey = "";
            IEnumerator<Column> tableKeysEnumerator = this.tableKey.Values.GetEnumerator();
            while (tableKeysEnumerator.MoveNext()) rowKey = rowKey + getCellValue.Invoke(t, tableKeysEnumerator.Current.columnName);
            return rowKey;
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
