using System.Collections.Generic;
using System.Linq;

namespace HunterAllen.Utility
{
    public static class EnumerableExtensions
    {
        public static T Random<T>(this IEnumerable<T> source)
        {
            return source.ElementAt(UnityEngine.Random.Range(0, source.Count()));            
        }
        public static IEnumerable<T> RemoveIndexes<T>(this IEnumerable<T> source, params int[] indexes)
        {
            var list = source.ToList();
            for (int i = indexes.Length - 1; i >= 0; i--)
            {
                list.RemoveAt(indexes[i]);
            }
            return list.AsEnumerable();
        }
        public static IEnumerable<T> RemoveValues<T>(this IEnumerable<T> source, params T[] values)
        {
            var list = source.ToList();
            for (int i = values.Length - 1; i >= 0; i--)
            {
                list.Remove(values[i]);
            }
            return list.AsEnumerable();
        }
    }
}
