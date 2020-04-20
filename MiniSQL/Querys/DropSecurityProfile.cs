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
    public class DropSecurityProfile : PrivilegeManipulationQuery
    {
        private string targetSecurityProfile;
        public DropSecurityProfile(IDatabaseContainer container) : base(container)
        {
        }

        public override bool ValidateParameters()
        {
            Column column = this.GetContainer().GetDatabase(this.targetDatabase).GetTable(this.targetTableName).GetColumn(SystemeConstants.ProfileNameColumn);
            if (!column.ExistCells(this.targetSecurityProfile)) this.SaveTheError("The security profile doenst exist");
            else if(this.GetContainer().GetDatabase(SystemeConstants.SystemDatabaseName).GetTable(SystemeConstants.NoRemovableProfilesTableName).GetColumn(SystemeConstants.ProfileNameColumn).ExistCells(this.targetSecurityProfile)) this.SaveTheError("This profile is not removable");
            return this.GetIsValidQuery();  
        }

        public override void ExecuteParticularQueryAction()
        {
            this.DeleteProfileDatabasePrivileges();
            this.DeleteProfileTablePrivileges();
            this.DeleteUsersWithThisProfile();
            this.DeleteSecurityProfile();
        }

        private void DeleteSecurityProfile()
        {
            bool b = false;
            ITable table = this.GetContainer().GetDatabase(this.targetDatabase).GetTable(this.targetTableName);
            IEnumerator<Row> rowEnumerator = table.GetRowEnumerator();
            int i = -1;
            while (rowEnumerator.MoveNext() && !b)
            {
                b = rowEnumerator.Current.GetCell(SystemeConstants.ProfileNameColumn).data.Equals(this.targetSecurityProfile);
                i = i + 1;
            }
            table.DestroyRow(i);
        }

        private void DeleteProfileTablePrivileges()
        {
            Delete delete = new Delete(this.GetContainer());
            delete.targetDatabase = SystemeConstants.SystemDatabaseName;
            delete.targetTableName = SystemeConstants.PrivilegesOfProfilesOnTablesTableName;
            delete.whereClause.AddCritery(SystemeConstants.PrivilegesOfProfilesOnTablesProfileColumnName, this.targetSecurityProfile, Operator.equal);
            delete.Execute();
        }

        private void DeleteProfileDatabasePrivileges()
        {
            Delete delete = new Delete(this.GetContainer());
            delete.targetDatabase = SystemeConstants.SystemDatabaseName;
            delete.targetTableName = SystemeConstants.PrivilegesOfProfilesOnDatabasesTableName;
            delete.whereClause.AddCritery(SystemeConstants.PrivilegesOfProfilesOnDatabasesProfileColumnName, this.targetSecurityProfile, Operator.equal);
            delete.Execute();
        }

        private void DeleteUsersWithThisProfile()
        {
            Delete delete = new Delete(this.GetContainer());
            delete.targetDatabase = SystemeConstants.SystemDatabaseName;
            delete.targetTableName = SystemeConstants.UsersTableName;
            delete.whereClause.AddCritery(SystemeConstants.UsersProfileColumnName, this.targetSecurityProfile, Operator.equal);
            delete.Execute();
        }

        public override string GetNeededExecutePrivilege()
        {
            throw new NotImplementedException();
        }

        public void SetTargetSecurityProfile(string securityProfile)
        {
            this.targetSecurityProfile = securityProfile;
        }

    }
}
