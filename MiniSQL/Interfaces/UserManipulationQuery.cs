using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    public abstract class UserManipulationQuery : AbstractQuery
    {
        protected IUserThread userThread { get; }
        public UserManipulationQuery(IDatabaseContainer container, IUserThread userThread) : base(container)
        {
            this.userThread = userThread;
        }

        public override void Execute()
        {
            if (this.GetErrorCount() == 0) this.ExecuteParticularQueryAction();
        }

        public abstract void ExecuteParticularQueryAction();

    }
}
