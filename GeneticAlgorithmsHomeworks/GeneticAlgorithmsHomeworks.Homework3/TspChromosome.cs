using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmsHomeworks.Core;
using GeneticAlgorithmsHomeworks.Genetic;

namespace GeneticAlgorithmsHomeworks.Homework3
{
    public sealed class TspChromosome : AbstractChromosome<City, TspChromosome>
    {
        public TspChromosome(IEnumerable<City> set)
            : base(set)
        {
        }

        public double GetTravelDistance()
        {
            var travelingDistance = 0d;
            for (var i = 0; i < Genes.Count() - 1; i++)
            {
                var currentCity = Genes.ElementAt(i);
                var nextCity = Genes.ElementAt(i + 1);

                travelingDistance += currentCity.DistanceTo(nextCity);
            }

            return travelingDistance;
        }

        public string GetPath()
        {
            return string.Join("\n\t", Genes.Select(c => c.Name));
        }

        public override TspChromosome Mutate(Rate mutationRate)
        {
            var mutatedGenes = this.Genes;
            mutationRate.RunOnSuccessfulRandomPass(() =>
            {
                var random = new Random(DateTime.Now.Millisecond);

                var firstIndex = random.Next() % this.Genes.Count();
                var secondIndex = random.Next() % this.Genes.Count();

                mutatedGenes = this.Permute(this.Genes, (firstIndex, secondIndex));
            });

            return new TspChromosome(mutatedGenes);
        }

        private IEnumerable<City> Permute(IEnumerable<City> genes, (int, int) indexes)
        {
            var firstValue = genes.ElementAt(indexes.Item1);
            var secondValue = genes.ElementAt(indexes.Item2);

            var permutedList = genes.ToList();
            permutedList[indexes.Item1] = secondValue;
            permutedList[indexes.Item2] = firstValue;

            return permutedList;
        }
    }
}