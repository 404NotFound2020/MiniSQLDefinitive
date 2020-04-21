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

        public override bool ValidatePrivileges(ISystemePrivilegeModule privilegeModule)
        {
            return privilegeModule.CheckProfileDatabasePrivileges(this.username, this.targetDatabase, this.GetNeededExecutePrivilege());
        }

        public abstract void ExecuteParticularQueryAction();
    }
}
