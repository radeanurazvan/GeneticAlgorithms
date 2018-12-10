using System.Linq;
using GeneticAlgorithmsHomeworks.Core;
using GeneticAlgorithmsHomeworks.Function;

namespace GeneticAlgorithmsHomeworks.Homework1
{
    public sealed class BinarySetToDoubleSetConverter : FunctionSetToDoubleSetConverter<DimensionSet<BinaryRepresentation>>
    {
        public override DimensionSet<double> Convert(
            DimensionSet<BinaryRepresentation> source,
            DimensionalFunction function)
        {
            var doubles =
                source.Select((x, dimension) =>
                    BinaryHelper.DecodeBinary(x, function.GetDomain().GetDefinitionForDimension(dimension + 1), function.Precision));

            return new DimensionSet<double>(doubles);
        }
    }
}