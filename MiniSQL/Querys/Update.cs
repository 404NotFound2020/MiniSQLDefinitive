using MiniSQL.Classes;
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

        public override void ExecuteParticularQueryAction(Table table)
        {
            IEnumerator<Row> rowEnumerator = table.GetRowEnumerator();
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

            throw new NotImplementedException();
        }

        public void AddUpdateColumnData(string columnName, string newValue) 
        {
            this.updateColumnData.Add(columnName, newValue);
        }

        protected override void ValidateParameters(Table table)
        {
            IEnumerator<Row> rowEnumerator = table.GetRowEnumerator();
            while (rowEnumerator.MoveNext())
            {
                if (this.whereClause.IsSelected(rowEnumerator.Current))
                {
                    
                        if (!table.ExistColumn(rowEnumerator.Current))
                        {
                            this.SetResult(this.GetResult() + QuerysStringResultConstants.SelectedColumnDoenstExistError(rowEnumerator.Current) + "\n");
                            this.IncrementErrorCount();
                        }
                        else if (!table.GetColumn(rowEnumerator.Current).dataType.IsAValidDataType(this.updateColumnData[rowEnumerator.Current]))
                        {
                            this.IncrementErrorCount();
                        }
                    
                }
            }

            throw new NotImplementedException();
        }
    }
}
