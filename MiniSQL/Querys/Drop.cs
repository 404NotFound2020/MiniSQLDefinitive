using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Querys
{
    public class Drop : DataDefinitionQuery
    {

        public Drop(IDatabaseContainer container) : base(container)
        { 
            
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }

        public override bool ValidateParameters()
        {
            throw new NotImplementedException();
        }
    }
}
