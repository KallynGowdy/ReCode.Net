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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReCode.Factories
{
    /// <summary>
    /// Defines a factory that creates new <see cref="ReCode.IType"/> objects.
    /// </summary>
    public class TypeFactory : ReusedInstanceFactoryBase<Type, IType>
    {
        private static readonly Lazy<TypeFactory> lazy = new Lazy<TypeFactory>(() => new TypeFactory());

        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static TypeFactory Instance
        {
            get { return lazy.Value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeFactory"/> class.
        /// </summary>
        protected TypeFactory() : base(t => new EditableType(t))
        {
            //foreach (Type t in typeof(int).Assembly.GetTypes().Concat(Assembly.GetExecutingAssembly().GetTypes()))
            //{
            //    Instances.TryAdd(t, new Lazy<IType>(() => Constructor(t)));
            //}
        }

        /// <summary>
        /// Gets a <see cref="ReCode.IType"/> object that represents the given type.
        /// </summary>
        /// <param name="type">The type that the instance should be retrieved for.</param>
        /// <returns>Returns an <see cref="ReCode.IType"/> object that represents the given type.</returns>
        public virtual IType RetrieveInstanceForType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            return base.GetInstance(type);
        }

        /// <summary>
        /// Gets the singleton instance that relates to the given argument.
        /// </summary>
        /// <param name="arg">The value used to initialize the result value.</param>
        /// <returns>Returns an object that is the same reference for each call with the same argument.</returns>
        public override IType GetInstance(Type arg)
        {
            if (arg == null)
            {
                throw new ArgumentNullException("arg");
            }
            return base.GetInstance(arg);
        }
    }
}
