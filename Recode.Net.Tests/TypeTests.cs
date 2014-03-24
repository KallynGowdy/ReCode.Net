// Copyright 2014 Kallyn Gowdy
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCode.Tests
{
    /// <summary>
    /// A class that defines a set of tests for manipulating types.
    /// </summary>
    [TestFixture]
    public class TypeTests
    {
        public class TestClass
        {
            private int privateInt;

            public int PublicReadOnlyInt
            {
                get
                {
                    return privateInt;
                }
            }

            public int PublicSetOnlyInt
            {
                set
                {
                    privateInt = value;
                }
            }
        }

        public class AlternateTestClass
        {

        }

        [Test]
        public void TestManipulateFieldAccess()
        {
            IType testType = typeof(TestClass).Edit();

            Assert.AreEqual(AccessModifier.Private, testType.Fields["privateInt"].Access);

            testType.Fields["privateInt"].Access = AccessModifier.Public;

            Assert.AreEqual(AccessModifier.Public, testType.Fields["privateInt"].Access);

            //Reset changes
            testType.Fields["privateInt"].Access = AccessModifier.Private;
        }

        [Test]
        public void TestManipulateFieldName()
        {
            IType testType = typeof(TestClass).Edit();

            Assert.NotNull(testType.Fields["privateInt"]);

            Assert.AreEqual("privateInt", testType.Fields["privateInt"].Name);

            testType.Fields["privateInt"].Name = "reallyPrivateInt";

            Assert.Null(testType.Fields["privateInt"]);

            Assert.AreEqual("reallyPrivateInt", testType.Fields["reallyPrivateInt"].Name);

            //Reset changes
            testType.Fields["reallyPrivateInt"].Name = "privateInt";
        }

        [Test]
        public void TestManipulateFieldType()
        {
            IType testType = typeof(TestClass).Edit();

            IField testTypeField = testType.Fields["privateInt"];

            Assert.NotNull(testTypeField);

            Assert.AreEqual(typeof(int).Edit(), testTypeField.FieldType);

            testTypeField.FieldType = typeof(float).Edit();

            Assert.AreEqual(typeof(float).Edit(), testTypeField.FieldType);

            //Reset changes
            testTypeField.FieldType = typeof(int).Edit();
        }

        [Test]
        public void TestManipulateFieldDeclaringType()
        {
            IType testType = typeof(TestClass).Edit();

            IField field = testType.Fields["privateInt"];

            Assert.NotNull(field);

            Assert.AreEqual(testType, field.DeclaringType);

            field.DeclaringType = typeof(AlternateTestClass).Edit();

            IField goneField = testType.Fields["privateInt"];

            Assert.Null(goneField);

            IType alternateType = typeof(AlternateTestClass).Edit();

            Assert.NotNull(alternateType.Fields["privateInt"]);

            //Reset changes
            field.DeclaringType = testType;
        }

        [TestCase(typeof(TestClass), "NewType")]
        public void TestManipulateTypeName(Type type, string newName)
        {
            IType t = type.Edit();

            Assert.NotNull(t);

            string originalName = t.Name;

            IModule m = t.Module;

            Assert.NotNull(m.Types[originalName]);

            t.Name = newName;

            Assert.NotNull(m.Types[newName]);

            t.Name = originalName;
        }

        [Test]
        public void TestManipulateTypeModule()
        {
            IType type = typeof(TestClass).Edit();

            Assert.NotNull(type);

            IModule module = type.Module;

            Assert.NotNull(module);

            IAssembly assembly = module.Assembly;

            Assert.NotNull(assembly);

            if (assembly.Modules.Count > 1)
            {
                type.Module = assembly.Modules.Last();

                Assert.False(module.Types.Values.Contains(type));

                Assert.True(type.Module.Types.Values.Contains(type));

                type.Module = module;
            }
        }
    }
}
