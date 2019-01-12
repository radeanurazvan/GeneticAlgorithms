namespace GeneticAlgorithmsHomeworks.Homework3
{
    using System.Collections.Generic;

    public static class World
    {
        public static IEnumerable<City> Cities => new List<City> 
        {
            new City { Name = "Iasi", Position = 0 },
            new City { Name = "Piatra Neamt", Position = 25 },
            new City { Name = "Suceava", Position = 40 },
            new City { Name = "Bacau", Position = 61 },
            new City { Name = "Botosani", Position = 66 },
            new City { Name = "Ghergheni", Position = 119 },
            new City { Name = "Comanesti", Position = 126 },
            new City { Name = "Vaslui", Position = 164 },
            new City { Name = "Laza", Position = 188 },
            new City { Name = "Lipova", Position = 208 },
            new City { Name = "Roznov", Position = 216 },
            new City { Name = "Cacica", Position = 223 },
            new City { Name = "Radauti", Position = 396 },
            new City { Name = "Saveni", Position = 406 },
            new City { Name = "Albita", Position = 428 },
            new City { Name = "Galati", Position = 432 },
            new City { Name = "Dorohoi", Position = 499 },
        };
    }
}