using MiniSQL.Classes;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.ColumnBehaviours
{
    public class PKActivated : IPK
    {

        private static PKActivated pkActivated;

        private PKActivated() 
        { 
        
        }

        public static PKActivated GetPKActivated() 
        {
            if (pkActivated == null) 
            {
                pkActivated = new PKActivated();
            }
            return pkActivated;        
        }

        public bool CanInsert(Column column, Cell cell)
        {
            throw new NotImplementedException();
        }
    }
}
