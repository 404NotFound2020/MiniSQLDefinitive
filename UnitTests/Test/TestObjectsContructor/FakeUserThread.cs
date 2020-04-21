using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Test.TestObjectsContructor
{
    public class FakeUserThread : IUserThread
    {
        public string username { get; set; }

        public FakeUserThread() {
            this.username = "anonimous";
        }
    }
}
