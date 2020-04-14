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
        private string databaseName;
        private string tableName;
        private string privilegeName;
        private string profileName;

        public GrantPrivilege(IDatabaseContainer container) : base(container)
        {
           
        }

    public override void ExecuteParticularQueryAction()
        {
            throw new NotImplementedException();
        }

        public override string GetNeededExecutePrivilege()
        {
            throw new NotImplementedException();
        }

        public override bool ValidateParameters()
        {
            IDatabaseContainer container = this.GetContainer();
            if (!container.ExistDatabase(this.databaseName)) this.SaveTheError("The database doenst exits");
            else
            {
                if (!container.GetDatabase(this.databaseName).ExistTable(this.tableName)) this.SaveTheError("The table doenst exist");
                else
                {
                    Dictionary<string, string> newValues = new Dictionary<string, string>();
                    newValues.Add(SystemeConstants.PrivilegesOfProfilesOnTablesProfileColumnName, this.profileName);
                    newValues.Add(SystemeConstants.PrivilegesOfProfilesOnTablesDatabaseNameColumnName, this.databaseName);
                    newValues.Add(SystemeConstants.PrivilegesOfProfilesOnTablesTableNameColumnName, this.tableName);
                    newValues.Add(SystemeConstants.PrivilegesOfProfilesOnTablesPrivilegeColumnName, this.privilegeName);
                    if (!container.GetDatabase(this.targetDatabase).GetTable(this.targetTableName).foreignKey.Evaluate(newValues)) this.SaveTheError("Fk error");
                }
            }
            return this.GetIsValidQuery();

        }

        public void SetData( string databaseName, string tableName, string privilegeName, string profileName)
        {
            this.databaseName = databaseName;
            this.tableName = tableName;
            this.privilegeName = privilegeName;
            this.profileName = profileName;
        }
    }


}
