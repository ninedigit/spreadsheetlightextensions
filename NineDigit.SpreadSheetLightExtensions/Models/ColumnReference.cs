using System;
using System.Linq;

namespace NineDigit.SpreadsheetLightExtensions
{
    public sealed class ColumnReference
    {
        readonly string value;

        public ColumnReference(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Invalud column reference value.", nameof(value));

            if (value.Any(i => !char.IsLetter(i)))
                throw new ArgumentException("Column reference value can only contain letters.", nameof(value));

            this.value = value;
        }

        public static implicit operator string(ColumnReference c) => c.value;
        public static implicit operator int(ColumnReference c) => ColumnAddressConverter.ToColumnNumber(c);
        
        public static implicit operator ColumnReference(string s) => new ColumnReference(s);

        /// <summary>
        /// First column in row.
        /// </summary>
        public static ColumnReference First => new ColumnReference("A");

        public ColumnReference GetPrevious()
            => this.Add(-1);

        public ColumnReference GetNext()
            => this.Add(1);

        private ColumnReference Add(int columnsCount)
        {
            var number = ColumnAddressConverter.ToColumnNumber(this.value);
            return ColumnAddressConverter.ToColumnName(number + columnsCount);
        }

        public override string ToString()
            => this.value;

        /// <summary>
        /// Source: https://stackoverflow.com/a/2652855/1218508
        /// </summary>
        class ColumnAddressConverter
        {
            public static string ToColumnName(int col)
            {
                if (col <= 0)
                    throw new ArgumentException("Invalid column number", nameof(col));

                if (col <= 26)
                    return Convert.ToChar(col + 64).ToString();

                int div = col / 26;
                int mod = col % 26;
                if (mod == 0) { mod = 26; div--; }
                return ToColumnName(div) + ToColumnName(mod);
            }

            public static int ToColumnNumber(string colAdress)
            {
                if (string.IsNullOrWhiteSpace(colAdress))
                    throw new ArgumentException("Invalud column name", nameof(colAdress));

                colAdress = colAdress.ToUpperInvariant();
                var digits = new int[colAdress.Length];
                for (int i = 0; i < colAdress.Length; ++i)
                    digits[i] = Convert.ToInt32(colAdress[i]) - 64;
                
                int mul = 1;
                int res = 0;
                
                for (int pos = digits.Length - 1; pos >= 0; --pos)
                {
                    res += digits[pos] * mul;
                    mul *= 26;
                }

                return res;
            }
        }
    }
}
