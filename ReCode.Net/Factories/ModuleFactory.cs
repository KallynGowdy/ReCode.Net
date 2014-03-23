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
    /// Defines a factory that retrieves <see cref="ReCode.IField"/> objects.
    /// </summary>
    public class ModuleFactory : ReusedInstanceFactoryBase<Module, IModule>
    {
        private static readonly Lazy<ModuleFactory> lazy = new Lazy<ModuleFactory>(() => new ModuleFactory());

        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static ModuleFactory Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        protected ModuleFactory()
            : base(m => new EditableModule(m))
        {
        }

        /// <summary>
        /// Gets a <see cref="ReCode.IModule"/> object that represents the given module.
        /// </summary>
        /// <param name="module">The module that the instance should be retrieved for.</param>
        /// <returns>Returns an <see cref="ReCode.IModule"/> object that represents the given module.</returns>
        public IModule RetrieveInstanceForModule(Module module)
        {
            if (module == null)
            {
                throw new ArgumentNullException("module");
            }
            return base.GetInstance(module);
        }

        /// <summary>
        /// Gets the singleton instance that relates to the given argument.
        /// </summary>
        /// <param name="arg">The value used to initialize the result value.</param>
        /// <returns>Returns an object that is the same reference for each call with the same argument.</returns>
        public override IModule GetInstance(Module arg)
        {
            if (arg == null)
            {
                throw new ArgumentNullException("arg");
            }
            return base.GetInstance(arg);
        }
    }
}
