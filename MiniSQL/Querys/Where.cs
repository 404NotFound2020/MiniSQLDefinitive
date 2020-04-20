using MiniSQL.Classes;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Querys
{
    public class Where
    {
        //the column name and filter value pair
        private IList<Tuple<string,string>> filterCriteries;
        private IList<Operator> operators;

        public Where() 
        {
            this.filterCriteries = new List<Tuple<string, string>>();
            this.operators = new List<Operator>();
        }

        public void AddCritery(string colName, string value, Operator op)
        {
            this.filterCriteries.Add(new Tuple<string, string>(colName, value));
            this.operators.Add(op);
        }

        public IEnumerator<Tuple<string, string>> GetCriteriesEnumerator() 
        {
            return this.filterCriteries.GetEnumerator();
        }

        public bool IsSelected(Row row) 
        {
            IEnumerator<Tuple<string, string>> criteryEnumerator = this.filterCriteries.GetEnumerator();
            IEnumerator<Operator> operatorEnumerator = this.operators.GetEnumerator();
            bool b = true;
            Cell cell;
            while(criteryEnumerator.MoveNext() && operatorEnumerator.MoveNext() && b) 
            {
                cell = row.GetCell(criteryEnumerator.Current.Item1);
                b = cell.column.dataType.Evaluate(operatorEnumerator.Current, cell.data, criteryEnumerator.Current.Item2); //AND MODE (AT THE MOMENT IT DOENST MATTER, BECAUSE WE WILL EXPECT ONLY A OPERATOR)                   
            }                            
            return b;
        }

    }
}
