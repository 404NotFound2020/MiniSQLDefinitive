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

        public override bool ValidatePrivileges(ISystemePrivilegeModule privilegeModule)
        {
            if (!privilegeModule.CheckIsAutorizedToExecuteSecurityQuery(this.username)) this.SaveTheError(QuerysStringResultConstants.NotSpecialProfileUserToExecuteSecurityQuery);
            return this.GetIsValidQuery();
        }

        public override void Execute()
        {
            if (this.GetErrorCount() == 0) this.ExecuteParticularQueryAction();
        }

        public abstract void ExecuteParticularQueryAction();

    }
}
