using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    public interface IActiveSystemModule : ISystemeModule
    {
        void ActToAdd(IDatabase database);
        void ActToAdd(IDatabase database, ITable table);
        void ActToRemove(IDatabase database);
        void ActToRemove(IDatabase database, ITable table);
        void TableModified(IDatabase database, ITable table);
    }
}
