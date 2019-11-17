using System.Collections.Generic;
using System.IO;
using System.Linq;
using ShamirsSecretSharingScheme.Services;

namespace Shamir_s_Secret_Sharing.Models
{
    public class Secret
    {
        private Secret(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Secret CreateFrom(Polynomial poly)
        {
            return new Secret(poly.Coefficients[0].ToString("x"));
        }

        public static Secret ReconstructFrom(IEnumerable<Share> shares)
        {
            if (shares.Count() < 2)
                throw new InvalidDataException("There are needed at least 2 shares to recover any secret.");

            if (!AssertDistinct(shares))
                throw new InvalidDataException("Non-unique shares was provided. Unable to recover secret.");

            var interpolator = new Interpolator(shares, Common.BasePrime);
            var secret = interpolator.Interpolate();
            return new Secret(secret.ToString("x"));
        }

        private static bool AssertDistinct(IEnumerable<Share> shares)
        {
            return shares.Distinct().Count() == shares.Count();
        }

        public override string ToString()
        {
            return Value;
        }
    }
}