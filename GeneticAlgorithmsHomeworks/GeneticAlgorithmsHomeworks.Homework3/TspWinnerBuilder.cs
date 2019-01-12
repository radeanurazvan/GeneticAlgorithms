using System.Collections.Generic;
using GeneticAlgorithmsHomeworks.Genetic;

namespace GeneticAlgorithmsHomeworks.Homework3
{
    public sealed class TspWinnerBuilder
    {
        private TspOrchestrator orchestrator;

        public TspWinnerBuilder(TspOrchestrator orchestrator)
        {
            this.orchestrator = orchestrator;
        }

        public TspChromosome Build()
        {
            var chromosomes = new List<TspChromosome>();
            for (int i = 0; i < this.orchestrator.populationSize; i++)
            {
                var randomPath = TspHelper.GetRandomPath();
                chromosomes.Add(new TspChromosome(randomPath));
            }

            var population = Population<TspChromosome, City>.Create(chromosomes);

            return this.orchestrator.GetWinner(population);
        }
    }
}