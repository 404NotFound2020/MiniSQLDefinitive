using MiniSQL.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    public interface IDatabaseContainer
    {

        Database GetDatabase(string databaseName);
        bool ExistDatabase(string databaseName);









    }
}
