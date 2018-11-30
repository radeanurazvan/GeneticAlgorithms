using System;
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

            Value = representation;
        }

        public string Value { get; }

        public static BinaryRepresentation Create(string value)
        {
            return new BinaryRepresentation(value);
        }

        public static BinaryRepresentation FromDouble(double value)
        {
            var representation = new StringBuilder();
            foreach (var b in BitConverter.GetBytes(value))
            {
                representation.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
            }

            return new BinaryRepresentation(representation.ToString());
        }

        public static implicit operator string(BinaryRepresentation representation)
        {
            return representation.Value;
        }
    }
}