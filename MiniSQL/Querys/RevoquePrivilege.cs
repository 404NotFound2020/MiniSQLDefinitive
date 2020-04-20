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
    public class RevoquePrivilege : PrivilegeManipulationQuery
    {
        private Dictionary<string, string> values;

        public RevoquePrivilege(IDatabaseContainer container) : base(container)
        {
            this.values = new Dictionary<string, string>();
        }

        public override bool ValidateParameters()
        {
            IDatabase database = this.GetContainer().GetDatabase(this.targetDatabase);
            if (!this.GetContainer().ExistDatabase(this.values[SystemeConstants.PrivilegesOfProfilesOnTablesDatabaseNameColumnName])) this.SaveTheError("The database doesnt exist");
            else if (!this.GetContainer().GetDatabase(this.values[SystemeConstants.PrivilegesOfProfilesOnTablesDatabaseNameColumnName]).ExistTable(this.values[SystemeConstants.PrivilegesOfProfilesOnTablesTableNameColumnName])) this.SaveTheError("The table doesnt exist");
            else if (!database.GetTable(SystemeConstants.ProfilesTableName).GetColumn(SystemeConstants.ProfileNameColumn).ExistCells(this.values[SystemeConstants.PrivilegesOfProfilesOnTablesProfileColumnName])) this.SaveTheError("The profile doenst exist");
            else if (!database.GetTable(SystemeConstants.PrivilegesTableName).GetColumn(SystemeConstants.PrivilegesPrivilegeNameColumnName).ExistCells(this.values[SystemeConstants.PrivilegesOfProfilesOnTablesPrivilegeColumnName])) this.SaveTheError("The privilege doens exist");
            else if (database.GetTable(this.targetTableName).primaryKey.Evaluate(this.values)) this.SaveTheError("The values combination of profile, database, table and privilege is not in table");
            return this.GetIsValidQuery();
        }

        public override void ExecuteParticularQueryAction()
        {
            ITable table = this.GetContainer().GetDatabase(this.targetDatabase).GetTable(this.targetTableName);
            bool b = false;
            IEnumerator<Row> rowEnumerator = table.GetRowEnumerator();
            int i = -1;
            while (rowEnumerator.MoveNext() && !b)
            {
                b = rowEnumerator.Current.GetCell(SystemeConstants.PrivilegesOfProfilesOnTablesProfileColumnName).data.Equals(this.values[SystemeConstants.PrivilegesOfProfilesOnTablesProfileColumnName]) &&
                    rowEnumerator.Current.GetCell(SystemeConstants.PrivilegesOfProfilesOnTablesDatabaseNameColumnName).data.Equals(this.values[SystemeConstants.PrivilegesOfProfilesOnTablesDatabaseNameColumnName]) &&
                    rowEnumerator.Current.GetCell(SystemeConstants.PrivilegesOfProfilesOnTablesTableNameColumnName).data.Equals(this.values[SystemeConstants.PrivilegesOfProfilesOnTablesTableNameColumnName]) &&
                    rowEnumerator.Current.GetCell(SystemeConstants.PrivilegesOfProfilesOnTablesPrivilegeColumnName).data.Equals(this.values[SystemeConstants.PrivilegesOfProfilesOnTablesPrivilegeColumnName]);               
                i = i + 1;
            }
            rowEnumerator.Dispose();
            table.DestroyRow(i);
            this.SetResult(QuerysStringResultConstants.RowDeleted);
        }

        public override string GetNeededExecutePrivilege()
        {
            throw new NotImplementedException();
        }

        public void SetData(string profileName, string databaseName, string tableName, string privilegeName)
        {
            this.values.Add(SystemeConstants.PrivilegesOfProfilesOnTablesProfileColumnName, profileName);
            this.values.Add(SystemeConstants.PrivilegesOfProfilesOnTablesDatabaseNameColumnName, databaseName);
            this.values.Add(SystemeConstants.PrivilegesOfProfilesOnTablesTableNameColumnName, tableName);
            this.values.Add(SystemeConstants.PrivilegesOfProfilesOnTablesPrivilegeColumnName, privilegeName);
        }
    }
}
