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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReCode.Factories
{
    /// <summary>
    /// Defines a factory that retrieves <see cref="ReCode.IProperty"/> objects.
    /// </summary>
    public class PropertyFactory : ReusedInstanceFactoryBase<PropertyInfo, IProperty>
    {
        private static readonly Lazy<PropertyFactory> lazy = new Lazy<PropertyFactory>(() => new PropertyFactory());

        /// <summary>
        /// Gets the singleton instance of this <see cref="ReCode.Factories.PropertyFactory"/>.
        /// </summary>
        public static PropertyFactory Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyFactory"/> class.
        /// </summary>
        protected PropertyFactory()
            : base(p => new EditableProperty(p))
        {

        }

        /// <summary>
        /// Gets a <see cref="ReCode.IAssembly"/> object that represents the given type.
        /// </summary>
        /// <param name="assembly">The assembly that the instance should be retrieved for.</param>
        /// <returns>Returns an <see cref="ReCode.IAssembly"/> object that represents the given assembly.</returns>
        public IProperty RetrieveInstanceForProperty(PropertyInfo property)
        {
            if (property == null)
            {
                throw new ArgumentNullException("property");
            }
            return base.GetInstance(property);
        }

        /// <summary>
        /// Gets the singleton instance that relates to the given argument.
        /// </summary>
        /// <param name="arg">The value used to initialize the result value.</param>
        /// <returns>Returns an object that is the same reference for each call with the same argument.</returns>
        public override IProperty GetInstance(PropertyInfo arg)
        {
            if (arg == null)
            {
                throw new ArgumentNullException("property");
            }
            return base.GetInstance(arg);
        }
    }
}
