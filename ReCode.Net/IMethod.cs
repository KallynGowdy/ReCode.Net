using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace ReCode
{
    /// <summary>
    /// Defines an interface for a method that is editable.
    /// </summary>
    public interface IMethod : IMember, IAccess, IEquatable<IMethod>
    {
        /// <summary>
        /// Gets or sets the return type of this method.
        /// </summary>
        IType ReturnType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the dictionary of parameters that this method contains.
        /// </summary>
        IDictionary<string, IParameter> Parameters
        {
            get;
        }

        /// <summary>
        /// Invokes this method and returns the result.
        /// </summary>
        /// <param name="arguments">A list of arguments to provide the method with.</param>
        /// <param name="reference">A reference to the object that this method belongs to.</param>
        /// <returns>Returns the result of the method invokation.</returns>
        object Invoke(object reference, params object[] arguments);

        MethodDefinition CreateMethod(TypeDefinition typeDefinition);
    }
}
