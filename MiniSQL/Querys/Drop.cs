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
            this.container.GetDatabase(this.databaseName).DropTable(this.tableName);
        }

        public override bool ValidateParameters()
        {
            if (this.container.ExistDatabase(this.databaseName))
            {
                if (this.container.GetDatabase(databaseName).ExistTable(tableName))
                {
                    return true;
                }
                else
                {
                    this.IncrementErrorCount();
                    return false;
                }
            }
            else
            {
                this.IncrementErrorCount();
                return false;
            }
        }

    }
}
