using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace ReCode
{
    public static class MethodExtensions
    {
        public static MethodAttributes GetMonoMethodAttributes(this IMethod method)
        {
            MethodAttributes attributes = method.Access.ToMethodAttributes();

            if (method.IsStatic)
            {
                attributes |= MethodAttributes.Static;
            }

            return attributes;
        }

    }
}
