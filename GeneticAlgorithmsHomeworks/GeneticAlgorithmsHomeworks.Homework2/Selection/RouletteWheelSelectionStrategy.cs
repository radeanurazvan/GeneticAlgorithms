using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmsHomeworks.Function;

namespace GeneticAlgorithmsHomeworks.Homework2
{
    public sealed class RouletteWheelSelectionStrategy : PopulationSelectionStrategy
    {
        public override Population Select(Population population, FitnessFunction fitness)
        {
            var selectedPopulation = new List<DimensionSet<Chromosome>>();

            var setValues = ComputeSetValues(population.Chromosomes, fitness);
            var wheelValues = ComputeWheelValues(setValues).ToList();

            for (int i = 0; i < population.Size; i++)
            {
                var random = new Random().NextDouble();
                var selectedIndex = wheelValues.FindIndex(wv => wv > random);
                var selectedSet = population.Chromosomes.ElementAt(selectedIndex);
                selectedPopulation.Add(selectedSet);
            }

            return Population.Create(selectedPopulation);
        }

        private static IEnumerable<double> ComputeWheelValues(IEnumerable<double> setValues)
        {
            var totalFitness = setValues.Sum();
            var wheelValues = new List<double>();

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

        private static IEnumerable<double> ComputeSetValues(IEnumerable<DimensionSet<Chromosome>> chromosomes, FitnessFunction fitness)
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