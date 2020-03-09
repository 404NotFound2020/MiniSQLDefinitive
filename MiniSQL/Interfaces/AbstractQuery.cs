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
        private int errorCount;
        public string targetTableName;

        public AbstractQuery(IDatabaseContainer container) 
        {
            this.container = container;
            this.result = "";
            this.errorCount = 0;
        }

        public string GetResult() 
        {
            return result;    
        }

        protected void SetResult(string result) 
        {
            this.result = result;       
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

        public int GetErrorCount() 
        {
            return errorCount;
        }

        public abstract bool ValidateParameters(); 
        public abstract void Execute();

    }
}
