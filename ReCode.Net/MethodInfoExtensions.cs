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
    /// Defines a static class that contains extension methods for <see cref="System.Reflection.MethodInfo"/> objects.
    /// </summary>
    public static class MethodInfoExtensions
    {
        /// <summary>
        /// Gets the access modifiers that are applied to this method.
        /// </summary>
        /// <param name="method">The method that the modifiers should be retrieved from.</param>
        /// <returns>Returns a new <see cref="ReCode.AccessModifier"/> object that represents the access modifiers that are applied to the method.</returns>
        public static AccessModifier GetAccessModifiers(this MethodInfo method)
        {
            if (method.IsPublic)
            {
                return AccessModifier.Public;
            }
            else if (method.IsPrivate)
            {
                return AccessModifier.Private;
            }
            else if (method.IsFamily)
            {
                return AccessModifier.Protected;
            }
            else if (method.IsFamilyAndAssembly)
            {
                return AccessModifier.ProtectedAndInternal;
            }
            else if (method.IsFamilyOrAssembly)
            {
                return AccessModifier.ProtectedOrInternal;
            }
            else
            {
                return AccessModifier.Internal;
            }
        }
    }
}
