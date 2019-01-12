using System.Collections.Generic;

namespace GeneticAlgorithmsHomeworks.Homework3
{
    using System;

    using GeneticAlgorithmsHomeworks.Core;

    public static class TspHelper
    {
        public static IEnumerable<City> GetRandomPath()
        {
            return World.Cities.Shuffle(new Random(DateTime.Now.Millisecond));
        }
    }
}