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

        public override void ExecuteParticularQueryAction()
        {
            this.GetContainer().GetDatabase(this.targetDatabase).DropTable(this.targetTableName);
        }

        public override bool ValidateParameters()
        {
            if (this.GetContainer().ExistDatabase(this.targetDatabase))
            {
                if (this.GetContainer().GetDatabase(this.targetDatabase).ExistTable(this.targetTableName))
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
