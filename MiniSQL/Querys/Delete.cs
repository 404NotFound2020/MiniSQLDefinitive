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
    public class Delete : DataManipulationQuery
    {

        public Delete(IDatabaseContainer container) : base(container) 
        {

        }

        public override void ExecuteParticularQueryAction(ITable table)
        {
            Select select = new Select(this.GetContainer());
            select.whereClause = whereClause;
            select.ExecuteParticularQueryAction(table);
            IEnumerator<int> enumerator = select.GetSelectedRowsIndex();
            IEnumerator<Row> afectedRowsEnumerator = select.GetAfectedRowEnum();
            int i = 0;
            int h = 0;
            while (enumerator.MoveNext() && afectedRowsEnumerator.MoveNext())
            {
                if (!afectedRowsEnumerator.Current.CheckIfRowCouldBeChanged()) h = h + 1;
                else
                {                   
                    this.AddAfectedRow(afectedRowsEnumerator.Current);
                    table.DestroyRow(enumerator.Current - i);
                    i++;
                }
                
            }
            this.SetResult(QuerysStringResultConstants.DeletedRow(i) + "\n" + QuerysStringResultConstants.NumbersRowsThatCannotDeleteBecauseOfForeignKey(h));
        }

        protected override void ValidateParameters(ITable table)
        {
           
        }

        public override string GetNeededExecutePrivilege()
        {
            throw new NotImplementedException();
        }

    }
} 
