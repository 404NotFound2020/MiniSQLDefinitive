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
    public class Insert : DataManipulationQuery
    {
        private List<string> values;

        public Insert(IDatabaseContainer container) : base(container)
        {
            this.values = new List<string>();
        }

        protected override void ValidateParameters(Table table)
        {
            if(this.values.Count > table.GetColumnCount()) this.SaveTheError(QuerysStringResultConstants.TooMuchValues);
            else 
            {
                IEnumerator<string> valuesEnumerator = this.values.GetEnumerator();
                IEnumerator<Column> columnEnumerator = table.GetColumnEnumerator();
                while(valuesEnumerator.MoveNext()  && columnEnumerator.MoveNext()) 
                {                    
                    if (!columnEnumerator.Current.dataType.IsAValidDataType(valuesEnumerator.Current)) this.SaveTheError(QuerysStringResultConstants.ColumnsAndDataTypesError(columnEnumerator.Current.columnName, columnEnumerator.Current.dataType.GetSimpleTextValue()));
                }
                if (!table.primaryKey.Evaluate(this.values)) this.SaveTheError(QuerysStringResultConstants.PrimaryKeyError);
            }
        }

        public override void ExecuteParticularQueryAction(Table table)
        {
            IEnumerator<string> valuesEnumerator = values.GetEnumerator();
            IEnumerator<Column> columnEnumerator = table.GetColumnEnumerator();
            Row newRow = table.CreateRowDefinition();
            while(valuesEnumerator.MoveNext() && columnEnumerator.MoveNext()) 
            {
                newRow.GetCell(columnEnumerator.Current.columnName).data = valuesEnumerator.Current;
            }
            table.AddRow(newRow);
            this.SetResult(this.GetResult() + QuerysStringResultConstants.InsertSuccess);
        }

        public void AddValue(string value) 
        {
            this.values.Add(value);        
        }


    }
}
