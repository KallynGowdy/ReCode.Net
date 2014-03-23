using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Gets or sets the name of this member.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        private IType declaringType;

        /// <summary>
        /// Gets or sets the type that this member is declared in.
        /// </summary>
        public IType DeclaringType
        {
            get
            {
                return declaringType;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                declaringType.Members.Remove(this);
                this.declaringType = value;
                declaringType.Members.Add(this);
            }
        }

        /// <summary>
        /// Gets the full name of the member.
        /// </summary>
        public string FullName
        {
            get { return string.Format("{0}.{1}", declaringType, Name); }
        }
    }
}
