using System;

namespace NineDigit.SpreadsheetLightExtensions
{
    public sealed class RowReference
    {
        readonly int value;

        public RowReference(int value)
        {
            if (value <= 0)
                throw new ArgumentException("Row reference value can only be positive non-zero number.", nameof(value));

            this.value = value;
        }

        public static implicit operator int(RowReference c) => c.value;
        public static implicit operator RowReference(int s) => new RowReference(s);

        public override string ToString()
            => this.value.ToString();
    }
}
