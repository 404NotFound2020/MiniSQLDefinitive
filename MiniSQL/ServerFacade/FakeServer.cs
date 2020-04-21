using MiniSQL.Constants;
using MiniSQL.Initializer;
using MiniSQL.Interfaces;
using MiniSQL.Querys;
using MiniSQL.SystemeClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.ServerFacade
{
    public class FakeServer : IUserThread
    {        
        public string username { get; set; }
        private static FakeServer fakeServer;

        private FakeServer() 
        {
            this.SetSystemeModules();
            QueryFactory.GetQueryFactory().SetSysteme(Systeme.GetSystem());
            this.username = "anonimous";
        }

        private void SetSystemeModules()
        {
            Systeme systeme = Systeme.GetSystem();
            SystemeDatabaseContainerModule databaseContainerModule = SystemeDatabaseContainerModule.GetSystemeDatabaseContainerModule();
            SystemeDataInicializationModule dataInicializationModule = SystemeDataInicializationModule.GetSystemeDataInicializationModule();
            SystemePrivilegeModule privilegeModule = SystemePrivilegeModule.GetSystemePrivilegeModule();
            databaseContainerModule.SetSysteme(systeme);
            dataInicializationModule.SetSysteme(systeme);
            privilegeModule.SetSysteme(systeme);
            systeme.SetActiveModule(databaseContainerModule);
            systeme.SetActiveModule(privilegeModule);
            systeme.SetSystemeModule(dataInicializationModule);
            databaseContainerModule.AcoplateTheModule();
            dataInicializationModule.AcoplateTheModule();
            privilegeModule.AcoplateTheModule();
        }

        public static FakeServer GetFakeServer() 
        {
            if (fakeServer == null) fakeServer = new FakeServer();
            return fakeServer;
        }

        public string[] ReturnRegex() 
        { 
            return new string[]{ RequestAndRegexConstants.selectPattern, RequestAndRegexConstants.insertPattern, RequestAndRegexConstants.updatePattern, RequestAndRegexConstants.createPattern, RequestAndRegexConstants.deletePattern, RequestAndRegexConstants.dropPattern, RequestAndRegexConstants.createDatabasePattern, RequestAndRegexConstants.dropDatabasePattern, RequestAndRegexConstants.deleteUser, RequestAndRegexConstants.createUser, RequestAndRegexConstants.grantDatabasePrivilege, RequestAndRegexConstants.grantTablePrivilege, RequestAndRegexConstants.revokeTablePrivilege, RequestAndRegexConstants.revokeDatabasePrivilege, RequestAndRegexConstants.createSecurityProfile, RequestAndRegexConstants.dropSecurityProfile, RequestAndRegexConstants.login };
        }

        public string ReceiveRequest(string request) {
            AbstractQuery query = QueryFactory.GetQueryFactory().GetQuery(new Request(request), this);           
            query.ValidateParameters();
            query.ValidatePrivileges((ISystemePrivilegeModule) Systeme.GetSystem().GetSystemeModule(SystemeConstants.SystemePrivilegeModule));            
            query.Execute();            
            return query.GetResult();        
        }

        public void SaveShit() 
        {
           ((SystemeDatabaseContainerModule) Systeme.GetSystem().GetSystemeModule(SystemeConstants.SystemeDatabaseModule)).SaveAll();
        }

    }
}
