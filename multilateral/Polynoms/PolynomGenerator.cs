﻿using System;
using SecretSharing.Helpers;

namespace SecretSharing.Polynoms
{
    internal class PolynomGenerator
    {
        public static uint lowerXBound = 1;
        public static uint upperXBound = Mod.MOD - 1;

        private static readonly Random rand = new Random();

        public static ushort[] GenerateWholeNumbers(int count)
        {
            var result = new ushort[count];

            for (var i = 0; i < count; i++) result[i] = (ushort) rand.Next(ushort.MinValue, ushort.MaxValue);

            return result;
        }

        public static ushort[,] GeneratePolynomsMatrix(ushort[] key, int count, int power)
        {
            var matrix = new ushort[count, power + 1];

            var currentCoefficients = new ushort[power + 1];

            for (var i = 0; i < count; i++)
            {
                currentCoefficients = GenerateWholeNumbers(power + 1);
                currentCoefficients[0] = key[i];
                matrix.SetRow(i, currentCoefficients);
            }

            return matrix;
        }

        public static uint GetRandomX(uint lowerBond, uint upperBond)
        {
            var thirtyBits = (uint) rand.Next(1 << 30);
            var twoBits = (uint) rand.Next(1 << 2);
            return (thirtyBits << 2) | twoBits;
        }
    }
}