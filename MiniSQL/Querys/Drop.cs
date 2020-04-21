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
            IDatabase database = this.GetContainer().GetDatabase(this.targetDatabase);
            ITable table = database.GetTable(this.targetTableName);
            database.DropTable(table.tableName);
            this.SetResult(QuerysStringResultConstants.TableSucesfullyDeleted(table.tableName));
        }

        public override bool ValidateParameters()
        {
            if (!this.GetContainer().ExistDatabase(this.targetDatabase)) this.SaveTheError(QuerysStringResultConstants.DatabaseDoesntExist(this.targetDatabase));
            else if (!this.GetContainer().GetDatabase(this.targetDatabase).ExistTable(this.targetTableName)) this.SaveTheError(QuerysStringResultConstants.TableDoensExist(this.targetDatabase, this.targetTableName));
            else if (!this.GetContainer().GetDatabase(this.targetDatabase).GetTable(this.targetTableName).IsDropable()) this.SaveTheError("Cannot drop, " + QuerysStringResultConstants.ForeignKeyError);
            return this.GetIsValidQuery();
        }

        public override string GetNeededExecutePrivilege()
        {
            return SystemeConstants.DropPrivilegeName;
        }

    }
}
