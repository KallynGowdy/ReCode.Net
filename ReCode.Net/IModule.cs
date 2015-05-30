using Mono.Cecil;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReCode
{
    /// <summary>
    /// Defines an interface for a module that belongs to an assembly.
    /// </summary>
    public interface IModule : IEquatable<IModule>
    {
        /// <summary>
        /// Gets the assembly that this module belongs to.
        /// </summary>
        IAssembly Assembly
        {
            get;
        }

        /// <summary>
        /// Gets the collection of types that this module contains.
        /// </summary>
        IDictionary<string, IType> Types
        {
            get;
        }

        /// <summary>
        /// Gets the fully qualified name of the module.
        /// </summary>
        string FullName
        {
            get;
        }

        /// <summary>
        /// Gets the architecture that this module targets.
        /// </summary>
        /// <value>
        /// The target architecture.
        /// </value>
        TargetArchitecture TargetArchitecture
        {
            get;
        }

        /// <summary>
        /// Gets the runtime that this module targets.
        /// </summary>
        TargetRuntime TargetRuntime
        {
            get;
        }

        /// <summary>
        /// Gets the kind that this module is. (Dll, Console, etc.)
        /// </summary>
        ModuleKind ModuleKind
        {
            get;
        }
    }
}
