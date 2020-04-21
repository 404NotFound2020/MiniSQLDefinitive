using MiniSQL.Classes;
using MiniSQL.Constants;
using MiniSQL.Querys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    public abstract class DataManipulationQuery : AbstractQuery
    {
        private IList<Row> afectedRows;
        public Where whereClause;

        public DataManipulationQuery(IDatabaseContainer container) : base(container)
        {
            this.afectedRows = new List<Row>();
            this.whereClause = new Where();
        }

        protected void AddAfectedRow(Row row) 
        {
            this.afectedRows.Add(row);
        }

        public IEnumerator<Row> GetAfectedRowEnum() 
        {
            return this.afectedRows.GetEnumerator();
        }

        public int GetAfectedRowCount() 
        {
            return this.afectedRows.Count();
        }

        public override void Execute()
        {
            if(this.GetErrorCount() == 0) this.ExecuteParticularQueryAction(this.GetContainer().GetDatabase(this.targetDatabase).GetTable(this.targetTableName));                
        }

        public override bool ValidateParameters()
        {            
            IDatabaseContainer container =  this.GetContainer();
            if (!container.ExistDatabase(this.targetDatabase)) this.SaveTheError(QuerysStringResultConstants.DatabaseDoesntExist(this.targetDatabase));
            else 
            {
                IDatabase targetDatabaseObject = container.GetDatabase(this.targetDatabase);
                if (!targetDatabaseObject.ExistTable(this.targetTableName)) this.SaveTheError(QuerysStringResultConstants.TableDoensExist(this.targetDatabase, this.targetTableName));
                else if(!this.GetPrivilegeModule().CheckProfileTablePrivileges(this.username, this.targetDatabase, this.targetTableName, this.GetNeededExecutePrivilege())) this.SaveTheError("Not enought privileges");
                else this.DoTheOtherValidations(targetDatabaseObject.GetTable(this.targetTableName));
            }
            return this.GetIsValidQuery();
        }

        private void DoTheOtherValidations(ITable table) 
        {
            this.ValidateParameters(table);
            this.ValidateWhere(table);
        }

        protected void ValidateWhere(ITable table) 
        {
            IEnumerator<Tuple<string, string>> enumerator = this.whereClause.GetCriteriesEnumerator();
            Column column;
            while (enumerator.MoveNext()) 
            {
                if (!table.ExistColumn(enumerator.Current.Item1)) this.SaveTheError(QuerysStringResultConstants.WhereClauseColumnDoensExist(enumerator.Current.Item1));
                else 
                {
                    column = table.GetColumn(enumerator.Current.Item1);
                    if (!column.dataType.IsAValidDataType(enumerator.Current.Item2)) this.SaveTheError(QuerysStringResultConstants.WhereClauseColumnDataTypeError(enumerator.Current.Item1) + ". The column data type is" + column.dataType.GetSimpleTextValue());                        
                }
            }
        }
       
        protected abstract void ValidateParameters(ITable table);
        public abstract void ExecuteParticularQueryAction(ITable table);

    }
}
