using System;

namespace GeneticAlgorithmsHomeworks.Core
{
    public class CharBit
    {
        private char value;

        private CharBit(char bit)
        {
            if (bit != '0' && bit != '1')
            {
                throw new InvalidOperationException("Bit values should be only 0 or 1!");
            }

            this.value = bit;
        }

        public static CharBit Create(char bit)
        {
            return new CharBit(bit);
        }

        public static implicit operator char(CharBit bit)
        {
            return bit.value;
        }

        public CharBit Negate()
        {
            if (this.value == '0')
            {
                return new CharBit('1');
            }

            return new CharBit('0');
        }
    }
}