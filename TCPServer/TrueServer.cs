using MiniSQL.Constants;
using MiniSQL.Interfaces;
using MiniSQL.Querys;
using MiniSQL.ServerFacade;
using MiniSQL.SystemeClasses;
using NetworkUtilities.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TCPServer.Drugs;

namespace TCPServer
{
    class TrueServer
    {
        private static TcpListener tcpListener;
        private static List<UserThread> clients;
        private static string regexMessage;

        static void Main(string[] args)
        {
            regexMessage = CreateRegexXmlMessage(new string[] { RequestAndRegexConstants.selectPattern, RequestAndRegexConstants.insertPattern, RequestAndRegexConstants.updatePattern, RequestAndRegexConstants.createPattern, RequestAndRegexConstants.deletePattern, RequestAndRegexConstants.dropPattern, RequestAndRegexConstants.createDatabasePattern, RequestAndRegexConstants.dropDatabasePattern, RequestAndRegexConstants.deleteUser, RequestAndRegexConstants.createUser, RequestAndRegexConstants.grantDatabasePrivilege, RequestAndRegexConstants.grantTablePrivilege, RequestAndRegexConstants.revokeTablePrivilege, RequestAndRegexConstants.revokeDatabasePrivilege, RequestAndRegexConstants.createSecurityProfile, RequestAndRegexConstants.dropSecurityProfile, RequestAndRegexConstants.login, RequestAndRegexConstants.exit });
            InitializeSysteme();
            QueryFactory.GetQueryFactory().SetSysteme(Systeme.GetSystem());
            InitialiceListener("192.168.0.10", 8088);
            RunServer();

        }

        private static void InitializeSysteme()
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

        private static void InitialiceListener(string ip, int port)
        {
            tcpListener = new TcpListener(IPAddress.Parse(ip), port);
        }

        private static void RunServer()
        {
            clients = new List<UserThread>();
            tcpListener.Start();
            while (true) {
                UserThread userThread = new UserThread(tcpListener.AcceptTcpClient());
                userThread.SendMessage(regexMessage);
                userThread.Run();               
            }
        }

        private static string CreateRegexXmlMessage(string[] regexList)
        {
            string xmlRegexPartMessage = "";
            for (int i = 0; i < regexList.Length; i++) xmlRegexPartMessage = xmlRegexPartMessage + XmlMessage.CreateTextNode("regex", regexList[i]);
            return xmlRegexPartMessage;
        }

        public static string ReceiveRequest(UserThread thread, string request)
        {
            AbstractQuery query = QueryFactory.GetQueryFactory().GetQuery(new XmlMessage(request), thread);
            Console.WriteLine(thread.username);
            query.ValidateParameters();
            query.Execute();
            return query.GetResult();
        }

        



    }
}
