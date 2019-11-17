using System.Numerics;

namespace ShamirsSecretSharingScheme.Services
{
    public static class Common
    {
        /**
         * Prime number determining strength of generated secret.
         * 12th Mersenne Prime gives about 128 bits of strength.
         * 13th Mersenne Prime gives about 521 bits of strength.
         * Value must be the same when shares are generated and
         * when secret is restored.
         */
        public static BigInteger BasePrime => MersennePrimesGenerator.Get(12);
    }
}