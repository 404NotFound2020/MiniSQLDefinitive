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
        private Dictionary<string, string> values;

        public CreateUser(IDatabaseContainer container) : base(container)
        {
            this.values = new Dictionary<string, string>();
        }

        public override void ExecuteParticularQueryAction()
        {
            ITable table = this.GetContainer().GetDatabase(this.targetDatabase).GetTable(this.targetTableName);
            Row row = table.CreateRowDefinition();
            IEnumerator<string> keyEnumerator = this.values.Keys.GetEnumerator();
            while (keyEnumerator.MoveNext()) row.GetCell(keyEnumerator.Current).data = this.values[keyEnumerator.Current];
            table.AddRow(row);
            this.SetResult(QuerysStringResultConstants.SecurityUserCreated);
        }

        public override string GetNeededExecutePrivilege()
        {
            return SystemeConstants.CreateUserPrivilege;
        }

        public override bool ValidateParameters()
        {
            ITable targetTable = this.GetContainer().GetDatabase(this.targetDatabase).GetTable(this.targetTableName);
            if (!targetTable.foreignKey.Evaluate(values)) this.SaveTheError("The profile doesnt exist");
            else if (!targetTable.primaryKey.Evaluate(values)) this.SaveTheError("The user exist");
            return this.GetIsValidQuery();
        }

        public void SetUser(string newUsername, string password, string profileName)
        {
            this.values.Add(SystemeConstants.UsersNameColumnName, newUsername);
            this.values.Add(SystemeConstants.UsersPasswordColumnName, password);
            this.values.Add(SystemeConstants.UsersProfileColumnName, profileName);
        }
    }

}
