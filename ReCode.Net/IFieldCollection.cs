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
    /// Defines an interface for an object that contains a set of fields.
    /// </summary>
    public interface IFieldCollection
    {
        /// <summary>
        /// Adds the given field to the collection.
        /// </summary>
        /// <param name="field">The field to add to the collection.</param>
        void Add(IField field);

        /// <summary>
        /// Removes the field with the given name from the collection.
        /// </summary>
        /// <param name="fieldName">The name of the field to remove from the collection.</param>
        /// <returns></returns>
        bool Remove(string fieldName);

        /// <summary>
        /// Gets or sets the field with the given name to the given value.
        /// </summary>
        /// <param name="name">The name of the field to retrieve.</param>
        /// <returns>Returns the <see cref="ReCode.IField"/> object with the given name.</returns>
        IField this[string name]
        {
            get;
            set;
        }
    }
}
