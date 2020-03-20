using MiniSQL.Constants;
using MiniSQL.Initializer;
using MiniSQL.Interfaces;
using MiniSQL.ServerFacade;
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
        private IDatabaseContainer databaseContainer;

        private QueryFactory() 
        { 
        
        }

        public static QueryFactory GetQueryFactory() 
        {
            if (queryFactory == null) queryFactory = new QueryFactory();
            return queryFactory;
        }

        public AbstractQuery GetQuery(Request request) 
        {
            IDatabaseContainer system = this.databaseContainer;
            AbstractQuery query = null;
            switch (request.GetElementsContentByTagName(RequestAndRegexConstants.queryTagName)[0]) 
            {
                case RequestAndRegexConstants.selectQueryIdentificator:
                    query = this.CreateSelectQuery(request, system);
                break;
                case RequestAndRegexConstants.insertQueryIdentificator:
                    query = this.CreateInsertQuery(request, system);
                break;
                case RequestAndRegexConstants.updateQueryIdentificator:
                    query = this.CreateUpdateQuery(request, system);
                break;
                case RequestAndRegexConstants.deleteQueryIdentificator:
                    query = this.CreateDeleteQuery(request, system);
                break;
                case RequestAndRegexConstants.dropTableQueryIdentificator:
                    query = this.CreateDropTableQuery(request, system);
                break;
                case RequestAndRegexConstants.createTableQueryIdentificator:
                    query = this.CreateCreateTableQuery(request, system);
                break;
            }
            return query;
        }

        private Select CreateSelectQuery(Request request, IDatabaseContainer container) 
        {
            Select select = new Select(container);
            this.SetDatabaseAndTableTarget(request, select);
            string[] selectedColumns = request.GetElementsContentByTagName(RequestAndRegexConstants.selectedColumnTagName);
            for(int i = 0; i < selectedColumns.Length; i++) 
            {
                select.AddSelectedColumnName(selectedColumns[i]);
            }
            select.whereClause = this.CreateWhereClause(request);
            return select;
        }

        private Insert CreateInsertQuery(Request request, IDatabaseContainer container)
        {
            Insert insert = new Insert(container);
            this.SetDatabaseAndTableTarget(request, insert);
            string[] values = request.GetElementsContentByTagName(RequestAndRegexConstants.valueTagName);
            for(int i = 0; i < values.Length; i++) 
            {
                insert.AddValue(values[i]);
            } 
            return insert;
        }

        private Update CreateUpdateQuery(Request request, IDatabaseContainer container)
        {
            Update update = new Update(container);
            this.SetDatabaseAndTableTarget(request, update);
            string[] toUpdatedColumns = request.GetElementsContentByTagName(RequestAndRegexConstants.updatedColumnTagName);
            string[] values = request.GetElementsContentByTagName(RequestAndRegexConstants.updatedValueTagName);
            for(int i = 0; i < toUpdatedColumns.Length; i++) 
            {
                update.AddValue(toUpdatedColumns[i], values[i]);
            }
            update.whereClause = this.CreateWhereClause(request);
            return update;
        }

        private Delete CreateDeleteQuery(Request request, IDatabaseContainer container)
        {
            Delete delete = new Delete(container);
            this.SetDatabaseAndTableTarget(request, delete);
            delete.whereClause = this.CreateWhereClause(request);
            return delete;
        }

        private Drop CreateDropTableQuery(Request request, IDatabaseContainer container)
        {
            Drop drop = new Drop(container);
            this.SetDatabaseAndTableTarget(request, drop);
            return drop;
        }

        private Create CreateCreateTableQuery(Request request, IDatabaseContainer container)
        {
            Create create = new Create(container);
            this.SetDatabaseAndTableTarget(request, create);
            string[] columnsNames = request.GetElementsContentByTagName(RequestAndRegexConstants.columnTagName);
            string[] columnsTypes = request.GetElementsContentByTagName(RequestAndRegexConstants.columnTypeTagName);
            for(int i = 0; i < columnsNames.Length; i++) 
            {
                create.AddColumn(columnsNames[i], columnsTypes[i]);
            }
            return create;
        }

        private Where CreateWhereClause(Request request) 
        {
            Where where = new Where();
            string[] columnToEvaluate = request.GetElementsContentByTagName(RequestAndRegexConstants.toEvaluateColumnTagName);
            string[] evaluationValue = request.GetElementsContentByTagName(RequestAndRegexConstants.evalValueTagName);
            string[] operators = request.GetElementsContentByTagName(RequestAndRegexConstants.operatorTagName);
            OperatorFactory operatorFactory = OperatorFactory.GetOperatorFactory();
            for(int i = 0; i < columnToEvaluate.Length; i++) 
            {
                where.AddCritery(new Tuple<string, string>(columnToEvaluate[i], evaluationValue[i]), operatorFactory.GetOperator(operators[i]));
            }
            return where;
        }

        private void SetDatabaseAndTableTarget(Request request, AbstractQuery query) {
            query.targetDatabase = this.databaseContainer.GetDefaultDatabaseName();
            string[] databaseGroups = request.GetElementsContentByTagName(RequestAndRegexConstants.databaseTagName);            
            if(!(databaseGroups.Length == 0)) query.targetDatabase = databaseGroups[0];
            query.targetTableName = request.GetElementsContentByTagName(RequestAndRegexConstants.tableTagName)[0];
        }

        public void SetContainer(IDatabaseContainer container) 
        {
            this.databaseContainer = container;
        }

    }
}
