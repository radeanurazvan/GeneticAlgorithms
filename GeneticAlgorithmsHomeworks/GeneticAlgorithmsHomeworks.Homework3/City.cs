using System;
using System.Collections.Generic;
using GeneticAlgorithmsHomeworks.Core;

namespace GeneticAlgorithmsHomeworks.Homework3
{
    public sealed class City : ValueObject
    {
        public string Name { get; set; }

        public CoordinateSet Coordinates { get; set; }

        public double DistanceTo(City city)
        {
            var xSquare = Math.Pow(city.Coordinates.X - this.Coordinates.X, 2);
            var ySquare = Math.Pow(city.Coordinates.Y - this.Coordinates.Y, 2);

            return Math.Sqrt(xSquare + ySquare);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Coordinates;
        }
    }
}