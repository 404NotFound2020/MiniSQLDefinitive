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
        private List<int> selectedRowsIndexInTable;
             
        public Select(IDatabaseContainer container) : base(container)
        {
            this.selectedColumnNames = new List<string>();
            this.selectedRowsIndexInTable = new List<int>();
        }

        public override void ExecuteParticularQueryAction(Table table)
        {
            IEnumerator<Row> rowEnumerator = table.GetRowEnumerator();
            int i = 0;
            string result = "";
            while (rowEnumerator.MoveNext()) 
            {
                if (this.whereClause.IsSelected(rowEnumerator.Current)) 
                {
                    this.AddAfectedRow(rowEnumerator.Current);
                    result = result + "\n" + this.GenerateStringResult(rowEnumerator.Current);
                    this.selectedRowsIndexInTable.Add(i);
                }
                i = i + 1;
            }
            this.SetResult(this.GenerateHeader() + result);
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
            if (!columnName.Equals("*"))
            {
                this.selectedColumnNames.Add(columnName);
            }
            else {
                this.selectedAllColumns = true; 
            }
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
                        this.SetResult(QuerysStringResultConstants.SelectedColumnDoenstExistError(enumerator.Current));
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
 
        public IEnumerator<int> GetSelectedRowsIndex()
        {
            return this.selectedRowsIndexInTable.GetEnumerator();
        }


    }
}
