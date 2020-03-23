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
    public class Update : DataManipulationQuery
    {
        private Dictionary<string, string> updateColumnData;

        public Update(IDatabaseContainer container) : base(container)
        {
            this.updateColumnData = new Dictionary<string, string>();   
        }

        protected override void ValidateParameters(Table table)
        {
            IEnumerator<KeyValuePair<string, string>> enumerator = this.updateColumnData.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (!table.ExistColumn(enumerator.Current.Key))
                {
                    this.SetResult(QuerysStringResultConstants.SelectedColumnDoenstExistError(enumerator.Current.Key));
                    this.IncrementErrorCount();
                }
                else if (!table.GetColumn(enumerator.Current.Key).dataType.IsAValidDataType(enumerator.Current.Value))
                {
                    this.SetResult(QuerysStringResultConstants.ColumnsAndDataTypesError(enumerator.Current.Key, table.GetColumn(enumerator.Current.Key).dataType.GetSimpleTextValue()));
                    this.IncrementErrorCount();
                }
            }
        }

        public override void ExecuteParticularQueryAction(Table table)
        {
            IEnumerator<Row> rowEnumerator = table.GetRowEnumerator();
            while (rowEnumerator.MoveNext())
            {
                if (this.whereClause.IsSelected(rowEnumerator.Current))
                {
                    IEnumerator<KeyValuePair<string, string>> rowColum = updateColumnData.GetEnumerator();
                    while (rowColum.MoveNext())
                    {
                        rowEnumerator.Current.GetCell(rowColum.Current.Key).data = rowColum.Current.Value;
                    }
                    this.AddAfectedRow(rowEnumerator.Current);
                }
            }
            this.SetResult("Total rows updated: " + this.GetAfectedRowCount());
        }

        public void AddValue(string columnName, string value)
        {
            if (!this.updateColumnData.ContainsKey(columnName)) {
                this.updateColumnData.Add(columnName, value);
            }
            else
            {
                this.updateColumnData[columnName] = value;
            }
 
        }


    }
}
