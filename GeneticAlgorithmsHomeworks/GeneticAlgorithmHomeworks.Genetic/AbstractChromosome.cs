namespace GeneticAlgorithmsHomeworks.Genetic
{
    using System.Collections.Generic;

    using GeneticAlgorithmsHomeworks.Core;
    using GeneticAlgorithmsHomeworks.Function;

    public abstract class AbstractChromosome<TGene, TChromosome> : DimensionSet<TGene>
    {
        protected AbstractChromosome(IEnumerable<TGene> set)
            : base(set)
        {
        }

        public IEnumerable<TGene> Genes => this.values;

        public abstract TChromosome Mutate(Rate mutationRate);
    }
}