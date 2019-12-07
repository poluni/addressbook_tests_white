using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_tests_white
{
    public class TestBase
    {
        public ApplicationManager app;

        [TestFixtureSetUp]
        public void InitApplication() => app = new ApplicationManager();

        [TestFixtureTearDown]
        public void StopApplication() => app.Stop();
    }
}
