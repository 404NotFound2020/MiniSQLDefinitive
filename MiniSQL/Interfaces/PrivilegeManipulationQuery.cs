using MiniSQL.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    public abstract class PrivilegeManipulationQuery : AbstractQuery
    {
        public PrivilegeManipulationQuery(IDatabaseContainer container) : base(container)
        {

        }
        public override bool ValidateParameters()
        {
            if (!this.GetPrivilegeModule().CheckIsAutorizedToExecuteSecurityQuery(this.username)) this.SaveTheError(QuerysStringResultConstants.NotSpecialProfileUserToExecuteSecurityQuery);
            else this.Validate();
            return this.GetIsValidQuery();
        }

        protected abstract void Validate();

        public override void Execute()
        {
            if (this.GetErrorCount() == 0) this.ExecuteParticularQueryAction();
        }

        public abstract void ExecuteParticularQueryAction();

    }
}
