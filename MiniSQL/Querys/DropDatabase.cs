﻿using MiniSQL.Constants;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Querys
{
    public class DropDatabase : BaseDefinitionQuery
    {
        public DropDatabase(IDatabaseContainer container) : base(container)
        {  

        }

        public override void ExecuteParticularQueryAction()
        {
            this.GetContainer().RemoveDatabase(this.targetDatabase);
            this.SetResult(QuerysStringResultConstants.TheDatabaseWasDeleted);
        }

        public override bool ValidateParameters()
        {
            bool b;
            if (b = !this.GetContainer().ExistDatabase(this.targetDatabase)) this.SaveTheError(QuerysStringResultConstants.DatabaseDoesntExist(this.targetDatabase));
            else if (!this.GetPrivilegeModule().CheckProfileDatabasePrivileges(this.username, this.targetDatabase, this.GetNeededExecutePrivilege())) this.SaveTheError("Not enougth privileges");
            return !b;
        }

        public override string GetNeededExecutePrivilege()
        {
            return SystemeConstants.DropDatabasePrivilegeName;
        }
    }
}
