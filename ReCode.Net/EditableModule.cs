using Mono.Cecil;
using ReCode.Factories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReCode
{
    /// <summary>
    /// Defines a class for a module that is editable.
    /// </summary>
    public class EditableModule : IModule
    {
        private Lazy<IAssembly> assembly;

        /// <summary>
        /// Gets the assembly that this module belongs to.
        /// </summary>
        public IAssembly Assembly
        {
            get
            {
                return assembly.Value;
            }
        }

        Lazy<IDictionary<string, IType>> types;

        /// <summary>
        /// Gets the collection of types that this module contains.
        /// </summary>
        public IDictionary<string, IType> Types
        {
            get
            {
                return types.Value;
            }
        }

        /// <summary>
        /// Gets the fully qualified name of the module.
        /// </summary>
        public string FullName
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditableModule" /> class.
        /// </summary>
        /// <param name="module">The module that this object represents.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if the given module is null.</exception>
        public EditableModule(Module module)
        {
            if (module == null)
            {
                throw new ArgumentNullException("module");
            }
            assembly = new Lazy<IAssembly>(() => module.Assembly.Edit());
            types = new Lazy<IDictionary<string, IType>>(() => new DictionaryCollection<string, IType>(t => t.Name, module.GetTypes().Select(t => t.Edit()).ToArray()));
            FullName = module.FullyQualifiedName;
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:EditableModule" />.
        /// </returns>
        public override int GetHashCode()
        {
            return unchecked(14723 * this.FullName.GetHashCode());
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return FullName;
        }

        /// <summary>
        /// Determines if this <see cref="EditableModule"/> object equals the given <see cref="Object"/> object.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> object to compare with this object.</param>
        /// <returns>
        /// Returns true if this object object is equal to the obj object, otherwise false.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is IModule)
            {
                return Equals((IModule)obj);
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// Determines if this <see cref="EditableModule"/> object equals the given <see cref="IModule"/> object.
        /// </summary>
        /// <param name="other">The <see cref="IModule"/> object to compare with this object.</param>
        /// <returns>
        /// Returns true if this object object is equal to the other object, otherwise false.
        /// </returns>
        public bool Equals(IModule other)
        {
            return other != null &&
                this.FullName.Equals(other.FullName);
        }
    }
}
