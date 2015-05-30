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
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReCode
{
    /// <summary>
    /// Defines a class for an assembly that is editable.
    /// </summary>
    public class EditableAssembly : IAssembly
    {
        /// <summary>
        /// Gets or sets the name of the assembly.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the full name of the assembly.
        /// </summary>
        public string FullName
        {
            get
            {
                return Name;
            }
        }

        /// <summary>
        /// Gets the location that this assembly lives in on disk.
        /// </summary>
        public string Location
        {
            get;
            private set;
        }

        private Lazy<ICollection<IModule>> modules;

        /// <summary>
        /// Gets or sets the collection of modules that the assembly contains.
        /// </summary>
        public ICollection<IModule> Modules
        {
            get
            {
                return modules.Value;
            }
        }

        private Lazy<DictionaryCollection<string, IType>> types;

        /// <summary>
        /// Gets the collection of types that belong to this assembly.
        /// </summary>
        public IDictionary<string, IType> Types
        {
            get
            {
                return types.Value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditableAssembly" /> class.
        /// </summary>
        /// <param name="assembly">The assembly that should be represented.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if the given assembly is null.</exception>
        public EditableAssembly(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }
            Name = assembly.GetName().FullName;
            Location = assembly.Location;
            modules = new Lazy<ICollection<IModule>>(() => assembly.Modules.Select(m => ModuleFactory.Instance.RetrieveInstanceForModule(m)).ToList());
            types = new Lazy<DictionaryCollection<string, IType>>(() => new DictionaryCollection<string, IType>(t => t.Name, new MergedCollection<IType>(Modules.Select(m => m.Types.Values).ToArray())));
        }

        /// <summary>
        /// Determines if this <see cref="EditableAssembly"/> object equals the given <see cref="Object"/> object.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> object to compare with this object.</param>
        /// <returns>
        /// Returns true if this object object is equal to the obj object, otherwise false.
        /// </returns>
        public override bool Equals(object obj)
        {
            return obj is IAssembly && Equals((IAssembly)obj);
        }

        /// <summary>
        /// Determines if this <see cref="EditableAssembly"/> object equals the given <see cref="IAssembly"/> object.
        /// </summary>
        /// <param name="other">The <see cref="IAssembly"/> object to compare with this object.</param>
        /// <returns>
        /// Returns true if this object object is equal to the other object, otherwise false.
        /// </returns>
        public bool Equals(IAssembly other)
        {
            return other != null &&
                this.FullName.Equals(other.FullName);
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object" />.
        /// </returns>
        public override int GetHashCode()
        {
            return unchecked(82207 * this.FullName.GetHashCode());
        }

    }
}
