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
    /// Defines a static class that contains extension methods for IField objects.
    /// </summary>
    public static class FieldExtensions
    {
        /// <summary>
        /// Gets the list of attributes that are applied to this field.
        /// </summary>
        /// <param name="field">The field that the attributes should be retrieved from.</param>
        /// <returns>Returns a new <see cref="Mono.Cecil.FieldAttributes"/> object.</returns>
        public static FieldAttributes GetMonoFieldAttributes(this IField field)
        {
            FieldAttributes attributes = field.Access.ToFieldAttributes();

            if (field.IsStatic)
            {
                attributes |= FieldAttributes.Static;
            }

            return attributes;
        }

        /// <summary>
        /// Creates a new <see cref="Mono.Cecil.FieldDefinition"/> object that represents this field.
        /// </summary>
        /// <param name="field">The field that a new <see cref="Mono.Cecil.FieldDefinition"/> object should be created for.</param>
        /// <returns>Returns a new <see cref="Mono.Cecil.FieldDefinition"/> object that represents this field.</returns>
        public static FieldDefinition ToFieldDefinition(this IField field, ModuleDefinition module)
        {
            FieldDefinition f = new FieldDefinition(field.Name, field.GetMonoFieldAttributes(), field.FieldType.GetTypeReference(module));

            

            return f;
        }
    }
}
