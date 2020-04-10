using MiniSQL.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    public interface ISysteme
    {
        void SaveTable(IDatabase database, ITable table);
        void RemoveTable(IDatabase database, ITable table);
        void SaveData(IDatabase database);
        void SaveData();

    }
}
