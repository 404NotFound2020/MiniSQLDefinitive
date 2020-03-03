using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Querys
{
    public class Create : DataDefinitionQuery
    {
        //Column name, datatype
        private Dictionary<string, string> columnsAndTypes;

        public Create(IDatabaseContainer container) : base(container)
        {
            this.columnsAndTypes = new Dictionary<string, string>();
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }

        public override bool ValidateParameters()
        {
            throw new NotImplementedException();
        }

        public void AddColumn(string columnName, string dataType) 
        { 
        
        }
    }
}
