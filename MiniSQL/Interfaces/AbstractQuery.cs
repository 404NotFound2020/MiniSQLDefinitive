using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    public abstract class AbstractQuery
    {
        private string result;              
        public string targetDatabase;        
        public string targetTableName;
        private int errorCount;
        private IDatabaseContainer container;

        public AbstractQuery(IDatabaseContainer container) 
        {
            this.container = container;
            this.errorCount = 0;
        }

        public string GetResult() 
        {
            return result;    
        }

        protected void SetResult(string result) 
        {
            if (this.result == null) this.result = result;
            else this.result = this.result + "\n" + result;
        }
        
        protected void SaveTheError(string error) 
        {
            this.SetResult(error);
            this.errorCount = this.errorCount + 1;
        }

        protected bool GetIsValidQuery() 
        {
            return this.errorCount == 0;
        }

        protected IDatabaseContainer GetContainer() 
        {
            return this.container;
        }

        protected void InicializateQueryState() 
        {
            this.errorCount = 0;
            this.result = null;
        }

        public int GetErrorCount() 
        {
            return errorCount;
        }

        public abstract bool ValidateParameters();
        public abstract bool ValidatePrivileges(ISystemePrivilegeModule privilegeModule);
        public abstract string GetNeededExecutePrivilege();
        //public abstract bool CheckPrivileges();
        public abstract void Execute();

 
    }
}
