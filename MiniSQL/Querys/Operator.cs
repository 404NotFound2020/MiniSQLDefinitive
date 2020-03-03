using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Querys
{
    public class Operator
    {
        private Func<Tuple<IComparable, IComparable>, bool> operatorFunction;

        public static Operator higher = new Operator((tuple) => {return tuple.Item1.CompareTo(tuple.Item2) == 1;});
        public static Operator less = new Operator((tuple) => {return tuple.Item1.CompareTo(tuple.Item2) == -1;});
        public static Operator equal = new Operator((tuple) => {return tuple.Item1.CompareTo(tuple.Item2) == 0;});

        private Operator(Func<Tuple<IComparable, IComparable>, bool> operatorFunction) 
        {
            this.operatorFunction = operatorFunction;
        }

        public bool evaluate(IComparable ele1, IComparable ele2) 
        {
            return this.operatorFunction.Invoke(new Tuple<IComparable, IComparable>(ele1, ele2));
        }


    }
}
