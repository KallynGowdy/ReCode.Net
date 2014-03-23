using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCode
{
    /// <summary>
    /// Defines a static class that contains extension methods for generic objects.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Creates a new <see cref="System.Lazy{T}"/> object that wraps the current value.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="value">The value to wrap as lazy.</param>
        /// <returns>Returns a new <see cref="System.Lazy{T}"/> object that wraps the current value.</returns>
        //public static Lazy<T> Lazy<T>(this T value)
        //{
        //    return new Lazy<T>(() => value);
        //}
    }
}
