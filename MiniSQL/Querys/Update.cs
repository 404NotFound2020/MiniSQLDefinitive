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

        protected override void ValidateParameters(ITable table)
        {
            IEnumerator<KeyValuePair<string, string>> enumerator = this.updateColumnData.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (!table.ExistColumn(enumerator.Current.Key)) this.SaveTheError(QuerysStringResultConstants.SelectedColumnDoenstExistError(enumerator.Current.Key));
                else if (!table.GetColumn(enumerator.Current.Key).dataType.IsAValidDataType(enumerator.Current.Value)) this.SaveTheError(QuerysStringResultConstants.ColumnsAndDataTypesError(enumerator.Current.Key, table.GetColumn(enumerator.Current.Key).dataType.GetSimpleTextValue()));
            }
            if (this.GetIsValidQuery()) {
                if (!table.primaryKey.Evaluate(this.updateColumnData)) this.SaveTheError(QuerysStringResultConstants.PrimaryKeyError);
                if (!table.foreignKey.Evaluate(this.updateColumnData)) this.SaveTheError(QuerysStringResultConstants.ForeignKeyError);
                //VALIDAR SI EL CAMBIO AFECTA A UNA TABLA
            }
        }

        public override void ExecuteParticularQueryAction(ITable table)
        {
            IEnumerator<Row> rowEnumerator = table.GetRowEnumerator();
            int couldnotChanged = 0;
            while (rowEnumerator.MoveNext())
            {
                if (this.whereClause.IsSelected(rowEnumerator.Current))
                {
                    IEnumerator<KeyValuePair<string, string>> rowColum = updateColumnData.GetEnumerator();
                    if (!rowEnumerator.Current.CheckIfRowCouldBeChanged()) couldnotChanged = couldnotChanged + 1;
                    else
                    {
                        while (rowColum.MoveNext()) rowEnumerator.Current.GetCell(rowColum.Current.Key).data = rowColum.Current.Value;                           
                        this.AddAfectedRow(rowEnumerator.Current);
                    }
                }
            }
            this.SetResult("Total rows updated: " + this.GetAfectedRowCount() + "\nCould not updated row number: " + couldnotChanged);
        }

        public void AddValue(string columnName, string value)
        {
            if (!this.updateColumnData.ContainsKey(columnName)) this.updateColumnData.Add(columnName, value);                
            else this.updateColumnData[columnName] = value; 
        }

        public override string GetNeededExecutePrivilege()
        {
            throw new NotImplementedException();
        }
    }
}
