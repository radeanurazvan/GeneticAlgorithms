using System.Linq;
using GeneticAlgorithmsHomeworks.Genetic;

namespace GeneticAlgorithmsHomeworks.Homework3
{
    public sealed class TspOrchestrator : GeneticOrchestrator<TspChromosome, City>
    {
        protected override FitnessFunction<TspChromosome, City> GetFitness()
        {
            return FitnessFunction<TspChromosome, City>.Create(c => c.GetTravelDistance());
        }

        protected override TspChromosome GetBestFromPopulation(Population<TspChromosome, City> population)
        {
            var populationMinimum = population.Chromosomes.Min(c => this.fitnessFunction.ValueFor(c));
            return population.Chromosomes.First(c => this.fitnessFunction.ValueFor(c) == populationMinimum);
        }

        protected override bool IsBetterCandidate(TspChromosome chromosome, TspChromosome winner)
        {
            return this.fitnessFunction.ValueFor(chromosome) < this.fitnessFunction.ValueFor(winner);
        }
    }
}