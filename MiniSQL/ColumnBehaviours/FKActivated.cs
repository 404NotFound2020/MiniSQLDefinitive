using MiniSQL.Classes;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.ColumnBehaviours
{
    public class FKActivated : IFK
    {
        public void AddReference(Column column, Column referencedColumn)
        {
            throw new NotImplementedException();
        }

        public bool CanInsert(Column column, Cell cell)
        {
            throw new NotImplementedException();
        }
    }
}
