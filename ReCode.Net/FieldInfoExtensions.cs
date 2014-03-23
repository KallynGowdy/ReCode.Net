using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReCode
{
    /// <summary>
    /// Defines a static class that contains extension methods for <see cref="System.Reflection.FieldInfo"/> objects.
    /// </summary>
    public static class FieldInfoExtensions
    {
        /// <summary>
        /// Gets the access modifiers that are applied to this field.
        /// </summary>
        /// <param name="field">The field that the modifiers should be retrieved from.</param>
        /// <returns>Returns a new <see cref="ReCode.AccessModifier"/> object that represents the access modifiers that are applied to the field.</returns>
        public static AccessModifier GetAccessModifiers(this FieldInfo field)
        {
            if (field.IsPublic)
            {
                return AccessModifier.Public;
            }
            else if (field.IsPrivate)
            {
                return AccessModifier.Private;                
            }
            else if (field.IsFamily)
            {
                return AccessModifier.Protected;
            }
            else if (field.IsFamilyAndAssembly)
            {
                return AccessModifier.ProtectedAndInternal;
            }
            else if (field.IsFamilyOrAssembly)
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
