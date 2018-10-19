using System;

namespace GeneticAlgorithmsHomeworks.Function
{
    public class DimensionDefinition
    {
        public DimensionDefinition(int value)
        {
            if (value < 1)
            {
                throw new ArgumentException("Dimension definition must be greater than 1");
            }

            Value = value;
        }

        public int Value { get; }

        public static implicit operator int(DimensionDefinition dim)
        {
            return dim.Value;
        }
    }
}