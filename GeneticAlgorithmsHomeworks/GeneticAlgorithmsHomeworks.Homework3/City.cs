namespace GeneticAlgorithmsHomeworks.Homework3
{
    using System;

    public sealed class City
    {
        public int Position { get; set; }

        public string Name { get; set; }

        public int DistanceTo(City city)
        {
            return Math.Abs(this.Position - city.Position);
        }
    }
}