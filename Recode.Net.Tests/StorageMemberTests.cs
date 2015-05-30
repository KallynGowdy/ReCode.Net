using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ReCode.Tests
{
    public class StorageMemberTests
    {
        [Fact]
        public void TestMemberRetrievesPrivateAccessFromFieldInfoProperly()
        {
            IType type = typeof(TypeTests.TestClass).Edit();
            EditableStorageMemberBase field = type.Members.OfType<EditableStorageMemberBase>().Single(m => m.Name == "privateInt");
            Assert.Equal(AccessModifier.Private, field.ReadAccess);
            Assert.Equal(AccessModifier.Private, field.WriteAccess);
        }

        [Fact]
        public void TestMemberRetrievesPublicAccessFromFieldInfoProperly()
        {
            IType type = typeof(TypeTests.TestClass).Edit();
            EditableStorageMemberBase field = type.Members.OfType<EditableStorageMemberBase>().Single(m => m.Name == "PublicInt");
            Assert.Equal(AccessModifier.Public, field.ReadAccess);
            Assert.Equal(AccessModifier.Public, field.WriteAccess);
        }

        [Fact]
        public void TestMemberRetrievesDifferentReadAndWriteAccessFromFieldInfoProperly()
        {
            IType type = typeof(TypeTests.TestClass).Edit();
            EditableStorageMemberBase field = type.Members.OfType<EditableStorageMemberBase>().Single(m => m.Name == "PublicReadPrivateSetInt");
            Assert.Equal(AccessModifier.Public, field.ReadAccess);
            Assert.Equal(AccessModifier.Private, field.WriteAccess);
        }

        [Fact]
        public void TestMemberCanSetReadAccessOfProperty()
        {
            var type = typeof(TypeTests.TestClass).Edit();
            var field = type.Properties["PublicReadPrivateSetInt"];

            field.ReadAccess = AccessModifier.Private;
            Assert.Equal(AccessModifier.Private, field.ReadAccess);
        }

        [Fact]
        public void TestMemberDeterminesWhetherPropertyHasSetterCorrectly()
        {
            var type = typeof(TypeTests.TestClass).Edit();
            var field = type.Properties["PublicReadOnlyInt"];

            Assert.False(field.CanWrite);
        }

        [Fact]
        public void TestMemberDeterminesWhetherPropertyHasGetterCorrectly()
        {
            var type = typeof(TypeTests.TestClass).Edit();
            var field = type.Properties["PublicSetOnlyInt"];

            Assert.False(field.CanRead);
        }
    }
}
