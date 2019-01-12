using GeneticAlgorithmsHomeworks.Core;

namespace GeneticAlgorithmsHomeworks.Genetic
{
    using System;

    public abstract class GeneticOrchestrator<TChromosome, TGene>
        where TChromosome : AbstractChromosome<TGene, TChromosome>
    {
        public int populationSize;
        protected FitnessFunction<TChromosome, TGene> fitnessFunction;

        private int generations;
        private Rate crossoverRate;
        private Rate mutationRate;

        private double startingBest;
        private AbstractCrossover<TChromosome, TGene> crossover;

        protected GeneticOrchestrator()
        {
            this.startingBest = this.GetStartingBest();
            this.fitnessFunction = this.GetFitness();
        }

        public GeneticOrchestrator<TChromosome, TGene> WithPopulationSize(int populationSize)
        {
            if (populationSize <= 0)
            {
                throw new InvalidOperationException("Population size should be greater than 0!");
            }

            this.populationSize = populationSize;
            return this;
        }

        public GeneticOrchestrator<TChromosome, TGene> WithGenerations(int generations)
        {
            if (generations <= 0)
            {
                throw new InvalidOperationException("Generations number should be greater than 0!");
            }

            this.generations = generations;
            return this;
        }

        public GeneticOrchestrator<TChromosome, TGene> WithCrossoverRate(double rate)
        {
            if (rate <= 0)
            {
                throw new InvalidOperationException("Crossover rate should be higher than 0!");
            }

            this.crossoverRate = Rate.Create(rate);
            return this;
        }

        public GeneticOrchestrator<TChromosome, TGene> WithMutationRate(double rate)
        {
            if (rate <= 0)
            {
                throw new InvalidOperationException("Mutation rate should be higher than 0!");
            }

            this.mutationRate = Rate.Create(rate);
            return this;
        }

        protected abstract FitnessFunction<TChromosome, TGene> GetFitness();

        public abstract double GetStartingBest();

        public GeneticOrchestrator<TChromosome, TGene> WithCrossover(AbstractCrossover<TChromosome, TGene> crossver)
        {
            this.crossover =
                crossver ?? throw new InvalidOperationException("Crossover cannot be null!");

            return this;
        }

        protected abstract TChromosome GetBestFromPopulation(Population<TChromosome, TGene> population);

        protected abstract bool IsNewCandidate(TChromosome chromosome, double currentBest);

        public TChromosome GetWinner(Population<TChromosome, TGene> population)
        {
            TChromosome winner = null;
            var currentBest = this.startingBest;
            var selectionStrategy = new RouletteWheelSelectionStrategy<TChromosome, TGene>();

            for (var generation = 1; generation <= this.generations; generation++)
            {
                population = population.Mutate(mutationRate);
                population = population.CrossOver(crossoverRate, this.crossover);
                population = population.Select(selectionStrategy, this.fitnessFunction);

                var bestGenerationChromosome = GetBestFromPopulation(population);
                if (this.IsNewCandidate(bestGenerationChromosome, currentBest))
                {
                    currentBest = this.fitnessFunction.ValueFor(bestGenerationChromosome);
                    winner = bestGenerationChromosome;
                }
            }

            return winner;
        }
    }
}