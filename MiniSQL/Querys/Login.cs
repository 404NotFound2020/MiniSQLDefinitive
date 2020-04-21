using MiniSQL.Constants;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Querys
{
    public class Login : UserManipulationQuery
    {
        private string targetUsername;
        private string password;

        public Login(IDatabaseContainer container, IUserThread userThread) : base(container, userThread)
        {
        }

        public override bool ValidateParameters()
        {
            ITable table = this.GetContainer().GetDatabase(this.targetDatabase).GetTable(this.targetTableName);
            if (!(table.GetColumn(SystemeConstants.UsersNameColumnName).ExistCells(this.targetUsername) && table.GetColumn(SystemeConstants.UsersPasswordColumnName).ExistCells(this.password))) this.SaveTheError("Invalid login");
            return GetIsValidQuery();
        }

        public override void ExecuteParticularQueryAction()
        {
            this.userThread.username = this.targetUsername;
            this.SetResult(QuerysStringResultConstants.UserLogged);
            this.SetResult(this.username);
        }

        public override string GetNeededExecutePrivilege()
        {
            throw new NotImplementedException();
        }

        public void SetData(string username, string password)
        {
            this.targetUsername = username;
            this.password = password;
        }



    }
}
