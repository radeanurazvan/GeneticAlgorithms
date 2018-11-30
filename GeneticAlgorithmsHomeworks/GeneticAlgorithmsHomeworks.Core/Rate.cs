using System;

namespace GeneticAlgorithmsHomeworks.Core
{
    public sealed class Rate
    {
        private double value;

        private double MinifiedRate => value / 100;

        private Rate(double rate)
        {
            this.value = rate;
        }

        public static Rate Create(double rate)
        {
            if (rate <= 0 || rate >= 100)
            {
                throw new InvalidOperationException("Rate should be greater than 0 and lower than 100!");
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

            if (rand >= 0 && rand <= MinifiedRate)
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