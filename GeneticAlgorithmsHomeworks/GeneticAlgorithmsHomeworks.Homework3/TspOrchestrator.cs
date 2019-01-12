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

        public override double GetStartingBest()
        {
            return double.MaxValue;
        }

        protected override TspChromosome GetBestFromPopulation(Population<TspChromosome, City> population)
        {
            var populationMinimum = population.Chromosomes.Min(c => this.fitnessFunction.ValueFor(c));
            return population.Chromosomes.First(c => this.fitnessFunction.ValueFor(c) == populationMinimum);
        }

        protected override bool IsNewCandidate(TspChromosome chromosome, double currentBest)
        {
            return this.fitnessFunction.ValueFor(chromosome) < currentBest;
        }
    }
}