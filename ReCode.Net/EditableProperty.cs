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
using ReCode.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PropertyAttributes = Mono.Cecil.PropertyAttributes;

namespace ReCode
{
    /// <summary>
    /// Defines a class for an editable property.
    /// </summary>
    public class EditableProperty : EditableStorageMemberBase, IProperty
    {
        /// <summary>
        /// Gets or sets the access modifier that apply to this object.
        /// </summary>
        public AccessModifier Access
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditableProperty"/> class.
        /// </summary>
        /// <param name="property">The property that this member represents.</param>
        public EditableProperty(PropertyInfo property)
            : base(property)
        {
            this.getMethod = new Lazy<IMethod>(() => property.CanRead ? MethodFactory.Instance.RetrieveInstanceForMethod(property.GetMethod) : null);
            this.setMethod = new Lazy<IMethod>(() => property.CanWrite ? MethodFactory.Instance.RetrieveInstanceForMethod(property.SetMethod) : null);
            
        }

        private Lazy<IMethod> getMethod;

        private Lazy<IMethod> setMethod;

        /// <summary>
        /// Gets or sets the type of values that this property manipulates.
        /// </summary>
        public IType PropertyType
        {
            get
            {
                return base.StoredType;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                base.StoredType = value;
            }
        }

        public PropertyDefinition CreateProperty(TypeDefinition typeDefinition)
        {
            return new PropertyDefinition(this.Name, PropertyAttributes.None, PropertyType.GetTypeReference(typeDefinition.Module))
            {
                GetMethod = this.GetMethod != null ? this.GetMethod.CreateMethod(typeDefinition) : null,
                SetMethod = this.SetMethod != null ? this.SetMethod.CreateMethod(typeDefinition) : null
            };
        }

        /// <summary>
        /// Gets or sets the method that retrieves the values from this property.
        /// Null if this property does not contain a 'get' method.
        /// </summary>
        public IMethod GetMethod
        {
            get
            {
                return getMethod.Value;
            }
            set
            {
                getMethod = new Lazy<IMethod>(() => value);
                base.GetFunction = r => getMethod.Value.Invoke(r);
            }
        }

        /// <summary>
        /// Gets or sets the method that writes values to this property.
        /// Null if this property does not contain a 'set' method.
        /// </summary>
        public IMethod SetMethod
        {
            get
            {
                return setMethod.Value;
            }
            set
            {
                setMethod = new Lazy<IMethod>(() => value);
                base.SetFunction = (r, v) => setMethod.Value.Invoke(r, v);
            }
        }

        /// <summary>
        /// Determines if this <see cref="EditableProperty"/> object equals the given <see cref="IProperty"/> object.
        /// </summary>
        /// <param name="other">The <see cref="IProperty"/> object to compare with this object.</param>
        /// <returns>
        /// Returns true if this object object is equal to the other object, otherwise false.
        /// </returns>
        public bool Equals(IProperty other)
        {
            return other != null &&
                this.Name.Equals(other.Name) &&
                this.CanRead == other.CanRead &&
                this.CanWrite == other.CanWrite &&
                this.PropertyType.Equals(other.PropertyType);
        }

        /// <summary>
        /// Determines if this <see cref="EditableProperty"/> object equals the given <see cref="IStorageMember"/> object.
        /// </summary>
        /// <param name="other">The <see cref="IStorageMember"/> object to compare with this object.</param>
        /// <returns>
        /// Returns true if this object object is equal to the other object, otherwise false.
        /// </returns>
        public override bool Equals(IStorageMember other)
        {
            return other is IProperty && Equals((IProperty)other);
        }

        /// <summary>
        /// Determines if this <see cref="EditableProperty"/> object equals the given <see cref="Object"/> object.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> object to compare with this object.</param>
        /// <returns>
        /// Returns true if this object object is equal to the obj object, otherwise false.
        /// </returns>
        public override bool Equals(object obj)
        {
            return obj is IProperty && Equals((IProperty)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return unchecked(53161 *
                (this.Name.GetHashCode() * 17) *
                (this.PropertyType.GetHashCode() * 17) *
                (this.CanRead.GetHashCode() * 17) *
                (this.CanWrite.GetHashCode() * 17));
        }
    }
}
