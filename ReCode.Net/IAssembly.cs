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

namespace ReCode
{
    /// <summary>
    /// Defines an interface for an assembly.
    /// </summary>
    public interface IAssembly : IEquatable<IAssembly>
    {
        /// <summary>
        /// Gets or sets the name of the assembly.
        /// </summary>
        string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the full name of the assembly.
        /// </summary>
        string FullName
        {
            get;
        }

        /// <summary>
        /// Gets the location that this assembly lives in on disk.
        /// </summary>
        string Location
        {
            get;
        }

        /// <summary>
        /// Gets the collection of modules that the assembly contains.
        /// </summary>
        ICollection<IModule> Modules
        {
            get;
        }

        /// <summary>
        /// Gets the collection of types that belong to this assembly.
        /// </summary>
        IDictionary<string, IType> Types
        {
            get;
        }
    }
}
