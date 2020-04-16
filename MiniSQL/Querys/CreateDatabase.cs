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
    public class CreateDatabase : DataDefinitionQuery
    {
        public CreateDatabase(IDatabaseContainer container) : base(container)
        {

        }   

        public override void ExecuteParticularQueryAction()
        {            
            this.GetContainer().AddDatabase(new Database(this.targetDatabase));
            this.SetResult(QuerysStringResultConstants.TheDatabaseWasCreated);
        }

        public override string GetNeededExecutePrivilege()
        {
            throw new NotImplementedException();
        }

        public override bool ValidateParameters()
        {   
            if (this.GetContainer().ExistDatabase(this.targetDatabase)) this.SaveTheError(QuerysStringResultConstants.TheDatabaseExist(this.targetDatabase));                
            return this.GetIsValidQuery();
        }
    }
}