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
        private Dictionary<string, string> columnsNameAndDataValues;
        private List<String> selectedColumnNames;
        private List<String> insertedDate;

        public Insert(IDatabaseContainer container) : base(container)
        {
            this.columnsNameAndDataValues = new Dictionary<string, string>();
            this.selectedColumnNames = new List<string>();
            this.insertedDate = new List<string>();
        }

        public override void ExecuteParticularQueryAction(Table table)
        {
            
            Row row = table.CreateRowDefinition();
            //FALTA CÓDIGO


            
        }

        protected override void ValidateParameters(Table table)
        {
            IEnumerator<string> enumerator = columnsNameAndDataValues.Keys.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (!table.ExistColumn(enumerator.Current))
                {
                    this.SetResult(this.GetResult() + QuerysStringResultConstants.SelectedColumnDoenstExistError(enumerator.Current) + "\n");
                    this.IncrementErrorCount();
                }
                else if(!table.GetColumn(enumerator.Current).dataType.IsAValidDataType(this.columnsNameAndDataValues[enumerator.Current])) 
                {
                    this.IncrementErrorCount();
                }
            }
        }

        public void AddValue(string columnName, string cellValue) 
        {
            columnsNameAndDataValues.Add(columnName, cellValue);
        }
    }
}
