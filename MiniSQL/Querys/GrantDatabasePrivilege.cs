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
    public class GrantDatabasePrivilege : PrivilegeManipulationQuery
    {
        private Dictionary<string, string> values;
        
        public GrantDatabasePrivilege(IDatabaseContainer container) : base(container)
        {
            this.values = new Dictionary<string, string>();
        }

        protected override void Validate()
        {
            string databaseName = this.values[SystemeConstants.PrivilegesOfProfilesOnDatabasesDatabaseNameColumnName];
            if (!this.GetContainer().ExistDatabase(databaseName)) this.SaveTheError(QuerysStringResultConstants.DatabaseDoesntExist(databaseName));
            else
            {
                ITable table = this.GetContainer().GetDatabase(this.targetDatabase).GetTable(this.targetTableName);
                if (!table.foreignKey.Evaluate(this.values)) this.SaveTheError(QuerysStringResultConstants.ForeignKeyError);
                else if (!table.primaryKey.Evaluate(this.values)) this.SaveTheError(QuerysStringResultConstants.PrimaryKeyError);
            }
        }

        public override void ExecuteParticularQueryAction()
        {
            ITable table = this.GetContainer().GetDatabase(this.targetDatabase).GetTable(this.targetTableName);
            Row row = table.CreateRowDefinition();
            IEnumerator<string> keyEnumerator = this.values.Keys.GetEnumerator();
            while (keyEnumerator.MoveNext()) row.GetCell(keyEnumerator.Current).data = this.values[keyEnumerator.Current];
            table.AddRow(row);
            this.SetResult(QuerysStringResultConstants.SecurityPrivilegeGranted);
        }

        public override string GetNeededExecutePrivilege()
        {
            throw new NotImplementedException();
        }

        public void SetData(string privilegeName, string profileName, string databaseName)
        {
            this.values.Add(SystemeConstants.PrivilegesOfProfilesOnDatabasesProfileColumnName, profileName);
            this.values.Add(SystemeConstants.PrivilegesOfProfilesOnDatabasesPrivilegeColumnName, privilegeName);
            this.values.Add(SystemeConstants.PrivilegesOfProfilesOnDatabasesDatabaseNameColumnName, databaseName);
        }

    }
}
