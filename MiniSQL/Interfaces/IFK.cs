using MiniSQL.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    public interface IFK
    {
        void AddReference(Column column, Column referencedColumn);
        bool CanInsert(Column column, Cell cell);

    }
}
