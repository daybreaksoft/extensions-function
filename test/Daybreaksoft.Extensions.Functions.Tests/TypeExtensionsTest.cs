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
    public class TypeExtensionsTest
    {
        [Fact]
        public void TestFindProperties()
        {
            var type1 = typeof(Model1);
            PropertyInfo[] properties;

            // Verifies that there is one result
            properties = type1.FindProperties<Attribute1>();
            Assert.Single(properties);
            Assert.Equal("P1", properties.First().Name);

            // Verifies that there are multiple result
            properties = type1.FindProperties<Attribute2>();
            Assert.Equal(3, properties.Count());
            Assert.Equal("P3", properties[1].Name);

            // Verifies that none result
            properties = type1.FindProperties<Attribute3>();
            Assert.Empty(properties);
        }

        [Fact]
        public void TestFindProperty()
        {
            var type1 = typeof(Model1);
            PropertyInfo property;

            // Verifies that there is one result 
            property = type1.FindProperty<Attribute1>();
            Assert.NotNull(property);
            Assert.Equal("P1", property.Name);

            // Verifies that throw exception if there are more than one reulst
            Assert.Throws<MultipleResultException>(() =>
            {
                type1.FindProperty<Attribute2>();
            });

            // Verifies that none result
            property = type1.FindProperty<Attribute3>();
            Assert.Null(property);
        }

        [Fact]
        public void TestInvokeMethod()
        {
            var obj = new MethodTest();
            var type = obj.GetType();

            // Verifies that call method correctly
            type.InvokeMethod("Add", obj, 5);
            Assert.Equal(5, obj.Count);

            // Verifies that not find method
            Assert.Throws<NullReferenceException>(() => { type.InvokeMethod("NoMethod", obj, 1); });
        }
    }
}
