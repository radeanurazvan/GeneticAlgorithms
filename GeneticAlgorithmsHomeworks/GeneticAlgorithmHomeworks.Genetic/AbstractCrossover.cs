namespace GeneticAlgorithmsHomeworks.Genetic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class AbstractCrossover<TChromosome, TGene>
        where TChromosome : AbstractChromosome<TGene, TChromosome>
    {
        protected abstract (TChromosome, TChromosome) DoCrossoverCore(
            int cutPoint,
            (IEnumerable<TGene> leftCut, IEnumerable<TGene> rightCut) first,
            (IEnumerable<TGene> leftCut, IEnumerable<TGene> rightCut) second);

        public CrossoverResult<TChromosome, TGene> DoCrossover(TChromosome first, TChromosome second)
        {
            var cut = new Random().Next(first.Count() - 2);

            var firstLeftCut = first.Genes.TakeWhile((bit, index) => index != cut);
            var firstRightCut = first.Genes.Except(firstLeftCut);

            var secondLeftCut = second.Genes.TakeWhile((bit, index) => index != cut);
            var secondRightCut = second.Genes.Except(secondLeftCut);

            var result = DoCrossoverCore(cut, (firstLeftCut, firstRightCut), (secondLeftCut, secondRightCut));

            return new CrossoverResult<TChromosome, TGene>(result.Item1, result.Item2);
        }
    }
}