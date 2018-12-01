using System;
using System.Linq;

namespace GeneticAlgorithmsHomeworks.Homework2
{
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
            var cut = new Random().Next(first.Bits.Count() - 2);

            var firstLeftCut = first.Bits.TakeWhile((bit, index) => index != cut);
            var firstRightCut = first.Bits.Except(firstLeftCut);

            var secondLeftCut = second.Bits.TakeWhile((bit, index) => index != cut);
            var secondRightCut = second.Bits.Except(secondLeftCut);


            FirstResult = Chromosome.Create(firstLeftCut.Concat(secondRightCut));
            SecondResult = Chromosome.Create(secondLeftCut.Concat(firstRightCut));
        }
    }
}