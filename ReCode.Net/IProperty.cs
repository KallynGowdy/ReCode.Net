﻿// Copyright 2014 Kallyn Gowdy
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
    /// Defines an interface for an editable property.
    /// </summary>
    public interface IProperty : IStorageMember, IAccess, IEquatable<IProperty>
    {
        /// <summary>
        /// Gets or sets the method that retrieves the values from this property.
        /// Null if this property does not contain a 'get' method.
        /// </summary>
        IMethod GetMethod
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the method that writes values to this property.
        /// Null if this property does not contain a 'set' method.
        /// </summary>
        IMethod SetMethod
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of values that this property manipulates.
        /// </summary>
        IType PropertyType
        {
            get;
            set;
        }

        PropertyDefinition CreateProperty(TypeDefinition typeDefinition);
    }
}
