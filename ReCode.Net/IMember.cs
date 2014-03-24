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
    /// Defines an interface for objects that define editable members of a type.
    /// </summary>
    public interface IMember
    {
        /// <summary>
        /// Gets or sets the name of the member.
        /// </summary>
        string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the full name of the member.
        /// </summary>
        string FullName
        {
            get;
        }

        /// <summary>
        /// Gets or sets the type that declares this member.
        /// </summary>
        IType DeclaringType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether this member is static.
        /// </summary>
        bool IsStatic
        {
            get;
            set;
        }
    }
}
