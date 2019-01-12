namespace GeneticAlgorithmsHomeworks.Homework3
{
    using System.Collections.Generic;
    using System.Linq;

    using GeneticAlgorithmsHomeworks.Core;
    using GeneticAlgorithmsHomeworks.Genetic;

    public sealed class TspChromosome : AbstractChromosome<City, TspChromosome>
    {
        public TspChromosome(IEnumerable<City> set)
            : base(set)
        {
        }

        public double GetTravelDistance()
        {
            var travelingDistance = 0;
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
            return string.Join(" - ", Genes.Select(c => c.Name));
        }

        public override TspChromosome Mutate(Rate mutationRate)
        {
            var indexesToPermute = this.GetGenesToPermute(mutationRate);
            if (indexesToPermute.Count() <= 1)
            {
                return this;
            }

            return new TspChromosome(this.PermuteGenes(indexesToPermute));
        }

        private IEnumerable<City> PermuteGenes(IEnumerable<int> indexesToPermute)
        {
            var permutedGenes = this.Genes.ToList();
            for (var i = 0; i < indexesToPermute.Count() - 1; i += 2)
            {
                var firstIndex = indexesToPermute.ElementAt(i);
                var secondIndex = indexesToPermute.ElementAt(i + 1);

                permutedGenes = this.Permute(this.Genes, (firstIndex, secondIndex)).ToList();
            }

            return permutedGenes;
        }

        private IEnumerable<int> GetGenesToPermute(Rate mutationRate)
        {
            var indexToPermute = new List<int>();
            var genesList = this.Genes.ToList();

            foreach (var gene in this.Genes)
            {
                if (!mutationRate.DoRandomPass())
                {
                    continue;
                }

                var index = genesList.IndexOf(gene);
                indexToPermute.Add(index);
            }

            return indexToPermute;
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