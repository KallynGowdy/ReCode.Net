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
using System.Text;
using System.Threading.Tasks;

namespace ReCode.Factories
{
    /// <summary>
    /// Defines a class that provides a base implementation of IReusedInstanceFactory{TArg, TReturn}.
    /// </summary>
    /// <typeparam name="TArg">The type of values that determine which instance of a .</typeparam>
    /// <typeparam name="TReturn">The type of objects that are created and reused by this factory.</typeparam>
    public class ReusedInstanceFactoryBase<TArg, TReturn> : IReusedInstanceFactory<TArg, TReturn>
    {
        /// <summary>
        /// Gets the function that, given a TArg object, returns a new TReturn object.
        /// </summary>
        public Func<TArg, TReturn> Constructor
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets the dictionary of create objects that relate to given arguments.
        /// </summary>
        protected ConcurrentDictionary<TArg, Lazy<TReturn>> Instances
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReusedInstanceFactoryBase{TArg, TReturn}"/> class.
        /// </summary>
        /// <param name="constructor">The constructor to use for creating new instances of TReturn objects..</param>
        /// <exception cref="System.ArgumentNullException">Thrown if the given constructor is null.</exception>
        public ReusedInstanceFactoryBase(Func<TArg, TReturn> constructor)
        {
            if (constructor == null)
            {
                throw new ArgumentNullException("constructor");
            }
            this.Constructor = constructor;
            Instances = new ConcurrentDictionary<TArg, Lazy<TReturn>>();
        }

        /// <summary>
        /// Gets the singleton instance that relates to the given argument.
        /// </summary>
        /// <param name="arg">The value used to initialize the result value.</param>
        /// <returns>Returns an object that is the same reference for each call with the same argument.</returns>
        public virtual TReturn GetInstance(TArg arg)
        {
            Lazy<TReturn> instance;
            if (!Instances.TryGetValue(arg, out instance))
            {
                lock (Instances)
                {
                    if (!Instances.TryGetValue(arg, out instance))
                    {
                        instance = new Lazy<TReturn>(() => Constructor(arg));
                        Instances.TryAdd(arg, instance);
                    }
                }
            }
            return instance.Value;
        }
    }
}
