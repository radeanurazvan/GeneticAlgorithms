using System;

namespace GeneticAlgorithmsHomeworks.Homework2
{
    using System.Linq;

    public class Crossover
    {
        public Chromosome FirstResult { get; private set; }

        public Chromosome SecondResult { get; private set; }

        private Crossover(Chromosome first, Chromosome second)
        {
            DoCrossover(first, second);
        }

        public static Crossover Create(Chromosome first, Chromosome second)
        {
            if (first == null || second == null)
            {
                throw new InvalidOperationException("Crossover subjects cannot be null!");
            }

            return new Crossover(first, second);
        }

        private void DoCrossover(Chromosome first, Chromosome second)
        {
            var cut = new Random().Next(first.Count() - 2);

            var firstLeftCut = first.Representations.TakeWhile((bit, index) => index != cut);
            var firstRightCut = first.Representations.Except(firstLeftCut);

            var secondLeftCut = second.Representations.TakeWhile((bit, index) => index != cut);
            var secondRightCut = second.Representations.Except(secondLeftCut);


            FirstResult = Chromosome.Create(firstLeftCut.Concat(secondRightCut));
            SecondResult = Chromosome.Create(secondLeftCut.Concat(firstRightCut));
        }
    }
}