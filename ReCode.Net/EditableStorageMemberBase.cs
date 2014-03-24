using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReCode
{
    /// <summary>
    /// Defines a base class for members of types that provide easy access to values.
    /// </summary>
    public abstract class EditableStorageMemberBase : EditableMemberBase, IStorageMember
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditableStorageMemberBase"/> class.
        /// </summary>
        /// <param name="field">The field that this member represents.</param>
        protected EditableStorageMemberBase(FieldInfo field)
            : base(field)
        {
            this.StoredType = field.FieldType.Edit();

            this.GetFunction = r => field.GetValue(r);

            this.SetFunction = (r, v) => field.SetValue(r, v);

            AccessModifier access = field.GetAccessModifiers();

            ReadAccess = access;
            WriteAccess = access;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditableStorageMemberBase"/> class.
        /// </summary>
        /// <param name="prop">The property that this member represents.</param>
        protected EditableStorageMemberBase(PropertyInfo prop)
            : base(prop)
        {
            this.StoredType = prop.PropertyType.Edit();

            initDefaultAccessModifiers();

            if (prop.CanRead)
            {
                this.GetFunction = r => prop.GetValue(r);
                ReadAccess = prop.SetMethod.GetAccessModifiers();
            }
            if (prop.CanWrite)
            {
                this.SetFunction = (r, v) => prop.SetValue(r, v);
                WriteAccess = prop.SetMethod.GetAccessModifiers();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditableStorageMemberBase"/> class.
        /// </summary>
        /// <param name="name">The name of this member</param>
        /// <param name="storedType">The type of objects stored in this member.</param>
        /// <param name="getFunction">The get function for this member.</param>
        /// <param name="setFunction">The set function for this member.</param>
        protected EditableStorageMemberBase(string name, IType storedType, Func<object, object> getFunction, Action<object, object> setFunction)
            : base(name)
        {
            this.StoredType = storedType;
            this.GetFunction = getFunction;
            this.SetFunction = setFunction;
            initDefaultAccessModifiers();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditableStorageMemberBase"/> class.
        /// </summary>
        /// <param name="name">The name of this member.</param>
        /// <param name="storedType">The type of objects that are stored in this member.</param>
        protected EditableStorageMemberBase(string name, IType storedType)
            : base(name)
        {
            this.StoredType = storedType;
            initDefaultAccessModifiers();
        }

        private void initDefaultAccessModifiers()
        {
            ReadAccess = AccessModifier.Private;
            WriteAccess = AccessModifier.Private;
        }

        /// <summary>
        /// Gets whether the member can read it's value.
        /// </summary>
        public virtual bool CanRead
        {
            get { return GetFunction != null; }
        }

        /// <summary>
        /// Gets whether the member can write it's value.
        /// </summary>
        public virtual bool CanWrite
        {
            get { return SetFunction != null; }
        }

        /// <summary>
        /// Gets or sets the function that retrieves the stored value from a given object.
        /// </summary>
        protected Func<object, object> GetFunction
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the function that sets the stored value for a given object reference.
        /// </summary>
        protected Action<object, object> SetFunction
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the type of values that this member stores.
        /// </summary>
        public IType StoredType
        {
            get;
            protected set;
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object" />.
        /// </returns>
        public override int GetHashCode()
        {
            int prime = 17;
            return unchecked(17989 * ((FullName.GetHashCode() * prime) *
                                      (CanRead .GetHashCode() * prime) *
                                      (CanWrite.GetHashCode() * prime)));
        }

        /// <summary>
        /// Determines if this <see cref="EditableStorageMemberBase"/> object equals the given <see cref="Object"/> object.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> object to compare with this object.</param>
        /// <returns>
        /// Returns true if this object object is equal to the obj object, otherwise false.
        /// </returns>
        public override bool Equals(object obj)
        {
            return obj is IStorageMember && Equals((IStorageMember)obj);
        }

        /// <summary>
        /// Determines if this <see cref="EditableStorageMemberBase"/> object equals the given <see cref="IStorageMember"/> object.
        /// </summary>
        /// <param name="other">The <see cref="IStorageMember"/> object to compare with this object.</param>
        /// <returns>
        /// Returns true if this object object is equal to the other object, otherwise false.
        /// </returns>
        public virtual bool Equals(IStorageMember other)
        {
            return other != null &&
                this.CanRead == other.CanRead &&
                this.CanWrite == other.CanWrite &&
                this.FullName.Equals(other.FullName);
        }

        /// <summary>
        /// Gets the value stored in the given object in this member.
        /// </summary>
        /// <param name="reference">The object that the value for this member should be retrieved from.</param>
        /// <returns>
        /// Returns the value stored in this member.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">The value cannot be retrieved from this member.</exception>
        /// <exception cref="System.ArgumentNullException">The reference cannot be null because this member is not static.</exception>
        public virtual object GetValue(object reference)
        {
            if (!CanRead)
            {
                throw new InvalidOperationException("The value cannot be retrieved from this member.");
            }
            if (!IsStatic && reference == null)
            {
                throw new ArgumentNullException("The reference cannot be null because this member is not static.");
            }
            return GetFunction(reference);
        }

        /// <summary>
        /// Sets the value stored in this member in the given object to the given value.
        /// </summary>
        /// <param name="reference">The object that the value for this member should be set for.</param>
        /// <param name="value">The value to set to.</param>
        /// <exception cref="System.InvalidOperationException">The value cannot be set for this member.</exception>
        /// <exception cref="System.ArgumentNullException">The reference cannot be null because this member is not static.</exception>
        public virtual void SetValue(object reference, object value)
        {
            if (!CanWrite)
            {
                throw new InvalidOperationException("The value cannot be set for this member.");
            }
            if (!IsStatic && reference == null)
            {
                throw new ArgumentNullException("The reference cannot be null because this member is not static.");
            }
            SetFunction(reference, value);
        }

        /// <summary>
        /// Gets or sets the access modifiers that are applied when reading from this member.
        /// </summary>
        public virtual AccessModifier ReadAccess
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the access modifiers that are applied when writing to this member.
        /// </summary>
        public virtual AccessModifier WriteAccess
        {
            get;
            set;
        }
    }
}
