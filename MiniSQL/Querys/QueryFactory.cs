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
        private ISysteme systeme;

        private QueryFactory()
        {

        }

        public static QueryFactory GetQueryFactory()
        {
            if (queryFactory == null) queryFactory = new QueryFactory();
            return queryFactory;
        }

        public AbstractQuery GetQuery(Request request, IUserThread thread)
        {
            IDatabaseContainer container = ((ISystemeDatabaseModule)this.systeme.GetSystemeModule(SystemeConstants.SystemeDatabaseModule)).GetDatabaseContainer();
            AbstractQuery query = null;
            switch (request.GetElementsContentByTagName(RequestAndRegexConstants.queryTagName)[0])
            {
                case RequestAndRegexConstants.selectQueryIdentificator:
                    query = this.CreateSelectQuery(request, container);
                    break;
                case RequestAndRegexConstants.createUserQueryIdentificator:
                    query = CreateUserQuery(request, container);
                    break;
                case RequestAndRegexConstants.insertQueryIdentificator:
                    query = this.CreateInsertQuery(request, container);
                    break;
                case RequestAndRegexConstants.updateQueryIdentificator:
                    query = this.CreateUpdateQuery(request, container);
                    break;
                case RequestAndRegexConstants.deleteQueryIdentificator:
                    query = this.CreateDeleteQuery(request, container);
                    break;
                case RequestAndRegexConstants.dropTableQueryIdentificator:
                    query = this.CreateDropTableQuery(request, container);
                    break;
                case RequestAndRegexConstants.createTableQueryIdentificator:
                    query = this.CreateCreateTableQuery(request, container);
                    break;
                case RequestAndRegexConstants.createDatabaseQueryIdentificator: 
                    query = this.CreateDatabaseQuery(request, container);
                    break;
                case RequestAndRegexConstants.dropDatabaseQueryIdentificator:
                    query = this.DropDatabaseQuery(request, container);
                    break;
                case RequestAndRegexConstants.createSecurityProfileQueryIdentificator:
                    query = CreateSecurityProfileQuery(request, container);
                    break;
                case RequestAndRegexConstants.deleteUserQueryIdentificator:
                    query = CreateDeleteUserQuery(request, container);
                    break;
                case RequestAndRegexConstants.grantDatabasePrivilegeQueryIdentificator:
                    query = CreateGrantDatabasePrivilege(request, container);
                    break;
                case RequestAndRegexConstants.revokeDatabasePrivilegeQueryIdentificator:
                    query = CreateRevoqueDatabasePrivilege(request, container);
                    break;
                case RequestAndRegexConstants.dropSecurityProfileQueryIdentificator:
                    query = CreateDropSecurityProfile(request, container);
                    break;
                case RequestAndRegexConstants.grantTablePrivilegeQueryIdentificator:
                    query = CreateGrantPrivilege(request, container);
                    break;
                case RequestAndRegexConstants.revokeTablePrivilegeQueryIdentificator:
                    query = CreateRevoquePrivilege(request, container);
                    break;
                case RequestAndRegexConstants.loginQueryIdentificator:
                    query = CreateLogin(request, container, thread);
                    break;
            }
            query.username = thread.username;
            return query;
        }

        private Select CreateSelectQuery(Request request, IDatabaseContainer container)
        {
            Select select = new Select(container);
            this.SetDatabaseAndTableTarget(request, select);
            string[] selectedColumns = request.GetElementsContentByTagName(RequestAndRegexConstants.selectedColumnTagName);
            for (int i = 0; i < selectedColumns.Length; i++)
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
            for (int i = 0; i < values.Length; i++)
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
            for (int i = 0; i < toUpdatedColumns.Length; i++)
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
            for (int i = 0; i < columnsNames.Length; i++)
            {
                create.AddColumn(columnsNames[i], columnsTypes[i]);
            }
            return create;
        }

        private CreateDatabase CreateDatabaseQuery(Request request, IDatabaseContainer container)
        {
            CreateDatabase createDatabase = new CreateDatabase(container);
            createDatabase.targetDatabase = request.GetElementsContentByTagName(RequestAndRegexConstants.databaseTagName)[0];
            return createDatabase;
        }

        private DropDatabase DropDatabaseQuery(Request request, IDatabaseContainer container)
        {
            DropDatabase dropDatabase = new DropDatabase(container);
            dropDatabase.targetDatabase = request.GetElementsContentByTagName(RequestAndRegexConstants.databaseTagName)[0];
            return dropDatabase;
        }

        private CreateSecurityProfile CreateSecurityProfileQuery(Request request, IDatabaseContainer container)
        {
            CreateSecurityProfile createSecurityProfile = new CreateSecurityProfile(container);
            createSecurityProfile.targetDatabase = SystemeConstants.SystemDatabaseName;
            createSecurityProfile.targetTableName = SystemeConstants.ProfilesTableName;
            createSecurityProfile.SetProfileName(request.GetElementsContentByTagName(RequestAndRegexConstants.valueTagName)[0]);
            return createSecurityProfile;
        }

        private CreateUser CreateUserQuery(Request request, IDatabaseContainer container)
        {
            CreateUser createUser = new CreateUser(container);
            createUser.targetDatabase = SystemeConstants.SystemDatabaseName;
            createUser.targetTableName = SystemeConstants.UsersTableName;
            createUser.SetUser(request.GetElementsContentByTagName(RequestAndRegexConstants.usernameTagName)[0], request.GetElementsContentByTagName(RequestAndRegexConstants.passwordTagName)[0], request.GetElementsContentByTagName(RequestAndRegexConstants.securityProfileTag)[0]);
            return createUser;
        }

        private DeleteUser CreateDeleteUserQuery(Request request, IDatabaseContainer container)
        {
            DeleteUser deleteUser = new DeleteUser(container);
            deleteUser.targetDatabase = SystemeConstants.SystemDatabaseName;
            deleteUser.targetTableName = SystemeConstants.UsersTableName;
            deleteUser.SetTargetUserName(request.GetElementsContentByTagName(RequestAndRegexConstants.usernameTagName)[0]);
            return deleteUser;
        }

        private GrantDatabasePrivilege CreateGrantDatabasePrivilege(Request request, IDatabaseContainer container)
        {
            GrantDatabasePrivilege grantDatabasePrivilege = new GrantDatabasePrivilege(container);
            grantDatabasePrivilege.targetDatabase = SystemeConstants.SystemDatabaseName;
            grantDatabasePrivilege.targetTableName = SystemeConstants.PrivilegesOfProfilesOnDatabasesTableName;
            grantDatabasePrivilege.SetData(request.GetElementsContentByTagName(RequestAndRegexConstants.privilegeTag)[0], request.GetElementsContentByTagName(RequestAndRegexConstants.securityProfileTag)[0], request.GetElementsContentByTagName(RequestAndRegexConstants.databaseTagName)[0]);            
            return grantDatabasePrivilege;
        }

        private RevokeDatabasePrivilege CreateRevoqueDatabasePrivilege(Request request, IDatabaseContainer container)
        {
            RevokeDatabasePrivilege revokeDatabasePrivilege = new RevokeDatabasePrivilege(container);
            revokeDatabasePrivilege.targetDatabase = SystemeConstants.SystemDatabaseName;
            revokeDatabasePrivilege.targetTableName = SystemeConstants.PrivilegesOfProfilesOnDatabasesTableName;
            revokeDatabasePrivilege.SetData(request.GetElementsContentByTagName(RequestAndRegexConstants.securityProfileTag)[0], this.GetTargetDatabase(request), request.GetElementsContentByTagName(RequestAndRegexConstants.privilegeTag)[0]);
            return revokeDatabasePrivilege;
        }

        private DropSecurityProfile CreateDropSecurityProfile(Request request, IDatabaseContainer container) {
            DropSecurityProfile dropSecurityProfile = new DropSecurityProfile(container);
            dropSecurityProfile.targetDatabase = SystemeConstants.SystemDatabaseName;
            dropSecurityProfile.targetTableName = SystemeConstants.ProfilesTableName;
            dropSecurityProfile.SetTargetSecurityProfile(request.GetElementsContentByTagName(RequestAndRegexConstants.valueTagName)[0]);
            return dropSecurityProfile;
        }

        private GrantPrivilege CreateGrantPrivilege(Request request, IDatabaseContainer container)
        {
            GrantPrivilege grantPrivilege = new GrantPrivilege(container);
            grantPrivilege.targetDatabase = SystemeConstants.SystemDatabaseName;
            grantPrivilege.targetTableName = SystemeConstants.PrivilegesOfProfilesOnTablesTableName;
            grantPrivilege.SetData(request.GetElementsContentByTagName(RequestAndRegexConstants.privilegeTag)[0], request.GetElementsContentByTagName(RequestAndRegexConstants.securityProfileTag)[0], this.GetTargetDatabase(request), request.GetElementsContentByTagName(RequestAndRegexConstants.tableTagName)[0]);
            return grantPrivilege;
        }

        private RevoquePrivilege CreateRevoquePrivilege(Request request, IDatabaseContainer container)
        {
            RevoquePrivilege revokePrivilege = new RevoquePrivilege(container);
            revokePrivilege.targetDatabase = SystemeConstants.SystemDatabaseName;
            revokePrivilege.targetTableName = SystemeConstants.PrivilegesOfProfilesOnTablesTableName;
            revokePrivilege.SetData(request.GetElementsContentByTagName(RequestAndRegexConstants.securityProfileTag)[0], this.GetTargetDatabase(request), request.GetElementsContentByTagName(RequestAndRegexConstants.tableTagName)[0], request.GetElementsContentByTagName(RequestAndRegexConstants.privilegeTag)[0]);
            return revokePrivilege;
        }

        private Login CreateLogin(Request request, IDatabaseContainer container, IUserThread thread)
        {
            Login login = new Login(container, thread);
            login.targetDatabase = SystemeConstants.SystemDatabaseName;
            login.targetTableName = SystemeConstants.UsersTableName;
            login.SetData(request.GetElementsContentByTagName(RequestAndRegexConstants.usernameTagName)[0], request.GetElementsContentByTagName(RequestAndRegexConstants.passwordTagName)[0]);
            return login;
        }

        private Where CreateWhereClause(Request request)
        {
            Where where = new Where();
            string[] columnToEvaluate = request.GetElementsContentByTagName(RequestAndRegexConstants.toEvaluateColumnTagName);
            string[] evaluationValue = request.GetElementsContentByTagName(RequestAndRegexConstants.evalValueTagName);
            string[] operators = request.GetElementsContentByTagName(RequestAndRegexConstants.operatorTagName);
            OperatorFactory operatorFactory = OperatorFactory.GetOperatorFactory();
            for (int i = 0; i < columnToEvaluate.Length; i++)
            {
                where.AddCritery(columnToEvaluate[i], evaluationValue[i], operatorFactory.GetOperator(operators[i]));
            }
            return where;
        }

        private void SetDatabaseAndTableTarget(Request request, AbstractQuery query)
        {
            query.targetDatabase = this.GetTargetDatabase(request);
            query.targetTableName = request.GetElementsContentByTagName(RequestAndRegexConstants.tableTagName)[0];
        }

        private string GetTargetDatabase(Request request)
        {
            string defaultDatabase = ((ISystemeDatabaseModule)this.systeme.GetSystemeModule(SystemeConstants.SystemeDatabaseModule)).GetDefaultDatabaseName();
            string[] databaseGroups = request.GetElementsContentByTagName(RequestAndRegexConstants.databaseTagName);
            if (!(databaseGroups.Length == 0)) defaultDatabase = databaseGroups[0];
            return defaultDatabase;
        }

        public void SetSysteme(ISysteme systeme)
        {
            this.systeme = systeme;
        }

    }
}
