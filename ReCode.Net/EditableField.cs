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
    public class EditableField : IField
    {
        /// <summary>
        /// Gets or sets the type of the field.
        /// </summary>
        public IType FieldType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets whether the member can read it's value.
        /// </summary>
        public bool CanRead
        {
            get { return true; }
        }

        /// <summary>
        /// Gets whether the member can write it's value.
        /// </summary>
        public bool CanWrite
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the type of values that this member stores.
        /// </summary>
        public IType StoredType
        {
            get { return FieldType; }
        }

        /// <summary>
        /// Gets or sets the name of the member.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the full name of the member.
        /// </summary>
        public string FullName
        {
            get
            {
                return string.Format("{0}.{1}", declaringType.FullName, this.Name);
            }
        }

        private IType declaringType;

        /// <summary>
        /// Gets or sets the type that declares this member.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Thrown if the given value is null.</exception>
        public IType DeclaringType
        {
            get
            {
                return declaringType;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                declaringType.Fields.Remove(this.Name);
                declaringType = value;
                value.Fields.Add(this.Name, this);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditableField"/> class.
        /// </summary>
        /// <param name="field">The field.</param>
        public EditableField(FieldInfo field)
        {
            if (field == null)
            {
                throw new ArgumentNullException("field");
            }
            this.Name = field.Name;
            this.FieldType = TypeFactory.Instance.RetrieveInstanceForType(field.FieldType);
            this.declaringType = TypeFactory.Instance.RetrieveInstanceForType(field.DeclaringType);
            this.Access = field.GetAccessModifiers();
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
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            else if (fieldType == null)
            {
                throw new ArgumentNullException("fieldType");
            }
            this.Name = name;
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
        public bool Equals(IStorageMember other)
        {
            if (other is IField)
            {
                return Equals((IField)other);
            }
            return false;
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
            if (obj is IField)
            {
                return Equals((IField)obj);
            }
            else if (obj is IStorageMember)
            {
                return Equals((IStorageMember)obj);
            }
            return base.Equals(obj);
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
