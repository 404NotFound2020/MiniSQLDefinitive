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
    public class Delete : DataManipulationQuery
    {
        private Dictionary<string, string> columnsNameAndDataValues;

        public Delete(IDatabaseContainer container) : base(container) 
        { 
        
        }

        public override void ExecuteParticularQueryAction(Table table)
        {
            throw new NotImplementedException();
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
                else if (!table.GetColumn(enumerator.Current).dataType.IsAValidDataType(this.columnsNameAndDataValues[enumerator.Current]))
                {
                    this.IncrementErrorCount();
                }
            }
        }
    }
}
