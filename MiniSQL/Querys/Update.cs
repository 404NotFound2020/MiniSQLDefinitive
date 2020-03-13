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
        private Dictionary<string, string> afectedColumnsAndValues;

        public Update(IDatabaseContainer container) : base(container)
        {
            this.afectedColumnsAndValues = new Dictionary<string, string>();   
        }

        protected override void ValidateParameters(Table table)
        {
            IEnumerator<KeyValuePair<string, string>> enumerator = this.afectedColumnsAndValues.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (!table.ExistColumn(enumerator.Current.Key))
                {
                    this.SetResult(this.GetResult() + QuerysStringResultConstants.SelectedColumnDoenstExistError(enumerator.Current.Key) + "\n");
                    this.IncrementErrorCount();
                }
                else if (!table.GetColumn(enumerator.Current.Key).dataType.IsAValidDataType(enumerator.Current.Value))
                {
                    this.SetResult(this.GetResult() + QuerysStringResultConstants.ColumnsAndDataTypesError(enumerator.Current.Key, table.GetColumn(enumerator.Current.Key).dataType.GetSimpleTextValue()) + "\n");
                    this.IncrementErrorCount();
                }
            }
        }

        public override void ExecuteParticularQueryAction(Table table)
        {
            
           /*IEnumerator<Row> rowEnumerator = table.GetRowEnumerator();
            while (rowEnumerator.MoveNext())
            {
                if (this.whereClause.IsSelected(rowEnumerator.Current))
                {
                    IEnumerator<KeyValuePair<string, string>> rowColum = updateColumnData.GetEnumerator();
                    while(rowColum.MoveNext())
                    {
                        rowEnumerator.Current.GetCell(rowColum.Current.Key).data=rowColum.Current.Value;
                    }
                }
            }  
            */

        }

        public void AddValue(string columnName, string value)
        {
            if (!this.afectedColumnsAndValues.ContainsKey(columnName)) {
                this.afectedColumnsAndValues.Add(columnName, value);
            }
            else
            {
                this.afectedColumnsAndValues[columnName] = value;
            }
 
        }


    }
}
