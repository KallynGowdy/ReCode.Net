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
    /// Defines a class for a field that is editable.
    /// </summary>
    public class EditableField : EditableStorageMemberBase, IField
    {
        /// <summary>
        /// Gets or sets the type of the field.
        /// </summary>
        public IType FieldType
        {
            get
            {
                return StoredType;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                StoredType = value;
            }
        }

        /// <summary>
        /// Gets whether the member can read it's value.
        /// </summary>
        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets whether the member can write it's value.
        /// </summary>
        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditableField"/> class.
        /// </summary>
        /// <param name="field">The field.</param>
        public EditableField(FieldInfo field)
            : base(field)
        {
            if (field == null)
            {
                throw new ArgumentNullException("field");
            }
            this.FieldType = TypeFactory.Instance.RetrieveInstanceForType(field.FieldType);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditableField"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="fieldType">Type of the field.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if the given name or fieldType is null.
        /// </exception>
        public EditableField(string name, IType fieldType)
            : base(name, fieldType)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            else if (fieldType == null)
            {
                throw new ArgumentNullException("fieldType");
            }
            this.FieldType = fieldType;
            Access = AccessModifier.Public;
        }

        /// <summary>
        /// Determines if this <see cref="EditableField"/> object equals the given <see cref="IStorageMember"/> object.
        /// </summary>
        /// <param name="other">The <see cref="IStorageMember"/> object to compare with this object.</param>
        /// <returns>
        /// Returns true if this object object is equal to the other object, otherwise false.
        /// </returns>
        public override bool Equals(IStorageMember other)
        {
            return other is IField && Equals((IField)other);
        }

        /// <summary>
        /// Gets or sets the access modifier that applies to this object.
        /// </summary>
        public AccessModifier Access
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the access modifiers that are applied when reading from this member.
        /// </summary>
        public override AccessModifier ReadAccess
        {
            get
            {
                return Access;
            }
            set
            {
                Access = value;
            }
        }

        /// <summary>
        /// Gets or sets the access modifiers that are applied when writing to this member.
        /// </summary>
        public override AccessModifier WriteAccess
        {
            get
            {
                return Access;
            }
            set
            {
                Access = value;
            }
        }

        /// <summary>
        /// Determines if this <see cref="EditableField"/> object equals the given <see cref="IField"/> object.
        /// </summary>
        /// <param name="other">The <see cref="IField"/> object to compare with this object.</param>
        /// <returns>
        /// Returns true if this object object is equal to the other object, otherwise false.
        /// </returns>
        public bool Equals(IField other)
        {
            return other != null &&
                other.FullName.Equals(this.FullName);
        }

        /// <summary>
        /// Determines if this <see cref="EditableField"/> object equals the given <see cref="Object"/> object.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> object to compare with this object.</param>
        /// <returns>
        /// Returns true if this object object is equal to the obj object, otherwise false.
        /// </returns>
        public override bool Equals(object obj)
        {
            return obj is IField && Equals((IField)obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object" />.
        /// </returns>
        public override int GetHashCode()
        {
            return unchecked(57973 * FullName.GetHashCode());
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0} {1}", FieldType, Name);
        }
    }
}
