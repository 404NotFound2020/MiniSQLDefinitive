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
        private IDatabaseContainer container;
        public string targetDatabase;        
        public string targetTableName;
        private int errorCount;

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
               
        protected bool GetIsValidQuery() 
        {
            return this.errorCount == 0;
        }

        protected IDatabaseContainer GetContainer() 
        {
            return this.container;
        }

        protected void IncrementErrorCount() 
        {
            this.errorCount = this.errorCount + 1;
        }

        protected void InicializateErrorCount() 
        {
            this.errorCount = 0;
        }

        public int GetErrorCount() 
        {
            return errorCount;
        }

        public abstract bool ValidateParameters(); 
        public abstract void Execute();

    }
}
