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
    public class DeleteUser : PrivilegeManipulationQuery
    {
        private string targetUsername;
        public DeleteUser(IDatabaseContainer container) : base(container)
        {
        }

        public override bool ValidateParameters()
        {
            Column column = this.GetContainer().GetDatabase(this.targetDatabase).GetTable(this.targetTableName).GetColumn(SystemeConstants.UsersNameColumnName);
            if (!column.ExistCells(this.targetUsername)) this.SaveTheError("The username doenst exist");
            else if (!column.GetCells(this.targetUsername)[0].row.CheckIfRowCouldBeChanged()) this.SaveTheError("The username can not delete, check if there any fk reference to this username");
            return this.GetIsValidQuery();
        }

        public override void ExecuteParticularQueryAction()
        {
            bool b = false;
            ITable table = this.GetContainer().GetDatabase(this.targetDatabase).GetTable(this.targetTableName);
            IEnumerator<Row> rowEnumerator = table.GetRowEnumerator();
            int i = -1;
            while (rowEnumerator.MoveNext() && !b)
            {
                b = rowEnumerator.Current.GetCell(SystemeConstants.UsersNameColumnName).data.Equals(this.targetUsername);
                i++;
            }
            rowEnumerator.Dispose();
            table.DestroyRow(i);
        }

        public override string GetNeededExecutePrivilege()
        {
            throw new NotImplementedException();
        }

        public void SetTargetUserName(string targetUsername)
        {
            this.targetUsername = targetUsername;
        }

    }
}