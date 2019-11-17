using System.Collections.Generic;
using System.Numerics;

namespace Shamir_s_Secret_Sharing.Models
{
    public class Share
    {
        private Share(int x, BigInteger value)
        {
            Value = value;
            X = x;
        }

        public BigInteger Value { get; }
        public int X { get; }

        public override string ToString()
        {
            return $"{X}: {Value.ToString("x")}";
        }

        public static IEnumerable<Share> GenerateMultipleFrom(Polynomial poly, uint count)
        {
            var result = new Share[count];

            for (var i = 0; i < count; i++)
            {
                var x = i + 1;
                result[i] = new Share(x, poly.EvaluateAt(x));
            }

            return result;
        }
    }
}