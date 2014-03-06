using System;
using System.Collections.Generic;

namespace XafWithAspNetCustomIdentity.Model._Configuration
{
    public static class ExportedTypesProvider
    {
        public static IEnumerable<Type> ExportedTypes
        {
            get
            {
                return new[]
                {
                    typeof(AspClaim),
                    typeof(AspRole),
                    typeof(AspUser),
                    typeof(AspUserLogin),
                };
            }
        }
    }
}