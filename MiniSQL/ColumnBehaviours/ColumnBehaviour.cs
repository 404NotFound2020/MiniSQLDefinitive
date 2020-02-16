using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.ColumnBehaviours
{
    public class ColumnBehaviour
    {
        public IPK primaryKeyManager;
        public IFK foreignKeyManager;

        public ColumnBehaviour(IPK primaryKeyManager, IFK foreignKeyManager) 
        {
            this.primaryKeyManager = primaryKeyManager;
            this.foreignKeyManager = foreignKeyManager;
        }


    }
}
