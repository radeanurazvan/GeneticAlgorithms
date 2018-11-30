using GeneticAlgorithmsHomeworks.Core;

namespace GeneticAlgorithmsHomeworks.Homework2
{
    public class Chromosome : BinaryRepresentation
    {
        protected Chromosome(string representation) : base(representation)
        {
        }

        public new static Chromosome Create(string representation)
        {
            var binaryRepresentation = BinaryRepresentation.Create(representation);

            return new Chromosome(binaryRepresentation);
        }
    }
}