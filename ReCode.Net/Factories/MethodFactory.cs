using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReCode.Factories
{
    /// <summary>
    /// Defines a factory for IMethod objects.
    /// </summary>
    public class MethodFactory : ReusedInstanceFactoryBase<MemberInfo, IMethod>
    {
        private static readonly Lazy<MethodFactory> lazy = new Lazy<MethodFactory>(() => new MethodFactory());

        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static MethodFactory Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        protected MethodFactory()
            : base(m =>
                {
                    if (m.MemberType == MemberTypes.Method)
                    {
                        return new EditableMethod((MethodInfo)m);
                    }
                    else if (m.MemberType == MemberTypes.Constructor)
                    {
                        return null;//new EditableMethod((ConstructorInfo)m);
                    }
                    throw new ArgumentException("The given member must be either a method or a constructor");
                })
        {
        }

        /// <summary>
        /// Gets a <see cref="ReCode.IMethod"/> object that represents the given method.
        /// </summary>
        /// <param name="method">The method that the instance should be retrieved for.</param>
        /// <returns>Returns an <see cref="ReCode.IMethod"/> object that represents the given method.</returns>
        public IMethod RetrieveInstanceForMethod(MethodInfo method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("assembly");
            }
            return base.GetInstance(method);
        }

        /// <summary>
        /// Gets a <see cref="ReCode.IMethod"/> object that represents the given constructor.
        /// </summary>
        /// <param name="constructor">The constructor that the instance should be retrieved for.</param>
        /// <returns>Returns an <see cref="ReCode.IMethod"/> object that represents the given constructor.</returns>
        public IMethod RetrieveInstanceForConstructor(ConstructorInfo constructor)
        {
            if (constructor == null)
            {
                throw new ArgumentNullException("constructor");
            }
            return base.GetInstance(constructor);
        }

        /// <summary>
        /// Gets a <see cref="ReCode.IMethod"/> object that represents the given member.
        /// </summary>
        /// <param name="member">The member that the instance should be retrieved for.</param>
        /// <returns>Returns an <see cref="ReCode.IMethod"/> object that represents the given member.</returns>
        /// <exception cref="System.ArgumentException">Thrown if the given member is not a method or constructor.</exception>
        public IMethod RetrieveInstanceForMember(MemberInfo member)
        {
            if (member == null)
            {
                throw new ArgumentNullException("member");
            }
            return base.GetInstance(member);
        }
    }
}
