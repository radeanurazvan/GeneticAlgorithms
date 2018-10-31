using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithmsHomeworks.Core
{
    public static class StringExtensions
    {
        public static IEnumerable<string> ChunksOfSize(this string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }
    }
}