using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Comparers
{
    public class DupleComparer<TypeOne, TypeTwo> : IEqualityComparer<Tuple<TypeOne, TypeTwo>>
    {
        private IEqualityComparer<TypeOne> equalityComparerOne;
        private IEqualityComparer<TypeTwo> equalityComparerTwo;

        public DupleComparer(IEqualityComparer<TypeOne> equalityComparerOne, IEqualityComparer<TypeTwo> equalityComparerTwo)
        {
            this.equalityComparerOne = equalityComparerOne;
            this.equalityComparerTwo = equalityComparerTwo;
        }

        public bool Equals(Tuple<TypeOne, TypeTwo> x, Tuple<TypeOne, TypeTwo> y)
        {
            return this.equalityComparerOne.Equals(x.Item1, y.Item1) && this.equalityComparerTwo.Equals(x.Item2, y.Item2);
        }

        public int GetHashCode(Tuple<TypeOne, TypeTwo> obj)
        {
            throw new NotImplementedException();
        }
    }
}
