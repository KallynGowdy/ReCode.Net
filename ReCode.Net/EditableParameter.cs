using Mono.Cecil;
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
    /// Defines a class for editable parameters.
    /// </summary>
    public class EditableParameter : EditableMemberBase, IParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditableParameter"/> class.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public EditableParameter(ParameterInfo parameter) : base(parameter.Name)
        {
            this.parameterType = new Lazy<IType>(() => TypeFactory.Instance.RetrieveInstanceForType(parameter.ParameterType));
            this.method = new Lazy<IMethod>(() => MethodFactory.Instance.RetrieveInstanceForMember(parameter.Member));
        }

        private Lazy<IType> parameterType;

        private Lazy<IMethod> method;

        /// <summary>
        /// Gets or sets the type of objects that this parameter accepts as input.
        /// </summary>
        public IType ParameterType
        {
            get
            {
                return parameterType.Value;
            }
            set
            {
                parameterType = new Lazy<IType>(() => value);
            }
        }

        
        /// <summary>
        /// Gets or sets the method that this parameter belongs to.
        /// </summary>
        public IMethod Method
        {
            get
            {
                return method.Value;
            }
            set
            {
                method = new Lazy<IMethod>(() => value);
            }
        }

        public ParameterDefinition CreateParameter(TypeDefinition typeDefinition)
        {
            return new ParameterDefinition(this.ParameterType.GetTypeReference(typeDefinition.Module))
            {
                Name = this.Name
            };
        }

        /// <summary>
        /// Gets the full name of the member.
        /// </summary>
        public override string FullName
        {
            get
            {
                if (Method != null)
                {
                    return string.Format("{0}-{1}", Method.FullName, this.Name);
                }
                else
                {
                    throw new InvalidOperationException("The full name of the parameter cannot be retrieved because it does not belong to a method. (i.e. Method is null)");
                }
            }
        }

        /// <summary>
        /// Determines if this <see cref="EditableParameter"/> object equals the given <see cref="IParameter"/> object.
        /// </summary>
        /// <param name="other">The <see cref="IParameter"/> object to compare with this object.</param>
        /// <returns>
        /// Returns true if this object object is equal to the other object, otherwise false.
        /// </returns>
        public bool Equals(IParameter other)
        {
            return other != null &&
                this.Name.Equals(other.Name) &&
                this.Method == other.Method &&
                this.ParameterType.Equals(other.ParameterType);
        }
    }
}
