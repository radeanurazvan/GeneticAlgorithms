using System.Collections.Generic;
using GeneticAlgorithmsHomeworks.Core;

namespace GeneticAlgorithmsHomeworks.Homework3
{
    public sealed class CoordinateSet : ValueObject
    {
        private CoordinateSet(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; }

        public double Y { get; }

        public static CoordinateSet Create(double x, double y)
        {
            return new CoordinateSet(x, y);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return X;
            yield return Y;
        }
    }
}