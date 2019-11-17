using System.Linq;
using System.Numerics;
using ShamirsSecretSharingScheme.Services;

namespace Shamir_s_Secret_Sharing.Models
{
    public class Polynomial
    {
        private readonly BigInteger _prime;

        private Polynomial(BigInteger[] coefficients)
        {
            Coefficients = coefficients;
            _prime = Common.BasePrime;
        }

        public BigInteger[] Coefficients { get; }

        public BigInteger EvaluateAt(int x)
        {
            var accum = new BigInteger(0);

            foreach (var coefficient in Coefficients.Reverse())
            {
                accum *= x;
                accum += coefficient;
                accum %= _prime;
            }

            return accum;
        }

        public static Polynomial Create(uint degree, BigInteger prime)
        {
            var coefficients = BigIntegerRandomGenerator.GetMultiple(prime, degree);
            return new Polynomial(coefficients);
        }
    }
}