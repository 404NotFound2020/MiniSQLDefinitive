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
        public abstract void Execute(IDatabaseContainer container);
        public string GetResult() 
        {
            return result;    
        }

        protected void SetResult(string result) 
        { 
               
        }


    }
}
