using MiniSQL.Classes;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Querys
{
    public class Insert : DataManipulationQuery
    {
        private Dictionary<string, string> columnsNameAndDataValues;

        public Insert(IDatabaseContainer container) : base(container)
        {
            this.columnsNameAndDataValues = new Dictionary<string, string>();            
        }

        public override void ExecuteParticularQueryAction(Table table)
        {
            throw new NotImplementedException();
        }

        protected override void ValidateParameters(Table table)
        {
            throw new NotImplementedException();
        }

        public void AddValue(string columnName, string cellValue) 
        { 
        
        }
    }
}
