using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReCode
{
    /// <summary>
    /// Defines a base class for type members that are editable.
    /// </summary>
    public abstract class EditableMemberBase : IMember
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditableMemberBase"/> class.
        /// </summary>
        /// <param name="info">The <see cref="System.Reflection.MemberInfo"/> object that contains information about the current member.</param>
        protected EditableMemberBase(MemberInfo info)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
            this.Name = info.Name;
            this.declaringType = new Lazy<IType>(() => info.DeclaringType.Edit());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditableMemberBase"/> class.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if the given name is null.</exception>
        protected EditableMemberBase(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            this.Name = name;
            declaringType = new Lazy<IType>(() => null);
        }

        /// <summary>
        /// Gets or sets the name of this member.
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or set whether this member is static.
        /// </summary>
        public virtual bool IsStatic
        {
            get;
            set;
        }

        private Lazy<IType> declaringType;

        /// <summary>
        /// Gets or sets the type that this member is declared in.
        /// </summary>
        public IType DeclaringType
        {
            get
            {
                return declaringType.Value;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                if (declaringType.Value != null)
                {
                    declaringType.Value.Members.Remove(this);
                }
                this.declaringType = new Lazy<IType>(() => value);
                declaringType.Value.Members.Add(this);
            }
        }

        /// <summary>
        /// Gets the full name of the member.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">The full name of the field cannot be retrieved because it is not contained by a type. (i.e. DeclaringType is null)</exception>
        public virtual string FullName
        {
            get
            {
                try
                {
                    return string.Format("{0}.{1}", DeclaringType.FullName, this.Name);
                }
                catch (NullReferenceException)
                {
                    throw new InvalidOperationException("The full name of the field cannot be retrieved because it is not contained by a type. (i.e. DeclaringType is null)");
                }
            }
        }
    }
}
