using MiniSQL.Constants;
using MiniSQL.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniSQL.SaveDataFormatManagers;

namespace UnitTests.Test.TestObjectsContructor
{
    class EnviromentTestSetup
    {
        public static void SetupEnviroment()
        {
            EnviromentTestSetup.SetupDataTypes();
        }


        public static void SetupDataTypes()
        {
            TypesKeyConstants.ReturnKeys().ForEach((string key) => DataTypesFactory.GetDataTypesFactory().GetDataType(key).SetDataFormatManager(NothingToDoManager.GetNothingToDoManager()));
        }





    }
}
