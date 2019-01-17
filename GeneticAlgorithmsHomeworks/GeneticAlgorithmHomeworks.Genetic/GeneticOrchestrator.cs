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

        private AbstractCrossover<TChromosome, TGene> crossover;
        private int badGenerationsLimit;

        protected GeneticOrchestrator()
        {
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

        public GeneticOrchestrator<TChromosome, TGene> WithBadGenerationsLimit(int limit)
        {
            if (limit <= 0)
            {
                throw new InvalidOperationException("Bad generations limit number should be greater than 0!");
            }

            this.badGenerationsLimit = limit;
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

        public GeneticOrchestrator<TChromosome, TGene> WithCrossover(AbstractCrossover<TChromosome, TGene> crossver)
        {
            this.crossover =
                crossver ?? throw new InvalidOperationException("Crossover cannot be null!");

            return this;
        }

        protected abstract TChromosome GetBestFromPopulation(Population<TChromosome, TGene> population);

        protected abstract bool IsBetterCandidate(TChromosome chromosome, TChromosome currentWinner);

        public TChromosome GetWinner(Population<TChromosome, TGene> population)
        {
            var selectionStrategy = new RouletteWheelSelectionStrategy<TChromosome, TGene>();

            return this.RunGenetic(() =>
            {
                population = population.Mutate(mutationRate);
                population = population.CrossOver(crossoverRate, this.crossover);
                population = population.Select(selectionStrategy, this.fitnessFunction);

                var bestGenerationChromosome = GetBestFromPopulation(population);
                return bestGenerationChromosome;
            });
        }

        private TChromosome RunGenetic(Func<TChromosome> genetic)
        {
            var winner = genetic();

            void OnNewCandidate(TChromosome candidate)
            {
                if (IsBetterCandidate(candidate, winner))
                {
                    winner = candidate;
                }
            }

            if (this.generations > 0)
            {
                RunUntilGenerationsRunOut(genetic, OnNewCandidate);
            }

            if (this.badGenerationsLimit > 0)
            {
                RunUntilBadGenerationsGetExhausted(genetic, OnNewCandidate);
            }

            return winner;
        }

        private void RunUntilGenerationsRunOut(Func<TChromosome> genetic, Action<TChromosome> onCandidateReceived)
        {
            for (var generation = 1; generation <= generations; generation++)
            {
                var candidate = genetic();
                onCandidateReceived(candidate);
            }
        }

        private void RunUntilBadGenerationsGetExhausted(Func<TChromosome> genetic, Action<TChromosome> onCandidateReceived)
        {
            var winner = genetic();
            var candidatesExhausted = false;

            var badGenerations = 0;

            while (badGenerations != badGenerationsLimit)
            {
                var newCandidate = genetic();
                onCandidateReceived(newCandidate);

                badGenerations++;
                if (IsBetterCandidate(newCandidate, winner))
                {
                    winner = newCandidate;
                    badGenerations = 0;
                }
            }
        }
    }
}