using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmsHomeworks.Core;
using GeneticAlgorithmsHomeworks.Genetic;

namespace GeneticAlgorithmsHomeworks.Homework3
{

    public sealed class TspCrossover : AbstractCrossover<TspChromosome, City>
    {
        protected override (TspChromosome, TspChromosome) DoCrossoverCore(
            int cutPoint,
            (IEnumerable<City> leftCut, IEnumerable<City> rightCut) first,
            (IEnumerable<City> leftCut, IEnumerable<City> rightCut) second)
        {
            var firstParent = new TspChromosome(first.leftCut.Concat(first.rightCut));
            var secondParent = new TspChromosome(second.leftCut.Concat(second.rightCut));
            var secondCut = new Random().Next(cutPoint, firstParent.Count() - 2);

            var firstOffspringGenesToCross = firstParent.Genes.Select((gene, index) => new {index, gene})
                .Where(x => x.index >= cutPoint && x.index <= secondCut)
                .Select(x => x.gene);


            var firstOffspring = secondParent.Genes.ToList().Where(x => !firstOffspringGenesToCross.Contains(x)).ToList();
            firstOffspring.InsertRange(cutPoint, firstOffspringGenesToCross);

            var seconfOffspringGenesToCross = secondParent.Genes.Select((gene, index) => new { index, gene })
                .Where(x => x.index >= cutPoint && x.index <= secondCut)
                .Select(x => x.gene);
            var secondOffSpring = firstParent.Genes.ToList().Where(x => !seconfOffspringGenesToCross.Contains(x)).ToList();
            secondOffSpring.InsertRange(cutPoint, seconfOffspringGenesToCross);

            return (new TspChromosome(firstOffspring), new TspChromosome(secondOffSpring));
        }
    }
}