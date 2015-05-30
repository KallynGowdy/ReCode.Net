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
using System.Threading.Tasks;

namespace ReCode
{
    /// <summary>
    /// Defines a generic interface for an object that saves it's changes.
    /// </summary>
    /// <typeparam name="TSaver">The type of object that allows the object to apply it's values.</typeparam>
    public interface IApplyChanges<TSaver>
    {
        /// <summary>
        /// Causes this object to apply it's values using the given object as output.
        /// </summary>
        /// <param name="saver">The object to use for output.</param>
        void Apply(TSaver saver);
    }
}
