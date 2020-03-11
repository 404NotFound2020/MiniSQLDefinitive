using MiniSQL.Classes;
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
        private List<string> selectedColumnNames;
        private List<Row> selectedRows;

        public Delete(IDatabaseContainer container) : base(container) 
        {
            this.selectedColumnNames = new List<string>();
            this.selectedRows = new List<Row>();
        }

        public override void ExecuteParticularQueryAction(Table table)
        {
            
        }

        protected override void ValidateParameters(Table table)
        {
           
        }
    }
} 
