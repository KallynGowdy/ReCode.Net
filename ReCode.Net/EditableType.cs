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
        Type internalType;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditableType"/> class.
        /// </summary>
        /// <param name="type">The type to edit.</param>
        public EditableType(Type type)
        {
            internalType = type;
            Name = type.Name;
            FullName = type.FullName;
        }

        private void setFields(ICollection<IMember> members)
        {
            fields = new ObservableDictionary<string, IField>(new DictionaryCollection<string, IField>(f => f.Name, new FilteredCollection<IField, IMember>(members)));
            fields.PropertyChanged += fields_PropertyChanged;
        }

        private DictionaryCollection<string, IMember> members = new DictionaryCollection<string, IMember>(v => v.Name);

        private ObservableDictionary<string, IField> fields;

        /// <summary>
        /// Gets or sets the collection of fields that this type contains.
        /// </summary>
        public IDictionary<string, IField> Fields
        {
            get
            {
                if (fields == null)
                {
                    members.AddRange(internalType
                        .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                        .Select(f => FieldFactory.Instance.RetrieveInstanceForField(f)));

                    setFields(members.Values);
                }
                return fields;
            }
        }

        /// <summary>
        /// Gets or sets the collection of members that this type contains.
        /// </summary>
        public ICollection<IMember> Members
        {
            get
            {
                return members;
            }
            set
            {
                members = new DictionaryCollection<string, IMember>(v => v.Name);
                setFields(members);
            }
        }

        void fields_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            fields[e.PropertyName].DeclaringType = this;
        }

        private IModule module;

        /// <summary>
        /// Gets or sets the module that this type lives in.
        /// </summary>
        public IModule Module
        {
            get
            {
                if (module == null)
                {
                    module = ModuleFactory.Instance.RetrieveInstanceForModule(internalType.Module);
                }
                return module;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                Module.Types.Remove(this.Name);
                module = value;
                module.Types.Add(this.Name, this);
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
