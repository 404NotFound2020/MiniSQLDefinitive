using MiniSQL.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    public abstract class DataDefinitionQuery : AbstractQuery
    {

        public DataDefinitionQuery(IDatabaseContainer container) : base(container)
        {
            
        }

        public override void Execute() 
        {
            if (this.GetErrorCount() == 0) this.ExecuteParticularQueryAction();
        }

        public override bool ValidateParameters()
        {
            if (!this.GetContainer().ExistDatabase(this.targetDatabase)) this.SaveTheError(QuerysStringResultConstants.DatabaseDoesntExist(this.targetDatabase));
            else if (!this.GetPrivilegeModule().CheckProfileDatabasePrivileges(this.username, this.targetDatabase, this.GetNeededExecutePrivilege())) this.SaveTheError("Not enougth parameters");
            else this.ValidateParameters(this.GetContainer().GetDatabase(this.targetDatabase));
            return this.GetIsValidQuery();
        }


        protected abstract void ValidateParameters(IDatabase database);
        public abstract void ExecuteParticularQueryAction();
    }
}
