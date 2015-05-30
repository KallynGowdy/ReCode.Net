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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCode
{
    /// <summary>
    /// Defines a class that contains extension methods for <see cref="ReCode.IModule"/> objects.
    /// </summary>
    public static class ModuleExtensions
    {
        /// <summary>
        /// Creates a new <see cref="Mono.Cecil.ModuleDefinition"/> object that represents the given <see cref="ReCode.IModule"/> object.
        /// </summary>
        /// <param name="module">The module that a definition should be created for.</param>
        /// <returns>Returns a new <see cref="Mono.Cecil.ModuleDefinition"/> object that represents the given <see cref="ReCode.IModule"/> object.</returns>
        public static ModuleDefinition ToModuleDefinition(this IModule module)
        {
            //TODO: Refactor ModuleKind to IModule property
            ModuleDefinition m = ModuleDefinition.CreateModule(module.FullName, ModuleKind.Dll);
            m.Types.Clear();
            foreach (IType t in module.Types.Values)
            {
                m.Types.Add(t.ToTypeDefinition());
            }
            return m;
        }

    }
}

