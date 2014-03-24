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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReCode
{
    /// <summary>
    /// Defines a static class that contains extension methods for <see cref="System.Reflection.PropertyInfo"/> objects.
    /// </summary>
    public static class PropertyInfoExtensions
    {
        /// <summary>
        /// Gets the least-restrictive access modifiers that are applied to one of this property's methods.
        /// </summary>
        /// <param name="prop">The property that the modifiers should be retrieved from.</param>
        /// <returns>Returns a new <see cref="ReCode.AccessModifier"/> object that represents the access modifiers that are applied to the property.</returns>
        public static AccessModifier GetAccessModifiers(this PropertyInfo prop)
        {
            return (new[] 
            { 
                prop.CanRead ? prop.GetMethod.GetAccessModifiers() : AccessModifier.Private, 
                prop.CanWrite ? prop.SetMethod.GetAccessModifiers() : AccessModifier.Private 
            }).Max();
        }
    }
}
