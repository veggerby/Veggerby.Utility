using System;
using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Utility.Extensions
{
    public static class UtilityExtensions
    {
        public static bool In<T>(this T value, params T[] values)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            return values.Contains(value);
        }

        public static T OneOrDefault<T>(this IEnumerable<T> values)
        {
            var items = values.Take(2);
            return items.Count() <= 1 ? items.SingleOrDefault() : default(T);
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> values, Action<T> action)
        {
            foreach (var value in values)
            {
                action(value);
                yield return value;
            }
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> values, Action<T, int> action)
        {
            int i = 0;
            foreach (var value in values)
            {
                action(value, i++);
                yield return value;
            }
        }

        public static IEnumerable<T> SkipLastDefault<T>(this IEnumerable<T> values)
        {
            return values
                .Reverse()
                .SkipWhile(x => x.Equals<T>(default(T)))
                .Reverse();
        }

        public static bool Equals<T>(this T value1, T value2)
        {
            return EqualityComparer<T>.Default.Equals(value1, value2);
        }

        public static bool AllNotDefault<T>(this IEnumerable<T> values)
        {
            return values.All(x => !x.Equals<T>(default(T)));
        }

        public static bool AnyNotDefault<T>(this IEnumerable<T> values)
        {
            return values.Any(x => !x.Equals<T>(default(T)));
        }

        public static IEnumerable<T> Append<T>(this IEnumerable<T> values, params T[] appendValues)
        {
            return values.Concat(appendValues);
        }

        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> values, params T[] prependValues)
        {
            return prependValues.Concat(values);
        }

        public static int IndexOf<TItem>(this IEnumerable<TItem> list, IEnumerable<TItem> subList, int offset = 0)
        {
            // source http://en.wikipedia.org/wiki/Knuth-Morris-Pratt_algorithm
            // alternative algorithms http://en.wikipedia.org/wiki/Boyer-Moore_string_search_algorithm, http://en.wikipedia.org/wiki/Rabin-Karp_string_search_algorithm
            var S = list.Skip(offset).ToList();
            var W = subList.ToList();

            var m = 0;
            var i = 0;
            var T = new int[W.Count()];

            // build T
            var pos = 2;
            var cnd = 0;
            T[0] = -1;
            T[1] = 0;

            while (pos < W.Count())
            {
                if (W.ElementAt(pos - 1).Equals(W.ElementAt(cnd)))
                {
                    cnd++;
                    T[pos] = cnd;
                    pos++;
                }
                else if (cnd > 0)
                {
                    cnd = T[cnd];
                }
                else
                {
                    T[pos] = 0;
                    pos++;
                }
            }

            while (m + i < S.Count())
            {
                if (W.ElementAt(i).Equals(S.ElementAt(m + i)))
                {
                    if (i == W.Count() - 1)
                    {
                        return m + offset;
                    }

                    i++;
                }
                else
                {
                    m += i - T[i];
                    i = T[i] > -1 ? T[i] : 0;
                }
            }

            return -1;
        }
    }
}