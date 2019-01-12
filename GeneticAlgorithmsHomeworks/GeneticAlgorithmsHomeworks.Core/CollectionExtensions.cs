namespace GeneticAlgorithmsHomeworks.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class CollectionExtensions
    {
        public static IEnumerable<T> RemoveWhere<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            var list = collection.ToList();

            foreach (var item in collection)
            {
                if (predicate(item))
                {
                    list.Remove(item);
                }
            }

            return list;
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
        {
            T[] elements = source.ToArray();
            for (int i = elements.Length - 1; i > 0; i--)
            {
                int swapIndex = rng.Next(i + 1);
                T tmp = elements[i];
                elements[i] = elements[swapIndex];
                elements[swapIndex] = tmp;
            }

            foreach (T element in elements)
            {
                yield return element;
            }
        }
    }
}