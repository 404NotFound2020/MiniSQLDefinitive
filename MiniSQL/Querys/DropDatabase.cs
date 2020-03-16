using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Querys
{
    public class DropDatabase : AbstractQuery
    {
        public DropDatabase(IDatabaseContainer container) : base(container)
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
