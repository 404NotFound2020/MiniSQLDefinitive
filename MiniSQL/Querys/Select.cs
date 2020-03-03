using MiniSQL.Classes;
using MiniSQL.Constants;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Querys
{
    public class Select : DataManipulationQuery
    {
        private List<string> selectedColumnNames;
        public bool selectedAllColumns;
        private List<Row> selectedRows;
             
        public Select(IDatabaseContainer container) : base(container)
        {
            this.selectedColumnNames = new List<string>();
            this.selectedRows = new List<Row>();
        }

        public override void ExecuteParticularQueryAction(Table table)
        {
            IEnumerator<Row> rowEnumerator = table.GetRowEnumerator();
            while (rowEnumerator.MoveNext()) 
            {
                if (this.whereClause.IsSelected(rowEnumerator.Current)) 
                {
                    this.selectedRows.Add(rowEnumerator.Current);
                    this.SetResult(this.GetResult() + this.GenerateStringResult(rowEnumerator.Current) + "\n");
                }
            }
            this.SetResult(this.GenerateHeader() + "\n" + this.GetResult());
        }

        private string GenerateStringResult(Row row) 
        {
            string stringfiedRow = "{";
            IEnumerator<string> selectedColumnsEnumerator = this.selectedColumnNames.GetEnumerator();
            selectedColumnsEnumerator.MoveNext();
            stringfiedRow = stringfiedRow + "'" + row.GetCell(selectedColumnsEnumerator.Current).data + "'";
            while (selectedColumnsEnumerator.MoveNext()) 
            {
                stringfiedRow = stringfiedRow + ", '" + row.GetCell(selectedColumnsEnumerator.Current).data + "'";
            }
            return stringfiedRow + "}";        
        }

        private string GenerateHeader() 
        {
            string header = "[";
            IEnumerator<string> columnNameEnumerator = this.selectedColumnNames.GetEnumerator();
            columnNameEnumerator.MoveNext();
            header = header + "'" + columnNameEnumerator.Current + "'";
            while (columnNameEnumerator.MoveNext()) 
            {
                header = header + ", '" + columnNameEnumerator.Current + "'";
            }
            return header + "]";        
        }

        public void AddSelectedColumnName(string columnName)
        {            
            selectedColumnNames.Add(columnName);
        }

        protected override void ValidateParameters(Table table)
        {
            if (!this.selectedAllColumns)
            {
                IEnumerator<string> enumerator = selectedColumnNames.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (!table.ExistColumn(enumerator.Current))
                    {
                        this.SetResult(this.GetResult() + QuerysStringResultConstants.SelectedColumnDoenstExistError(enumerator.Current) + "\n");
                        this.IncrementErrorCount();
                    }
                }
            }
            else
            {
                this.SelectAllColumnsOfATable(table);
            }
        }

        private void SelectAllColumnsOfATable(Table table) 
        {
            IEnumerator<Column> columnEnumerator = table.GetColumnEnumerator();
            while (columnEnumerator.MoveNext()) 
            {
                this.selectedColumnNames.Add(columnEnumerator.Current.columnName);            
            }
        }

        public IEnumerator<Row> GetSelectedRows() 
        {
            return this.selectedRows.GetEnumerator();
        }

    }
}
