namespace GeneticAlgorithmsHomeworks.Genetic
{
    public abstract class PopulationSelectionStrategy<TChromosome, TGene>
        where TChromosome : AbstractChromosome<TGene, TChromosome>
    {
        public abstract Population<TChromosome, TGene> Select(Population<TChromosome, TGene> population, FitnessFunction<TChromosome, TGene> fitness);
    }
}