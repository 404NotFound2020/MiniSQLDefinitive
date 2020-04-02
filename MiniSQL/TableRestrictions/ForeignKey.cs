using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.TableRestrictions
{
    public class ForeignKey : ITableRestriction
    {

        public bool Evaluate(List<string> values)
        {
            throw new NotImplementedException();
        }

        public bool Evaluate(IEnumerable<string> columnNames, List<string> values)
        {
            throw new NotImplementedException();
        }



    }
}
