using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniSQL.Constants;
using MiniSQL.Classes;


namespace MiniSQL.Querys
{
    public class CreateSecurityProfile : PrivilegeManipulationQuery
    {
        private string profileName;

        public CreateSecurityProfile(IDatabaseContainer container) : base(container)
        {

        }

        public override string GetNeededExecutePrivilege()
        {
            return SystemeConstants.CreateUserPrivilege;
        }

        protected override void Validate()
        {
            Dictionary<string, string> columnValuesPair = new Dictionary<string, string>();
            columnValuesPair.Add(SystemeConstants.ProfileNameColumn, this.profileName);
            if (!this.GetContainer().GetDatabase(this.targetDatabase).GetTable(this.targetTableName).primaryKey.Evaluate(columnValuesPair)) this.SaveTheError("The profile exist");
        }

        public void SetProfileName(string profileName) {
            this.profileName = profileName;
        }

        public override void ExecuteParticularQueryAction()
        {
            ITable table = this.GetContainer().GetDatabase(this.targetDatabase).GetTable(this.targetTableName);
            Row row = table.CreateRowDefinition();
            row.GetCell(SystemeConstants.ProfileNameColumn).data = this.profileName;
            table.AddRow(row);
            this.SetResult(QuerysStringResultConstants.SecurityProfileCreated);
        }
    }
}
