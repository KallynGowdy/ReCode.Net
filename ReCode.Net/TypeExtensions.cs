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
using ReCode.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCode
{
    /// <summary>
    /// Defines a static class that contains extension methods for <see cref="System.Type"/> objects.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Begins editing the given type.
        /// </summary>
        /// <param name="type">The type to edit.</param>
        /// <returns>Returns a new <see cref="ReCode.IType"/> object that can be edited.</returns>
        public static IType Edit(this Type type)
        {
            return TypeFactory.Instance.RetrieveInstanceForType(type);
        }

        /// <summary>
        /// Gets a new <see cref="Mono.Cecil.TypeDefinition"/> object that represents the given type.
        /// </summary>
        /// <param name="type">The type that the reference should be retrieved for.</param>
        /// <returns>Returns a new <see cref="Mono.Cecil.TypeDefinition"/> object</returns>
        public static TypeDefinition ToTypeDefinition(this IType type)
        {
            TypeDefinition t = new TypeDefinition(type.Namespace, type.Name, TypeAttributes.Class);
            t.Methods.Clear();
            t.Fields.Clear();
            t.Properties.Clear();

            foreach (IField f in type.Fields.Values)
            {
                t.Fields.Add(f.CreateField(t));
            }

            foreach (IProperty p in type.Properties.Values)
            {
                t.Properties.Add(p.CreateProperty(t));
            }
            return t;
        }

        /// <summary>
        /// Gets a new <see cref="Mono.Cecil.TypeReference"/> object that points to this type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static TypeReference GetTypeReference(this IType type, ModuleDefinition moduleDefinition)
        {
            return new TypeReference(type.Namespace, type.Name, moduleDefinition, moduleDefinition);
        }
    }
}
