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
        private string databaseName;
        private string tableName;
        private string privilegeName;
        private string profileName;

        public GrantPrivilege(IDatabaseContainer container) : base(container)
        {
           
        }

        public override void ExecuteParticularQueryAction()
        {
           ITable table = this.GetContainer().GetDatabase(this.targetDatabase).GetTable(this.targetTableName);
            Row row = table.CreateRowDefinition();
            row.GetCell(SystemeConstants.PrivilegesOfProfilesOnTablesProfileColumnName).data = this.profileName;
            row.GetCell(SystemeConstants.PrivilegesOfProfilesOnTablesPrivilegeColumnName).data = this.privilegeName;
            row.GetCell(SystemeConstants.PrivilegesOfProfilesOnTablesDatabaseNameColumnName).data = this.databaseName;
            row.GetCell(SystemeConstants.PrivilegesOfProfilesOnTablesTableNameColumnName).data = this.tableName;
            table.AddRow(row);

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
                    newValues.Add(SystemeConstants.PrivilegesOfProfilesOnTablesPrivilegeColumnName, this.privilegeName);
                    ITable table = container.GetDatabase(this.targetDatabase).GetTable(this.targetTableName);
                    if (!table.foreignKey.Evaluate(newValues)) this.SaveTheError("Fk error");
                    else
                    {
                        newValues.Add(SystemeConstants.PrivilegesOfProfilesOnTablesDatabaseNameColumnName, this.databaseName);
                        newValues.Add(SystemeConstants.PrivilegesOfProfilesOnTablesTableNameColumnName, this.tableName);
                        if (!table.primaryKey.Evaluate(newValues)) this.SaveTheError("Pk error");
                    }
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
