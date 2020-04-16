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

        public override bool ValidateParameters()
        {
            if (!this.GetContainer().ExistDatabase(this.values[SystemeConstants.PrivilegesOfProfilesOnDatabasesDatabaseNameColumnName])) this.SaveTheError("The database doenst exits");
            else
            {
                ITable table = this.GetContainer().GetDatabase(SystemeConstants.SystemDatabaseName).GetTable(SystemeConstants.PrivilegesOfProfilesOnDatabasesTableName);
                if (!table.foreignKey.Evaluate(this.values)) this.SaveTheError("FK violated");
                else if (!table.primaryKey.Evaluate(this.values)) this.SaveTheError("PK violated");
            }
            return this.GetIsValidQuery();
        }

        public override void ExecuteParticularQueryAction()
        {
            ITable table = this.GetContainer().GetDatabase(SystemeConstants.SystemDatabaseName).GetTable(SystemeConstants.PrivilegesOfProfilesOnDatabasesTableName);
            Row row = table.CreateRowDefinition();
            row.GetCell(SystemeConstants.PrivilegesOfProfilesOnDatabasesProfileColumnName).data = this.values[SystemeConstants.PrivilegesOfProfilesOnDatabasesProfileColumnName];
            row.GetCell(SystemeConstants.PrivilegesOfProfilesOnDatabasesPrivilegeColumnName).data = this.values[SystemeConstants.PrivilegesOfProfilesOnDatabasesPrivilegeColumnName];
            row.GetCell(SystemeConstants.PrivilegesOfProfilesOnDatabasesDatabaseNameColumnName).data = this.values[SystemeConstants.PrivilegesOfProfilesOnDatabasesDatabaseNameColumnName];
            table.AddRow(row);
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
