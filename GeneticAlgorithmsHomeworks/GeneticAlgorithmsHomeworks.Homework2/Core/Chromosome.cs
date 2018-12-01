using System.Collections.Generic;
using System.Text;
using GeneticAlgorithmsHomeworks.Core;

namespace GeneticAlgorithmsHomeworks.Homework2
{
    public class Chromosome : BinaryRepresentation
    {
        protected Chromosome(BinaryRepresentation representation) : base(representation.AsString())
        {
        }

        protected Chromosome(string representation) : base(representation)
        {
        }

        protected Chromosome(IEnumerable<CharBit> bits) : base(bits)
        {
        }

        public new static Chromosome Create(BinaryRepresentation representation)
        {
            return new Chromosome(representation);
        }

        public new static Chromosome Create(string representation)
        {
            return new Chromosome(representation);
        }

        public new static Chromosome Create(IEnumerable<CharBit> bits)
        {
            return new Chromosome(bits);
        }

        public Chromosome Mutate(Rate mutationRate)
        {
            var representation = new StringBuilder();
            foreach (var bit in Bits)
            {
                var mutationResult = bit;

                mutationRate.RunOnSuccessfulRandomPass(() =>
                {
                    mutationResult = bit.Negate();
                });
                representation.Append(mutationResult);
            }

            return new Chromosome(representation.ToString());
        }
    }
}