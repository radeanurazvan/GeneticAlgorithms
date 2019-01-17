using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmsHomeworks.Core;
using GeneticAlgorithmsHomeworks.Genetic;

namespace GeneticAlgorithmsHomeworks.Homework2
{
    public class Crossover : AbstractCrossover<Chromosome, BinaryRepresentation>
    {
        protected override (Chromosome, Chromosome) DoCrossoverCore(
            int cutPoint,
            (IEnumerable<BinaryRepresentation> leftCut, IEnumerable<BinaryRepresentation> rightCut) first,
            (IEnumerable<BinaryRepresentation> leftCut, IEnumerable<BinaryRepresentation> rightCut) second)
        {
            var firstOffspring = Chromosome.Create(first.leftCut.Concat(second.rightCut));
            var secondOffspring = Chromosome.Create(second.leftCut.Concat(first.rightCut));

            return (firstOffspring, secondOffspring);
        }
    }
}