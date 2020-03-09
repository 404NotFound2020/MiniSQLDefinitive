using MiniSQL.Classes;
using MiniSQL.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    public abstract class DataChangeQuery : DataManipulationQuery
    {
        private List<Tuple<string, string>> afectedColumnsAndValues;

        public DataChangeQuery(IDatabaseContainer container) : base(container) 
        {
            this.afectedColumnsAndValues = new List<Tuple<string, string>>();        
        }

        protected override void ValidateParameters(Table table) 
        {
            IEnumerator<Tuple<string, string>> enumerator = this.afectedColumnsAndValues.GetEnumerator();
            while (enumerator.MoveNext()) 
            {
                if (!table.ExistColumn(enumerator.Current.Item1)) 
                {
                    this.SetResult(this.GetResult() + QuerysStringResultConstants.SelectedColumnDoenstExistError(enumerator.Current.Item1) + "\n");
                    this.IncrementErrorCount();
                }
                else if (!table.GetColumn(enumerator.Current.Item1).dataType.IsAValidDataType(enumerator.Current.Item2))
                {
                    this.SetResult(this.GetResult() + QuerysStringResultConstants.ColumnsAndDataTypesError(enumerator.Current.Item1, table.GetColumn(enumerator.Current.Item1).dataType.GetSimpleTextValue()) + "\n");
                    this.IncrementErrorCount();
                }
            }
        }

        public void AddValue(string columnName, string value) 
        {
            this.afectedColumnsAndValues.Add(new Tuple<string, string>(columnName, value));           
        }

        protected IEnumerator<Tuple<string, string>> GetColumnAndDataEnumerator() 
        {
            return this.afectedColumnsAndValues.GetEnumerator();
        }





    }
}
