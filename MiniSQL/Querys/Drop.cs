using MiniSQL.Classes;
using MiniSQL.Constants;
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
            Database database = this.GetContainer().GetDatabase(this.targetDatabase);
            Table table = database.GetTable(this.targetTableName);
            database.DropTable(table.tableName);
            this.GetContainer().RemoveTable(database, table);
            this.SetResult(QuerysStringResultConstants.TableSucesfullyDeleted(table.tableName));
        }

        public override bool ValidateParameters()
        {
            if (!this.GetContainer().ExistDatabase(this.targetDatabase)) this.SaveTheError(QuerysStringResultConstants.DatabaseDoesntExist(this.targetDatabase));
            else if (!this.GetContainer().GetDatabase(this.targetDatabase).ExistTable(this.targetTableName)) this.SaveTheError(QuerysStringResultConstants.TableDoensExist(this.targetDatabase, this.targetTableName));
            return this.GetIsValidQuery();
        }

    }
}
