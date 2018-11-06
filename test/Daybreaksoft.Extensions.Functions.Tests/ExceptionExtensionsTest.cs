using Daybreaksoft.Extensions.Functions.Tests.Requires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Daybreaksoft.Extensions.Functions.Tests
{
    public class ExceptionExtensionsTest
    {
        [Fact]
        public void TestGetRootMessage()
        {
            try
            {
                Method1();
            }
            catch (Exception e)
            {
                Assert.Equal("Throw method 2", e.GetRootMessage());
            }
        }

        private void Method1()
        {
            try
            {
                Method2();
            }
            catch (Exception e)
            {
                throw new Exception("Throw method 1", e);
            }
        }

        private void Method2()
        {
            throw new Exception("Throw method 2");
        }
    }
}
