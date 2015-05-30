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
using System.Text;
using System.Threading.Tasks;

namespace ReCode.Factories
{
    /// <summary>
    /// Defines an interface for an object that provides the same objects to multiple calls with the same argument.
    /// </summary>
    /// <example>
    /// Objects of these types provide the same object for the same input. For example:
    /// <code>
    /// IReusedInstanceFactory{Type, IType} factory = TypeFactory.Instance; // Get the singleton instance
    /// 
    /// IType t1 = factory.GetInstance(typeof(int));
    /// IType t2 = factory.GetInstance(typeof(int));
    /// 
    /// Assert.AreSame(t1, t2); // The Assert passes
    /// </code>
    /// </example>
    /// <typeparam name="TArg">The type of object that is provided to the constructor.</typeparam>
    /// <typeparam name="TResult">The type of object that is created as the result.</typeparam>
    public interface IReusedInstanceFactory<in TArg, out TResult>
    {
        /// <summary>
        /// Gets the singleton instance that relates to the given argument.
        /// </summary>
        /// <param name="arg">The value used to initialize the result value.</param>
        /// <returns>Returns an object that is the same reference for each call with the same argument.</returns>
        TResult GetInstance(TArg arg);
    }
}
