using MiniSQL.Classes;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Querys
{
    public class Update : DataManipulationQuery
    {
        private Dictionary<string, string> updateColumnData;

        public Update(IDatabaseContainer container) : base(container)
        {
            this.updateColumnData = new Dictionary<string, string>();
        }

        public override void ExecuteParticularQueryAction(Table table)
        {
            throw new NotImplementedException();
        }

        public void AddUpdateColumnData(string columnName, string newValue) 
        { 
        
        }

        protected override void ValidateParameters(Table table)
        {
            throw new NotImplementedException();
        }
    }
}
