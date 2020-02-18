using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Comparers
{
    public class ListComparer<ValueType> : IEqualityComparer<List<ValueType>>
    {
        private IEqualityComparer<ValueType> valueComparator;

        public ListComparer(IEqualityComparer<ValueType> internalValueComparator) 
        {
            this.valueComparator = internalValueComparator;
        }

        public bool Equals(List<ValueType> x, List<ValueType> y)
        {  
            bool b = x.Count == y.Count;
            for(int i = 0; i < x.Count && b == true; i++) 
            {
                b = this.valueComparator.Equals(x[i], y[i]);
            }
            return b;
        }

        public int GetHashCode(List<ValueType> obj)
        {
            throw new NotImplementedException();
        }
    }
}
