using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    public abstract class DataDefinitionQuery : AbstractQuery
    {
        public string tableName;
        public string databaseName;
        public IDatabaseContainer container;
        public DataDefinitionQuery(IDatabaseContainer container) : base(container)
        {
            
        }


    }
}
