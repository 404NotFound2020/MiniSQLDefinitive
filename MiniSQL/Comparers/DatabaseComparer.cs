using MiniSQL.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Comparers
{
    public class DatabaseComparer : IEqualityComparer<Database>
    {
        
        public bool Equals(Database x, Database y)
        {
            if (!x.databaseName.Equals(y.databaseName))
                return false;
            if (!(x.user.Equals(y.user) && x.password.Equals(y.password)))
                return false;
            return new DictionaryComparer<string, Table>(new TableComparer()).Equals(x.ReadTables(), y.ReadTables());
        }

        public int GetHashCode(Database obj)
        {
            throw new NotImplementedException();
        }
    }
}
