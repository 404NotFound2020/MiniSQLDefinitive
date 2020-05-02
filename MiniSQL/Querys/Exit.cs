using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Querys
{
    public class Exit : UserManipulationQuery
    {
        private ISystemeDatabaseModule databaseModule;

        public Exit(IDatabaseContainer container, IUserThread userThread, ISystemeDatabaseModule databaseModule) : base(container, userThread)
        {
            this.databaseModule = databaseModule;
        }

        public override void ExecuteParticularQueryAction()
        {
            this.databaseModule.SaveAll();
            this.SetResult("all saved");
            userThread.Close();
        }

        public override string GetNeededExecutePrivilege()
        {
            return null;
        }

        public override bool ValidateParameters()
        {
            return true;
        }
    }
}
