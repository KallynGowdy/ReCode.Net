using System.Runtime.Remoting.Messaging;
using Mono.Cecil;
using Mono.Cecil.Cil;
using ReCode.Factories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ReCode.Net.Collections;
using MethodBody = Mono.Cecil.Cil.MethodBody;

namespace ReCode
{
    /// <summary>
    /// Defines a class for editable methods.
    /// </summary>
    public class EditableMethod : EditableMemberBase, IMethod
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="EditableMethod"/> class.
        /// </summary>
        /// <param name="method">The method.</param>
        public EditableMethod(MethodInfo method)
            : base(method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }
            Access = method.GetAccessModifiers();
            returnType = new Lazy<IType>(() => TypeFactory.Instance.RetrieveInstanceForType(method.ReturnType));
            parameters = new Lazy<DictionaryCollection<string, IParameter>>(() => new DictionaryCollection<string, IParameter>(p => p.Name, method.GetParameters().Select(p => ParameterFactory.Instance.RetrieveInstanceForParameter(p)).ToList()));
        }

        /// <summary>
        /// Invokes this method and returns the result.
        /// </summary>
        /// <param name="reference">A reference to the object that this method belongs to.</param>
        /// <param name="arguments">A list of arguments to provide the method with.</param>
        /// <returns>
        /// Returns the result of the method invocation.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public object Invoke(object reference, params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public MethodDefinition CreateMethod(TypeDefinition typeDefinition)
        {
            MethodDefinition m = new MethodDefinition(this.Name, this.GetMonoMethodAttributes(), new TypeReference(this.ReturnType.Namespace, this.ReturnType.Name, typeDefinition.Module, typeDefinition.Module));

            foreach (var p in this.Parameters.Values)
            {
                m.Parameters.Add(p.CreateParameter(typeDefinition));
            }


            m.Body = new MethodBody(m);
            return m;
        }

        /// <summary>
        /// Gets or sets the access modifier that apply to this object.
        /// </summary>
        public AccessModifier Access
        {
            get;
            set;
        }

        private Lazy<IType> returnType;

        /// <summary>
        /// Gets or sets the return type of this method.
        /// </summary>
        public IType ReturnType
        {
            get
            {
                return returnType.Value;
            }
            set
            {
                returnType = new Lazy<IType>(() => value);
            }
        }

        private Lazy<DictionaryCollection<string, IParameter>> parameters;

        /// <summary>
        /// Gets the dictionary of parameters that this method contains.
        /// </summary>
        public IDictionary<string, IParameter> Parameters
        {
            get
            {
                return parameters.Value;
            }
        }

        /// <summary>
        /// Determines if this <see cref="EditableMethod"/> object equals the given <see cref="IMethod"/> object.
        /// </summary>
        /// <param name="other">The <see cref="IMethod"/> object to compare with this object.</param>
        /// <returns>
        /// Returns true if this object object is equal to the other object, otherwise false.
        /// </returns>
        public bool Equals(IMethod other)
        {
            return other != null &&
                this.Name.Equals(other.Name) &&
                this.ReturnType == other.ReturnType &&
                this.Parameters.Values.SequenceEqual(other.Parameters.Values);
        }

        /// <summary>
        /// Determines if this <see cref="EditableMethod"/> object equals the given <see cref="Object"/> object.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> object to compare with this object.</param>
        /// <returns>
        /// Returns true if this object object is equal to the obj object, otherwise false.
        /// </returns>
        public override bool Equals(object obj)
        {
            return obj is IMethod && Equals((IMethod)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return unchecked(44123 *
                (this.FullName.GetHashCode() * 17) *
                (this.Access.GetHashCode() * 17));
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0} {1} {2}({3})", Access.ToNaturalString(), ReturnType, Name);
        }
    }
}
