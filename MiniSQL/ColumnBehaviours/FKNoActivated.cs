using MiniSQL.Classes;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.ColumnBehaviours
{
    public class FKNoActivated : IFK
    {

        private static FKNoActivated fKNoActivated;

        private FKNoActivated() 
        { 
        
        }

        public static FKNoActivated GetFKNoActivated() 
        { 
            if(fKNoActivated == null) 
            {
                fKNoActivated = new FKNoActivated();
            }
            return fKNoActivated;        
        }

        public void AddReference(Column column, Column referencedColumn)
        {
            
        }

        public bool CanInsert(Column column, Cell cell)
        {
            return true;
        }
    }
}
