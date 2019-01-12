namespace GeneticAlgorithmsHomeworks.Homework2
{
    using System.Collections.Generic;
    using System.Linq;

    using GeneticAlgorithmsHomeworks.Core;
    using GeneticAlgorithmsHomeworks.Genetic;

    public class Crossover : AbstractCrossover<Chromosome, BinaryRepresentation>
    {
        protected override (Chromosome, Chromosome) DoCrossoverCore(
            (IEnumerable<BinaryRepresentation> leftCut, IEnumerable<BinaryRepresentation> rightCut) first,
            (IEnumerable<BinaryRepresentation> leftCut, IEnumerable<BinaryRepresentation> rightCut) second)
        {
            var firstOffspring = Chromosome.Create(first.leftCut.Concat(second.rightCut));
            var secondOffspring = Chromosome.Create(second.leftCut.Concat(first.rightCut));

            return (firstOffspring, secondOffspring);
        }
    }
}