using MiniSQL.Classes;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Querys
{
    public class Delete : DataManipulationQuery
    {
        
        public Delete(IDatabaseContainer container) : base(container) 
        { 
        
        }

        public override void ExecuteParticularQueryAction(Table table)
        {
            throw new NotImplementedException();
        }

        protected override void ValidateParameters(Table table)
        {
            throw new NotImplementedException();
        }
    }
}
