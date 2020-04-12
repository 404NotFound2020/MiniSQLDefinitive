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
            return true;
        }

    }
}
