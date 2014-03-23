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
    /// Defines a factory that retrieves <see cref="ReCode.IField"/> objects.
    /// </summary>
    public class FieldFactory : ReusedInstanceFactoryBase<FieldInfo, IField>
    {
        private static readonly Lazy<FieldFactory> lazy = new Lazy<FieldFactory>(() => new FieldFactory());

        /// <summary>
        /// Gets the singleton instance of the FieldFactory.
        /// </summary>
        public static FieldFactory Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldFactory"/> class.
        /// </summary>
        protected FieldFactory() : base(f => new EditableField(f))
        {
        }

        /// <summary>
        /// Gets a <see cref="ReCode.IField"/> object that relates to the given field.
        /// </summary>
        /// <param name="field">The <see cref="System.Reflection.FieldInfo"/> object that should be represented by the returned value.</param>
        /// <returns>Returns a <see cref="ReCode.IField"/> object that relates to the given field.</returns>
        public IField RetrieveInstanceForField(FieldInfo field)
        {
            if (field == null)
            {
                throw new ArgumentNullException("field");
            }
            return base.GetInstance(field);
        }

        /// <summary>
        /// Gets the singleton instance that relates to the given argument.
        /// </summary>
        /// <param name="arg">The value used to initialize the result value.</param>
        /// <returns>Returns an object that is the same reference for each call with the same argument.</returns>
        public override IField GetInstance(FieldInfo arg)
        {
            if (arg == null)
            {
                throw new ArgumentNullException("arg");
            }
            return base.GetInstance(arg);
        }
    }
}
