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
    public class GrantPrivilege : PrivilegeManipulationQuery
    {
        private Dictionary<string, string> values;

        public GrantPrivilege(IDatabaseContainer container) : base(container)
        {
            this.values = new Dictionary<string, string>();
        }

        public override bool ValidateParameters()
        {
            string databaseName = this.values[SystemeConstants.PrivilegesOfProfilesOnTablesDatabaseNameColumnName];
            string tableName = this.values[SystemeConstants.PrivilegesOfProfilesOnTablesTableNameColumnName];
            if (!this.GetContainer().ExistDatabase(databaseName)) this.SaveTheError(QuerysStringResultConstants.DatabaseDoesntExist(databaseName));
            else if (!this.GetContainer().GetDatabase(databaseName).ExistTable(tableName)) this.SaveTheError(QuerysStringResultConstants.TableDoensExist(databaseName, tableName));
            else
            {
                ITable table = this.GetContainer().GetDatabase(this.targetDatabase).GetTable(this.targetTableName);
                if (!table.foreignKey.Evaluate(this.values)) this.SaveTheError(QuerysStringResultConstants.ForeignKeyError);
                else if (!table.primaryKey.Evaluate(this.values)) this.SaveTheError(QuerysStringResultConstants.PrimaryKeyError);
            }
            return this.GetIsValidQuery();
        }

        public override void ExecuteParticularQueryAction()
        {
            ITable table = this.GetContainer().GetDatabase(this.targetDatabase).GetTable(this.targetTableName);
            Row row = table.CreateRowDefinition();
            IEnumerator<string> keyEnumerator = this.values.Keys.GetEnumerator();
            while (keyEnumerator.MoveNext()) row.GetCell(keyEnumerator.Current).data = this.values[keyEnumerator.Current];
            table.AddRow(row);
        }

        public override string GetNeededExecutePrivilege()
        {
            throw new NotImplementedException();
        }

        public void SetData(string privilegeName, string profileName, string databaseName, string tableName)
        {
            this.values.Add(SystemeConstants.PrivilegesOfProfilesOnTablesProfileColumnName, profileName);
            this.values.Add(SystemeConstants.PrivilegesOfProfilesOnTablesPrivilegeColumnName, privilegeName);
            this.values.Add(SystemeConstants.PrivilegesOfProfilesOnTablesDatabaseNameColumnName, databaseName);
            this.values.Add(SystemeConstants.PrivilegesOfProfilesOnTablesTableNameColumnName, tableName);
        }

        /**
         * 
         * 
         * */
    }
}
