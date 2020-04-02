using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    public interface ITableRestriction
    {
        bool Evaluate(List<string> values);
        bool Evaluate(IEnumerable<string> columnNames, List<string> values);

    }
}
