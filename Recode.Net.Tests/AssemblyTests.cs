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

using Mono.Cecil;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReCode.Tests
{
    [TestFixture]
    public class AssemblyTests
    {
        [TestCase("RenamedAssembly")]
        public void TestRenameAssembly(string newName)
        {
            IAssembly assembly = Assembly.GetExecutingAssembly().Edit();

            string oldName = assembly.Name;

            assembly.Name = newName;

            Assert.AreEqual(newName, typeof(AssemblyTests).Edit().Module.Assembly.Name);

            assembly.Name = oldName;
        }

        [TestCase("AssemblyTests")]
        public void TestRetrieveType(string typeName)
        {
            IAssembly assembly = Assembly.GetExecutingAssembly().Edit();

            IType t = assembly.Types[typeName];
            Assert.NotNull(t);
        }

        [TestCase("AssemblyTests", "OtherType")]
        public void TestSaveAssembly(string typeName, string newName)
        {
            IAssembly assembly = Assembly.GetExecutingAssembly().Edit();

            IType t = assembly.Types[typeName];
            t.Name = newName;

            Assert.True(assembly.Types.Keys.Contains(newName), "The name change did not propagate to the dictionary of types in the assembly");

            Mono.Cecil.AssemblyDefinition def = assembly.ToAssemblyDefinition();

            Assert.That(def.Modules.Count, Is.EqualTo(assembly.Modules.Count));

            foreach (Tuple<IModule, ModuleDefinition> m in assembly.Modules.Zip(def.Modules, (a, b) => new Tuple<IModule, ModuleDefinition>(a, b)))
            {
                Assert.That(m.Item2.Types.Count, Is.EqualTo(m.Item1.Types.Count));
            }
        }
    }
}
