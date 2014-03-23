using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCode
{
    /// <summary>
    /// Defines an interface for an <see cref="ReCode.IMember"/> object that has the ablility to act like a field.
    /// </summary>
    public interface IStorageMember : IMember, IEquatable<IStorageMember>
    {
        /// <summary>
        /// Gets whether the member can read it's value.
        /// </summary>
        bool CanRead
        {
            get;
        }

        /// <summary>
        /// Gets whether the member can write it's value.
        /// </summary>
        bool CanWrite
        {
            get;
        }

        /// <summary>
        /// Gets the type of values that this member stores.
        /// </summary>
        IType StoredType
        {
            get;
        }
    }
}
