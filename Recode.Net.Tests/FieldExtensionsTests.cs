using Mono.Cecil;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCode.Tests
{
    [TestFixture]
    public class FieldExtensionsTests
    {
        public class TestType
        {
            public string PublicField;

            public static string PublicStaticField;
        }

        [TestCase(typeof(TestType), "PublicField", FieldAttributes.Public)]
        [TestCase(typeof(TestType), "PublicStaticField", FieldAttributes.Public | FieldAttributes.Static)]
        public void TestGetFieldAttributes(Type type, string fieldName, FieldAttributes expected)
        {
            IField field = type.Edit().Fields[fieldName];

            Assert.NotNull(field);

            Assert.AreEqual(expected, field.GetMonoFieldAttributes());
        }

    }
}
