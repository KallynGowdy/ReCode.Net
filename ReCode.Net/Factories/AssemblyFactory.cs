using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReCode.Factories
{
    /// <summary>
    /// Defines a factory that retrieves <see cref="ReCode.IAssembly"/> objects.
    /// </summary>
    public class AssemblyFactory : ReusedInstanceFactoryBase<Assembly, IAssembly>
    {
        private static readonly Lazy<AssemblyFactory> lazy = new Lazy<AssemblyFactory>(() => new AssemblyFactory());

        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static AssemblyFactory Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        protected AssemblyFactory() : base(a => new EditableAssembly(a))
        {
        }

        /// <summary>
        /// Gets a <see cref="ReCode.IAssembly"/> object that represents the given type.
        /// </summary>
        /// <param name="assembly">The assembly that the instance should be retrieved for.</param>
        /// <returns>Returns an <see cref="ReCode.IAssembly"/> object that represents the given assembly.</returns>
        public IAssembly RetrieveInstanceForAssembly(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }
            return base.GetInstance(assembly);
        }

        /// <summary>
        /// Gets the singleton instance that relates to the given argument.
        /// </summary>
        /// <param name="arg">The value used to initialize the result value.</param>
        /// <returns>Returns an object that is the same reference for each call with the same argument.</returns>
        public override IAssembly GetInstance(Assembly arg)
        {
            if (arg == null)
            {
                throw new ArgumentNullException("arg");
            }
            return base.GetInstance(arg);
        }
    }
}
