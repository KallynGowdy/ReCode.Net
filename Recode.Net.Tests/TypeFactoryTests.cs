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
using ReCode.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCode.Tests
{
    [TestFixture]
    public class TypeFactoryTests
    {
        [TestCase(typeof(int))]
        public void TestRetrieveSameInstance(Type type)
        {
            TypeFactory factory = TypeFactory.Instance;

            Assert.NotNull(factory);

            IType t1 = factory.RetrieveInstanceForType(type);

            Assert.NotNull(t1);

            IType t2 = factory.RetrieveInstanceForType(type);

            Assert.NotNull(t2);

            Assert.AreSame(t1, t2);
        }
    }
}
