using ReCode.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReCode
{
    /// <summary>
    /// Defines a class that contains extension methods for <see cref="System.Reflection.Assembly"/> and <see cref="ReCode.IAssembly"/> types.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Gets an <see cref="ReCode.IAssembly"/> object that allows for editing the current assemblie's representation.
        /// </summary>
        /// <param name="assembly">The assembly to edit.</param>
        /// <returns>Returns an <see cref="ReCode.IAssembly"/> object that can be modified.</returns>
        public static IAssembly Edit(this Assembly assembly)
        {
            return AssemblyFactory.Instance.RetrieveInstanceForAssembly(assembly);
        }
    }
}
