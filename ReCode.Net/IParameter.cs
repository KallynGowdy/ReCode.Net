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
using Mono.Cecil;

namespace ReCode
{
    /// <summary>
    /// Defines an interface for a parameter in a method.
    /// </summary>
    public interface IParameter : IMember, IEquatable<IParameter>
    {
        /// <summary>
        /// Gets or sets the type of objects that this parameter accepts as input.
        /// </summary>
        IType ParameterType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the method that this parameter belongs to.
        /// </summary>
        IMethod Method
        {
            get;
            set;
        }

        ParameterDefinition CreateParameter(TypeDefinition typeDefinition);
    }
}
