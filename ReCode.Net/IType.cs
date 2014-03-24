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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCode
{
    /// <summary>
    /// Defines an interface for objects that represent an editable type.
    /// </summary>
    public interface IType
    {
        /// <summary>
        /// Gets the full name of the type.
        /// </summary>
        string FullName
        {
            get;
        }

        /// <summary>
        /// Gets or sets the name of this type.
        /// </summary>
        string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the dictionary of fields that this type contains.
        /// </summary>
        IDictionary<string, IField> Fields
        {
            get;
        }

        /// <summary>
        /// Gets the dictionary of properties that this type contains,
        /// </summary>
        //IDictionary<string, IProperty> Properties
        //{
        //    get;
        //}

        /// <summary>
        /// Gets the collection of members that this type contains.
        /// </summary>
        ICollection<IMember> Members
        {
            get;
        }

        /// <summary>
        /// Gets or sets the module that this type lives in.
        /// </summary>
        IModule Module
        {
            get;
            set;
        }
    }
}
