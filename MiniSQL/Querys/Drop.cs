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
            this.GetContainer().GetDatabase(this.targetDatabaseName).DropTable(this.targetTableName);
        }

        public override bool ValidateParameters()
        {
            if (this.GetContainer().ExistDatabase(this.targetDatabaseName))
            {
                if (this.GetContainer().GetDatabase(this.targetDatabaseName).ExistTable(this.targetTableName))
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
