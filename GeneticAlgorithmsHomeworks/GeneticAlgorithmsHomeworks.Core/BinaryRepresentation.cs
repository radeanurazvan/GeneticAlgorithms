using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithmsHomeworks.Core
{
    public class BinaryRepresentation : ValueObject
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

            Bits = representation.Select(CharBit.Create);
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

        public static BinaryRepresentation Create(IEnumerable<CharBit> bits)
        {
            return new BinaryRepresentation(bits);
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

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Bits;
        }
    }
}