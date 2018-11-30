using System;
using System.Linq;
using GeneticAlgorithmsHomeworks.Core;
using static System.Math;
    
namespace GeneticAlgorithmsHomeworks.Function
{
    public class BinaryHelper
    {
        public static int BitsNumberForDomainDimension(DomainDefinition dimensionDomain, int precision)
        {
            var logArgument = (dimensionDomain.End - dimensionDomain.Start) * Pow(10, precision);

            return (int)Log(logArgument, 2);
        }

        public static double DecodeBinary(BinaryRepresentation binaryRepresentation, DomainDefinition domainDefinition, int precision)
        {
            var d = Convert.ToInt64(binaryRepresentation.AsString(), 2);

            var bitsNumber = BitsNumberForDomainDimension(domainDefinition, precision);
            return (domainDefinition.End - domainDefinition.Start) * (d / (Pow(2, bitsNumber) - 1)) + domainDefinition.Start;
        }
    }
}