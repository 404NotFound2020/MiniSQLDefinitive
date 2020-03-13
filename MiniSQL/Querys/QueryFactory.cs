using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Querys
{
    public class QueryFactory
    {
        private static QueryFactory queryFactory;

        private QueryFactory() 
        { 
        
        }

        public static QueryFactory GetQueryFactory() 
        {
            if (queryFactory == null) queryFactory = new QueryFactory();
            return queryFactory;
        }


        public AbstractQuery GetQuery(string xmlRequest) 
        {
            return null;
        }


    }
}
