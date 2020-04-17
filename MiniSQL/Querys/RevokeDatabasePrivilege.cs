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
    public class RevokeDatabasePrivilege : PrivilegeManipulationQuery
    {
        private Dictionary<string, string> values;
        public RevokeDatabasePrivilege(IDatabaseContainer container) : base(container)
        {
            this.values = new Dictionary<string, string>();
        }

        public override bool ValidateParameters()
        {
            IDatabase database = this.GetContainer().GetDatabase(SystemeConstants.SystemDatabaseName);
            if (!database.GetTable(SystemeConstants.ProfilesTableName).GetColumn(SystemeConstants.ProfileNameColumn).ExistCells(this.values[SystemeConstants.PrivilegesOfProfilesOnDatabasesProfileColumnName])) this.SaveTheError("The profile doenst exist");
            else if (!this.GetContainer().ExistDatabase(this.values[SystemeConstants.PrivilegesOfProfilesOnDatabasesDatabaseNameColumnName])) this.SaveTheError("The database doesnt exist");
            else if (!database.GetTable(SystemeConstants.DatabasesPrivilegesTableName).GetColumn(SystemeConstants.DatabasesPrivilegesPrivilegeNameColumnName).ExistCells(this.values[SystemeConstants.PrivilegesOfProfilesOnDatabasesPrivilegeColumnName])) this.SaveTheError("The privilege doens exist");
            return this.GetIsValidQuery();
        }

        public override void ExecuteParticularQueryAction()
        {
            ITable table = this.GetContainer().GetDatabase(this.targetDatabase).GetTable(this.targetTableName);
            bool b = false;
            IEnumerator<Row> rowEnumerator = table.GetRowEnumerator();
            int i = -1;
            while (rowEnumerator.MoveNext() && !b) {
                b = (rowEnumerator.Current.GetCell(SystemeConstants.PrivilegesOfProfilesOnDatabasesProfileColumnName).data.Equals(this.values[SystemeConstants.PrivilegesOfProfilesOnDatabasesProfileColumnName]) && rowEnumerator.Current.GetCell(SystemeConstants.PrivilegesOfProfilesOnDatabasesPrivilegeColumnName).data.Equals(this.values[SystemeConstants.PrivilegesOfProfilesOnDatabasesPrivilegeColumnName]) && rowEnumerator.Current.GetCell(SystemeConstants.PrivilegesOfProfilesOnDatabasesDatabaseNameColumnName).data.Equals(this.values[SystemeConstants.PrivilegesOfProfilesOnDatabasesDatabaseNameColumnName]));
                i = i + 1;
            }
            rowEnumerator.Dispose();
            if (!b) this.SetResult(QuerysStringResultConstants.NothingDeleted);
            else
            {
                table.DestroyRow(i);
                this.SetResult(QuerysStringResultConstants.RowDeleted);
            }
        }

        public override string GetNeededExecutePrivilege()
        {
            throw new NotImplementedException();
        }

        public void SetData(string profileName, string databaseName, string privilegeName)
        {
            this.values.Add(SystemeConstants.PrivilegesOfProfilesOnDatabasesProfileColumnName, profileName);
            this.values.Add(SystemeConstants.PrivilegesOfProfilesOnDatabasesPrivilegeColumnName, privilegeName);
            this.values.Add(SystemeConstants.PrivilegesOfProfilesOnDatabasesDatabaseNameColumnName, databaseName);
        }
    }
}
