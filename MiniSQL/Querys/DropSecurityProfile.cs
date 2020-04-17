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
            else if(!column.GetCells(this.targetSecurityProfile)[0].row.CheckIfRowCouldBeChanged()) this.SaveTheError("This profile cannot remove, check the fks");
            return this.GetIsValidQuery();  
        }

        public override void ExecuteParticularQueryAction()
        {
            bool b = false;
            ITable table = this.GetContainer().GetDatabase(this.targetDatabase).GetTable(this.targetTableName);
            IEnumerator<Row> rowEnumerator = table.GetRowEnumerator();
            int i = -1;
            while(rowEnumerator.MoveNext() && !b)
            {
                b = rowEnumerator.Current.GetCell(SystemeConstants.ProfileNameColumn).data.Equals(this.targetSecurityProfile);
                i = i + 1;
            }
            table.DestroyRow(i);
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
