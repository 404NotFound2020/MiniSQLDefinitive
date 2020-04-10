using MiniSQL.Classes;
using MiniSQL.Comparers;
using MiniSQL.TableRestrictions;
using System;
using System.Collections.Generic;

namespace MiniSQL.Interfaces
{
    public abstract class ITable
    {
        public abstract string tableName { get; set; }
        public abstract PrimaryKey primaryKey { get; }
        public abstract ForeignKey foreignKey { get; }
        public abstract void AddColumn(Column column);
        public abstract void AddRow(Row row);
        public abstract Row CreateRowDefinition();
        public abstract bool DestroyRow(int rowNumber);
        public abstract bool ExistColumn(string columnName);
        public abstract Column GetColumn(string columnName);
        public abstract int GetColumnCount();
        public abstract IEnumerator<Column> GetColumnEnumerator();
        public abstract int GetRowCount();
        public abstract IEnumerator<Row> GetRowEnumerator();
        public abstract bool IsDropable();
        public abstract void IncrementNumberOfReferencesAtThisTable();
        public abstract void DecrementNumberOfReferencesAtThisTable();
        protected abstract List<Column> GetColumnList();

        public static IEqualityComparer<ITable> GetTableComparer(){
            return new TableComparer();
        }

        private class TableComparer : IEqualityComparer<ITable>
        {
            public bool Equals(ITable x, ITable y)
            {
                if (!x.tableName.Equals(y.tableName))
                    return false;
                return new ListComparer<Column>(Column.GetColumnComparer()).Equals(x.GetColumnList(), y.GetColumnList()) && PrimaryKey.GetPrimaryKeyComparer().Equals(x.primaryKey, y.primaryKey) && ForeignKey.GetForeignKeyComparer().Equals(x.foreignKey, y.foreignKey);
            }

            public int GetHashCode(ITable obj)
            {
                throw new NotImplementedException();
            }
        }

    }
}