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

        /// <summary>
        /// Gets or sets the access modifiers that are applied when reading from this member.
        /// </summary>
        AccessModifier ReadAccess
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the access modifiers that are applied when writing to this member.
        /// </summary>
        AccessModifier WriteAccess
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the value stored in the given object in this member.
        /// </summary>
        /// <param name="reference">The object that the value for this member should be retrieved from.</param>
        /// <exception cref="System.InvalidOperationException">Thrown if this member cannot retrieve it's value.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if the given reference is null and this member is not static.</exception>
        /// <returns>Returns the value stored in this member.</returns>
        object GetValue(object reference);

        /// <summary>
        /// Sets the value stored in this member in the given object to the given value.
        /// </summary>
        /// <param name="reference">The object that the value for this member should be set for.</param>
        /// <param name="value">The value to set to.</param>
        /// <exception cref="System.InvalidOperationException">Thrown if this member cannot set it's value.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if the given reference is null and this member is not static.</exception>
        void SetValue(object reference, object value);
    }
}
