using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReCode.Factories
{
    /// <summary>
    /// Defines a factory for IParameter objects.
    /// </summary>
    public class ParameterFactory : ReusedInstanceFactoryBase<ParameterInfo, IParameter>
    {
        private static readonly Lazy<ParameterFactory> lazy = new Lazy<ParameterFactory>(() => new ParameterFactory());

        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static ParameterFactory Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        protected ParameterFactory()
            : base(m => new EditableParameter(m))
        {
        }

        /// <summary>
        /// Gets a <see cref="ReCode.IParameter"/> object that represents the given type.
        /// </summary>
        /// <param name="parameter">The parameter that the instance should be retrieved for.</param>
        /// <returns>Returns an <see cref="ReCode.IParameter"/> object that represents the given parameter.</returns>
        public IParameter RetrieveInstanceForParameter(ParameterInfo parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException("assembly");
            }
            return base.GetInstance(parameter);
        }

        /// <summary>
        /// Gets the singleton instance that relates to the given argument.
        /// </summary>
        /// <param name="arg">The value used to initialize the result value.</param>
        /// <returns>Returns an object that is the same reference for each call with the same argument.</returns>
        public override IParameter GetInstance(ParameterInfo arg)
        {
            if (arg == null)
            {
                throw new ArgumentNullException("arg");
            }
            return base.GetInstance(arg);
        }
    }
}
