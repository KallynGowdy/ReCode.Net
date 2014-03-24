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

using ReCode.Factories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ReCode
{
    /// <summary>
    /// Defines a class for a type that is editable.
    /// </summary>
    public class EditableType : IType
    {

        private static BindingFlags memberFlags
        {
            get
            {
                return BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditableType"/> class.
        /// </summary>
        /// <param name="type">The type to edit.</param>
        public EditableType(Type type)
        {
            fields = new Lazy<DictionaryCollection<string, IField>>
            (
                () => new DictionaryCollection<string, IField>(f => f.Name, new FilteredCollection<IField, IMember>(Members))
            );

            members = new Lazy<ICollection<IMember>>
            (
                () =>
                {
                    List<IMember> mems = new List<IMember>();
                    mems.AddRange(type.GetFields(memberFlags).Select(f => FieldFactory.Instance.RetrieveInstanceForField(f)));
                    return mems;
                }
            );

            module = new Lazy<IModule>(() => ModuleFactory.Instance.RetrieveInstanceForModule(type.Module));

            Name = type.Name;
            FullName = type.FullName;
        }

        private Lazy<ICollection<IMember>> members;

        private Lazy<DictionaryCollection<string, IField>> fields;

        /// <summary>
        /// Gets or sets the collection of fields that this type contains.
        /// </summary>
        public IDictionary<string, IField> Fields
        {
            get
            {
                return fields.Value;
            }
        }

        /// <summary>
        /// Gets or sets the collection of members that this type contains.
        /// </summary>
        public ICollection<IMember> Members
        {
            get
            {
                return members.Value;
            }
        }


        private Lazy<IModule> module;

        /// <summary>
        /// Gets or sets the module that this type lives in.
        /// </summary>
        public IModule Module
        {
            get
            {
                return module.Value;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                Module.Types.Remove(this.Name);
                module = new Lazy<IModule>(() => value);
                module.Value.Types.Add(this.Name, this);
            }
        }

        /// <summary>
        /// Gets or sets the name of this type.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the full name of the type.
        /// </summary>
        public string FullName
        {
            get;
            private set;
        }
    }
}
