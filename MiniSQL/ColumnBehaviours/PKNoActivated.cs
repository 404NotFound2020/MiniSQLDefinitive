using MiniSQL.Classes;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.ColumnBehaviours
{
    class PKNoActivated : IPK
    {

        private static PKNoActivated pkNoActivated;

        private PKNoActivated()
        {

        }

        public static PKNoActivated GetPKNoActivated()
        {
            if (pkNoActivated == null)
            {
                pkNoActivated = new PKNoActivated();
            }
            return pkNoActivated;
        }


        public bool CanInsert(Column column, Cell cell)
        {
            throw new NotImplementedException();
        }
    }
}
