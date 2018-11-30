using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithmsHomeworks.Core
{
    public class BinaryRepresentation
    {
        protected BinaryRepresentation(string representation)
        {
            if (string.IsNullOrWhiteSpace(representation))
            {
                throw new ArgumentException("Binary representation cannot be empty!");
            }

            var numberOfZero = representation.Count(x => x == '0');
            var numberOfOne = representation.Count(x => x == '1');

            if (numberOfOne + numberOfZero != representation.Length)
            {
                throw new InvalidOperationException("Invalid binary representation!");
            }

            Bits = representation.Select(x => CharBit.Create(x));
        }

        protected BinaryRepresentation(IEnumerable<CharBit> bits)
        {
            Bits = bits ?? throw new InvalidOperationException("Bits cannot be null!");
        }

        public IEnumerable<CharBit> Bits { get; }

        public static BinaryRepresentation Create(string value)
        {
            return new BinaryRepresentation(value);
        }

        public string AsString()
        {
            var builder = new StringBuilder();

            foreach (var bit in Bits)
            {
                builder = builder.Append(bit);
            }

            return builder.ToString();
        }
    }
}