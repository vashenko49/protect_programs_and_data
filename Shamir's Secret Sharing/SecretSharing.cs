using System.Collections.Generic;
using System.IO;
using Shamir_s_Secret_Sharing.Models;
using ShamirsSecretSharingScheme.Services;

namespace Shamir_s_Secret_Sharing
{
    public class SecretSharing
    {
        private SecretSharing(Secret secret, IEnumerable<Share> shares)
        {
            Secret = secret;
            Shares = shares;
        }

        public Secret Secret { get; }
        public IEnumerable<Share> Shares { get; }

        public static SecretSharing Create(int minimumShares, int allShares)
        {
            if (minimumShares > allShares)
                throw new InvalidDataException("Minimum required shares must be lower than number of generated shares");

            if (minimumShares <= 0 || allShares <= 0)
                throw new InvalidDataException("Number of shares must be higher than 0");

            var polynomial = Polynomial.Create((uint) minimumShares, Common.BasePrime);
            var secret = Secret.CreateFrom(polynomial);
            var shares = Share.GenerateMultipleFrom(polynomial, (uint) allShares);

            return new SecretSharing(secret, shares);
        }
    }
}