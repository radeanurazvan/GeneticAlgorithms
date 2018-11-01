using GeneticAlgorithmsHomeworks.Homework0;
using GeneticAlgorithmsHomeworks.Homework1;

namespace GeneticAlgorithmsHomeworks.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var homeworkPresenter = new HillClimbPresenter();
            var simulatedAnnealingPresenter = new SimulatedAnnealingPresenter();

            homeworkPresenter.Present();
            simulatedAnnealingPresenter.Present();
        }

    }
}
