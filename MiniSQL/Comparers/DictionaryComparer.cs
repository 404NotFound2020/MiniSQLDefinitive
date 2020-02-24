using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Comparers
{
    public class DictionaryComparer<KeyTipe, ValueType> : IEqualityComparer<IDictionary<KeyTipe, ValueType>>
    {
        private IEqualityComparer<ValueType> valueComparator;

        public DictionaryComparer(IEqualityComparer<ValueType> internalValueComparator) 
        {
            this.valueComparator = internalValueComparator;
        }
       
        public bool Equals(IDictionary<KeyTipe, ValueType> x, IDictionary<KeyTipe, ValueType> y)
        {
            if (x.Count != y.Count)
                return false;
            if (x.Keys.Except(y.Keys).Any())
                return false;
            if (y.Keys.Except(x.Keys).Any())
                return false;
            foreach (var par in x)
                if (!valueComparator.Equals(par.Value, y[par.Key]))
                    return false;
            return true;
        }

        public int GetHashCode(IDictionary<KeyTipe, ValueType> obj)
        {
            throw new NotImplementedException();
        }
    }
}
