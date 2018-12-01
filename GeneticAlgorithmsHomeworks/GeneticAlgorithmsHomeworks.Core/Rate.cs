using System;

namespace GeneticAlgorithmsHomeworks.Core
{
    public sealed class Rate
    {
        private double value;

        private Rate(double rate)
        {
            this.value = rate;
        }

        public static Rate Create(double rate)
        {
            if (rate < 0 || rate > 1)
            {
                throw new InvalidOperationException("Rate should be between 0 and 1!");
            }

            return new Rate(rate);
        }

        public static implicit operator double(Rate rate)
        {
            return rate.value;
        }

        public bool DoRandomPass()
        {
            var rand = new Random().NextDouble();

            if (rand >= 0 && rand <= value)
            {
                return true;
            }

            return false;
        }

        public void RunOnSuccessfulRandomPass(Action act)
        {
            if (DoRandomPass())
            {
                act();
            }
        }
    }
}