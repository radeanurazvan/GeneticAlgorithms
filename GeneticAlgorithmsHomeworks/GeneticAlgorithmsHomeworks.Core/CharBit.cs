using System;
using System.Collections.Generic;

namespace GeneticAlgorithmsHomeworks.Core
{
    public class CharBit : ValueObject
    {
        private char value;
        private int position;

        private CharBit(char bit, int position)
        {
            if (bit != '0' && bit != '1')
            {
                throw new InvalidOperationException("Bit values should be only 0 or 1!");
            }

            if (position < 0)
            {
                throw new InvalidOperationException("Bit position should not be negative!");
            }

            this.value = bit;
            this.position = position;
        }

        public static CharBit Create(char bit, int position)
        {
            return new CharBit(bit, position);
        }

        public static implicit operator char(CharBit bit)
        {
            return bit.value;
        }

        public CharBit Negate()
        {
            if (this.value == '0')
            {
                return new CharBit('1', this.position);
            }

            return new CharBit('0', this.position);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return value;
            yield return position;
        }
    }
}