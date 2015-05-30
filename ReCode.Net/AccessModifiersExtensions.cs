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
    /// Defines a static class that contains a list of extension methods for <see cref="ReCode.AccessModifier"/> objects.
    /// </summary>
    public static class AccessModifiersExtensions
    {
        /// <summary>
        /// Gets the string representation of the current <see cref="ReCode.AccessModifier"/> object as it would be represented in C#.
        /// </summary>
        /// <remarks>
        /// Because C# does not have a construct that specifies that a member's access is 'protected and internal' (but IL does) this method returns
        /// the C# construct for 'protected *or* internal' in that case. Normally, you wouldn't notice a difference.
        /// </remarks>
        /// <returns>Returns a new string that represents the given access modifiers.</returns>
        public static string ToNaturalString(this AccessModifier modifiers)
        {
            switch (modifiers)
            {
                case AccessModifier.Private:
                case AccessModifier.Public:
                case AccessModifier.Protected:
                    return modifiers.ToString().ToLower();
                default:
                    return "protected internal";
            }
        }

        /// <summary>
        /// Gets a <see cref="Mono.Cecil.FieldAttributes"/> object that represent the attributes that are equivalent to these modifiers.
        /// </summary>
        /// <param name="modifiers">The access modifiers that represent attributes applied to a type member.</param>
        /// <returns>Returns a new <see cref="Mono.Cecil.FieldAttributes"/> object that contains the given modifiers.</returns>
        public static FieldAttributes ToFieldAttributes(this AccessModifier modifiers)
        {
            switch (modifiers)
            {
                case AccessModifier.Internal:
                    return FieldAttributes.Assembly;
                case AccessModifier.Private:
                    return FieldAttributes.Private;
                case AccessModifier.Protected:
                    return FieldAttributes.Family;
                case AccessModifier.Public:
                    return FieldAttributes.Public;
                case AccessModifier.ProtectedAndInternal:
                    return FieldAttributes.FamANDAssem;
                default:
                    return FieldAttributes.FamORAssem;
            }
        }

        /// <summary>
        /// Gets a <see cref="Mono.Cecil.FieldAttributes"/> object that represent the attributes that are equivalent to these modifiers.
        /// </summary>
        /// <param name="modifiers">The access modifiers that represent attributes applied to a type member.</param>
        /// <returns>Returns a new <see cref="Mono.Cecil.FieldAttributes"/> object that contains the given modifiers.</returns>
        public static MethodAttributes ToMethodAttributes(this AccessModifier modifiers)
        {
            switch (modifiers)
            {
                case AccessModifier.Internal:
                    return MethodAttributes.Assembly;
                case AccessModifier.Private:
                    return MethodAttributes.Private;
                case AccessModifier.Protected:
                    return MethodAttributes.Family;
                case AccessModifier.Public:
                    return MethodAttributes.Public;
                case AccessModifier.ProtectedAndInternal:
                    return MethodAttributes.FamANDAssem;
                default:
                    return MethodAttributes.FamORAssem;
            }
        }
    }
}
