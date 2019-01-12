namespace GeneticAlgorithmsHomeworks.Homework3
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using GeneticAlgorithmsHomeworks.Core;
    using GeneticAlgorithmsHomeworks.Genetic;

    public sealed class TspCrossover : AbstractCrossover<TspChromosome, City>
    {
        protected override (TspChromosome, TspChromosome) DoCrossoverCore(
            (IEnumerable<City> leftCut, IEnumerable<City> rightCut) first,
            (IEnumerable<City> leftCut, IEnumerable<City> rightCut) second)
        {
            var firstParent = new TspChromosome(first.leftCut.Concat(first.rightCut));
            var secondParent = new TspChromosome(second.leftCut.Concat(second.rightCut));

            var firstOffspring = this.Cross(secondParent, first.rightCut);
            var secondOffspring = this.Cross(firstParent, second.rightCut);

            return (firstOffspring, secondOffspring);
        }

        private TspChromosome Cross(TspChromosome subject, IEnumerable<City> cut)
        {
            var alteredGenes = subject.Genes.ToList().RemoveWhere(cut.Contains);
            var shuffledCut = cut.ToList().Shuffle(new Random(DateTime.Now.Millisecond));

            var crossedGenes = alteredGenes.Concat(shuffledCut);
            return new TspChromosome(crossedGenes);
        }
    }
}