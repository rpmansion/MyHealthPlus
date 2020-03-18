using System.Collections.Generic;
using System.Linq;

namespace MyHealthPlus.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> value)
        {
            return value != null && value.Any();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> value)
        {
            return value == null || !value.Any();
        }
    }
}