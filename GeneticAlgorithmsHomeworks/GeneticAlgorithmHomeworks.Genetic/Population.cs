using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmsHomeworks.Core;

namespace GeneticAlgorithmsHomeworks.Genetic
{

    public sealed class Population<TChromosome, TGene>
        where TChromosome : AbstractChromosome<TGene, TChromosome>
    {
        public IEnumerable<TChromosome> Chromosomes { get; } = new List<TChromosome>();

        public int Size => Chromosomes.Count();

        private Population(IEnumerable<TChromosome> chromosomes)
        {
            this.Chromosomes = chromosomes ?? throw new InvalidOperationException();
        }

        public static Population<TChromosome, TGene> Create(IEnumerable<TChromosome> chromosomes)
        {
            return new Population<TChromosome, TGene>(chromosomes);
        }

        public Population<TChromosome, TGene> Mutate(Rate mutationRate)
        {
            var mutatedChromosomes = Chromosomes.Select(c => c.Mutate(mutationRate));
            return new Population<TChromosome, TGene>(mutatedChromosomes);
        }

        public Population<TChromosome, TGene> CrossOver(Rate crossoverRate, AbstractCrossover<TChromosome, TGene> crossover)
        {
            var chromosomesIndexToCross = this.Chromosomes
                .Select((chromosome, index) => (chromosome, index))
                .Where(c => crossoverRate.DoRandomPass())
                .Select((c, i) => i)
                .ToList();
            if (chromosomesIndexToCross.Any() && chromosomesIndexToCross.Count() % 2 != 0)
            {
                chromosomesIndexToCross.Remove(chromosomesIndexToCross.Last());
            }

            var crossedChromosomes = new List<TChromosome>();
            for (var i = 0; i < chromosomesIndexToCross.Count - 1; i += 2)
            {
                var firstChromosome = Chromosomes.ElementAt(i);
                var secondChromosome = Chromosomes.ElementAt(i + 1);

                var crossoverResult = crossover.DoCrossover(firstChromosome, secondChromosome);
                crossedChromosomes.Add(crossoverResult.FirstOffspring);
                crossedChromosomes.Add(crossoverResult.SecondOffspring);
            }

            var lefoutChromosomes = Chromosomes.Where((c, i) => !chromosomesIndexToCross.Contains(i));

            return new Population<TChromosome, TGene>(lefoutChromosomes.Concat(crossedChromosomes));
        }

        public Population<TChromosome, TGene> Select(PopulationSelectionStrategy<TChromosome, TGene> selectionStrategy, FitnessFunction<TChromosome, TGene> fitness)
        {
            return selectionStrategy.Select(this, fitness);
        }
    }
}