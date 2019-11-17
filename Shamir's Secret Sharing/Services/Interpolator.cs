using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Shamir_s_Secret_Sharing.Models;

namespace ShamirsSecretSharingScheme.Services
{
    public class Interpolator
    {
        private readonly BigInteger _basePrime;
        private readonly Share[] _shares;

        public Interpolator(IEnumerable<Share> shares, BigInteger basePrime)
        {
            _shares = shares.ToArray();
            _basePrime = basePrime;
        }

        public BigInteger Interpolate()
        {
            var k = _shares.Length;

            var numerators = new List<int>(k);
            var denominators = new List<int>(k);

            for (var i = 0; i < k; i++)
            {
                var actualShare = _shares[i];
                var otherShares = _shares.Where((item, index) => index != i);

                var n = otherShares.Select(s => -s.X);
                var d = otherShares.Select(s => actualShare.X - s.X);

                numerators.Add(ProductOfInputs(n));
                denominators.Add(ProductOfInputs(d));
            }

            var accumulatedDenominator = ProductOfInputs(denominators);

            var total = new BigInteger(0);
            for (var i = 0; i < k; i++)
            {
                var currentNumerator = numerators[i];
                var currentDenominator = denominators[i];

                var pointY = _shares[i].Value;
                var currentNumeratorMod = Modulus(currentNumerator * accumulatedDenominator * pointY, _basePrime);
                var divisionMod = DivisionModulus(currentNumeratorMod, currentDenominator, _basePrime);
                total += divisionMod;
            }

            return Modulus(DivisionModulus(total, accumulatedDenominator, _basePrime) + _basePrime, _basePrime);
        }

        private static int ProductOfInputs(IEnumerable<int> values)
        {
            return values.Aggregate(1, (current, value) => current * value);
        }

        private static BigInteger DivisionModulus(BigInteger num, BigInteger den, BigInteger prime)
        {
            return num * ExtendedGreatestCommonDivisor(den, prime).Item1;
        }

        private static Tuple<BigInteger, BigInteger> ExtendedGreatestCommonDivisor(BigInteger a, BigInteger b)
        {
            var x = new Tuple<BigInteger, BigInteger>(0, 1);
            var y = new Tuple<BigInteger, BigInteger>(1, 0);

            while (!b.IsZero)
            {
                var quotient = a / b;
                var modulus = Modulus(a, b);

                a = b;
                b = modulus;

                x = GcdStep(quotient, x.Item1, x.Item2);
                y = GcdStep(quotient, y.Item1, y.Item2);
            }

            return new Tuple<BigInteger, BigInteger>(x.Item2, y.Item2);
        }

        private static BigInteger Modulus(BigInteger a, BigInteger b)
        {
            return (a % b + b) % b;
        }

        private static Tuple<BigInteger, BigInteger> GcdStep(BigInteger quotient, BigInteger value,
            BigInteger lastValue)
        {
            return new Tuple<BigInteger, BigInteger>(
                lastValue - quotient * value,
                value
            );
        }
    }
}