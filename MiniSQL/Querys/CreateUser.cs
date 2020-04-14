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
    public class CreateUser : PrivilegeManipulationQuery
    {

        private string newUsername;
        private string password;

        public CreateUser(IDatabaseContainer container) : base(container)
        {
        }

        public override void ExecuteParticularQueryAction()
        {
            ITable table = this.GetContainer().GetDatabase(this.targetDatabase).GetTable(this.targetTableName);
            Row row = table.CreateRowDefinition();
            row.GetCell(SystemeConstants.UsersNameColumnName).data = this.username;
            row.GetCell(SystemeConstants.UsersPasswordColumnName).data = this.password;
            table.AddRow(row);
            this.SetResult("Created user");
        }

        public override string GetNeededExecutePrivilege()
        {
            return SystemeConstants.CreateUserPrivilege;
        }

        public override bool ValidateParameters()
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add(SystemeConstants.UsersNameColumnName, this.username);
            if (!this.GetContainer().GetDatabase(this.targetDatabase).GetTable(this.targetTableName).primaryKey.Evaluate(values))
            {
                this.SaveTheError("The user exist");
            }
            return this.GetIsValidQuery();
        }

        public void SetUser(string newUsername, string password)
        {
            this.newUsername = newUsername;
            this.password = password;
        }
    }

}
