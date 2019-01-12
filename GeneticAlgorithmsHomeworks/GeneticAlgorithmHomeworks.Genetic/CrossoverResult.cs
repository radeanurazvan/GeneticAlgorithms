using System;

namespace GeneticAlgorithmsHomeworks.Genetic
{
    public sealed class CrossoverResult<TChromosome, TGene>
        where TChromosome : AbstractChromosome<TGene, TChromosome>
    {
        internal CrossoverResult(TChromosome firstOffspring, TChromosome secondOffspring)
        {
            if (firstOffspring == null || secondOffspring == null)
            {
                throw new InvalidOperationException("Crossover result cannot be null!");
            }

            this.FirstOffspring = firstOffspring;
            this.SecondOffspring = secondOffspring;
        }

        public TChromosome FirstOffspring { get; internal set; }

        public TChromosome SecondOffspring { get; internal set; }
    }
}