using MiniSQL.Classes;
using MiniSQL.Interfaces;
using MiniSQL.TableRestrictions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Initializer
{
    public class TableProxy : ITable
    {
        public DatabaseProxy databaseProxy;
        public ITable table;
        private ISysteme systeme;

        public TableProxy(DatabaseProxy databaseProxy, ITable table, ISysteme systeme)
        {
            this.databaseProxy = databaseProxy;
            this.table = table;
            this.systeme = systeme;
        }

        public override string tableName { get => this.table.tableName; set => this.table.tableName = value; }

        public override PrimaryKey primaryKey => this.table.primaryKey;

        public override ForeignKey foreignKey => this.table.foreignKey;

        public override void AddColumn(Column column)
        {
            this.table.AddColumn(column);
            this.systeme.SaveTable(this.databaseProxy.database, this.table);
        }

        public override void AddRow(Row row)
        {
            this.table.AddRow(row);
        }

        public override Row CreateRowDefinition()
        {
            return this.table.CreateRowDefinition();
        }

        public override bool DestroyRow(int rowNumber)
        {
            return this.table.DestroyRow(rowNumber);
        }

        public override bool ExistColumn(string columnName)
        {
            return this.table.ExistColumn(columnName);
        }

        public override Column GetColumn(string columnName)
        {
            return this.table.GetColumn(columnName);
        }

        public override int GetColumnCount()
        {
            return this.table.GetColumnCount();
        }

        public override IEnumerator<Column> GetColumnEnumerator()
        {
            return this.table.GetColumnEnumerator();
        }

        public override int GetRowCount()
        {
            return this.table.GetRowCount();
        }

        public override IEnumerator<Row> GetRowEnumerator()
        {
            return this.table.GetRowEnumerator();
        }

        public override bool IsDropable()
        {
            return this.table.IsDropable();
        }

        protected override List<Column> GetColumnList()
        {
            List<Column> columnList = new List<Column>();
            IEnumerator<Column> columnEnumerator = this.table.GetColumnEnumerator();
            while (columnEnumerator.MoveNext())
            {
                columnList.Add(columnEnumerator.Current);
            }
            return columnList;
        }
    }
}
