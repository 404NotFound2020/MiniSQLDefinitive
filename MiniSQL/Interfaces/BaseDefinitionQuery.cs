using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    public abstract class BaseDefinitionQuery : AbstractQuery
    {
        public BaseDefinitionQuery(IDatabaseContainer container) : base(container)
        {
        }

        public override void Execute()
        {
            if (this.GetErrorCount() == 0) this.ExecuteParticularQueryAction();
        }

        public override string GetNeededExecutePrivilege()
        {
            throw new NotImplementedException();
        }

        public abstract void ExecuteParticularQueryAction();
    }
}
