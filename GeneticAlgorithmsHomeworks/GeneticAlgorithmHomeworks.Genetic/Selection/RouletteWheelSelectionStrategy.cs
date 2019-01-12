using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithmsHomeworks.Genetic
{
    public sealed class RouletteWheelSelectionStrategy<TChromosome, TGene> : PopulationSelectionStrategy<TChromosome, TGene>
        where TChromosome : AbstractChromosome<TGene, TChromosome>
    {
        public override Population<TChromosome, TGene> Select(Population<TChromosome, TGene> population, FitnessFunction<TChromosome, TGene> fitness)
        {
            var selectedPopulation = new List<TChromosome>();

            var setValues = ComputeSetValues(population.Chromosomes, fitness);
            var wheelValues = ComputeWheelValues(setValues).ToList();

            for (var i = 0; i < population.Size; i++)
            {
                var random = new Random().NextDouble();
                var selectedIndex = wheelValues.FindIndex(wv => wv > random);
                if (selectedIndex == wheelValues.Count - 1)
                {
                    selectedIndex--;
                }

                var selectedSet = population.Chromosomes.ElementAt(selectedIndex);
                selectedPopulation.Add(selectedSet);
            }

            return Population<TChromosome, TGene>.Create(selectedPopulation);
        }

        private static IEnumerable<double> ComputeWheelValues(IEnumerable<double> setValues)
        {
            var totalFitness = setValues.Sum();
            var wheelValues = new List<double> { 0 };

            var accumulated = 0d;
            foreach (var value in setValues)
            {
                accumulated += value;
                wheelValues.Add(accumulated/totalFitness);
            }

            wheelValues.RemoveAt(wheelValues.Count - 1);
            wheelValues.Add(1);

            return wheelValues;
        }

        private static IEnumerable<double> ComputeSetValues(IEnumerable<TChromosome> chromosomes, FitnessFunction<TChromosome, TGene> fitness)
        {
            var setValues = chromosomes.Select(fitness.ValueFor);
            if (setValues.Any(v => v <= 0))
            {
                return TranslateSetValues(setValues);
            }

            return setValues;
        }

        private static IEnumerable<double> TranslateSetValues(IEnumerable<double> values)
        {
            var minimum = values.Min();
            var constant = Math.Abs(minimum) + 1;
            return values.Select(v => v + constant);
        }
    }
}