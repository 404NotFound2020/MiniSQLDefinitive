using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Querys
{
    public class GrantPrivilege : PrivilegeManipulationQuery
    {
        private string databaseName;
        private string tableName;
        private string privilegeName;
        private string profileName;

        public GrantPrivilege(IDatabaseContainer container) : base(container)
        {
           
        }

    public override void ExecuteParticularQueryAction()
        {
            throw new NotImplementedException();
        }

        public override string GetNeededExecutePrivilege()
        {
            throw new NotImplementedException();
        }

        public override bool ValidateParameters()
        {
            throw new NotImplementedException();
        }

        public void SetData()
        {
                    private string databaseName;
        private string tableName;
        private string privilegeName;
        private string profileName;

    }
    }


}
