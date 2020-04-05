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

        public override void ExecuteParticularQueryAction(Table table)
        {
            Select select = new Select(this.GetContainer());
            select.targetDatabase = this.targetDatabase;
            select.targetTableName = this.targetTableName;
            select.selectedAllColumns = true;
            select.whereClause = whereClause;
            select.ValidateParameters();
            select.ExecuteParticularQueryAction(table);
            IEnumerator<int> enumerator = select.GetSelectedRowsIndex();
            IEnumerator<Row> afectedRowsEnumerator = select.GetAfectedRowEnum();
            int i = 0;
            int h = 0;
            while (enumerator.MoveNext() && afectedRowsEnumerator.MoveNext())
            {
                if (!afectedRowsEnumerator.Current.CheckIfRowCouldBeDeleted()) h = h + 1;
                else
                {                   
                    this.AddAfectedRow(afectedRowsEnumerator.Current);
                    table.DestroyRow(enumerator.Current - i);
                    i++;
                }
                
            }
            this.SetResult(QuerysStringResultConstants.DeletedRow(i) + "\n" + QuerysStringResultConstants.NumbersRowsThatCannotDeleteBecauseOfForeignKey(h));
        }

        protected override void ValidateParameters(Table table)
        {
           
        }
    }
} 
