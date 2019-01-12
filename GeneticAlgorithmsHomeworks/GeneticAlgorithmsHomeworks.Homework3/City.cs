namespace GeneticAlgorithmsHomeworks.Homework3
{
    using System;
    using System.Collections.Generic;

    using GeneticAlgorithmsHomeworks.Core;

    public sealed class City : ValueObject
    {
        public int Position { get; set; }

        public string Name { get; set; }

        public int DistanceTo(City city)
        {
            return Math.Abs(this.Position - city.Position);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Position;
        }
    }
}