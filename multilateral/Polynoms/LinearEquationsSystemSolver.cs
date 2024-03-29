﻿using System;
using SecretSharing.Helpers;

namespace SecretSharing.Polynoms
{
    internal class LinearEquationsSystemSolver
    {
        public static ulong[] SolveWithFiniteField(ulong[,] matrix)
        {
            var n = matrix.GetLength(0);

            for (var i = 0; i < n; i++)
            {
                // Search for maximum in this column
                var maxEl = matrix[i, i];
                var maxRow = i;
                for (var k = i + 1; k < n; k++)
                    if (matrix[k, i] > maxEl)
                    {
                        maxEl = matrix[k, i];
                        maxRow = k;
                    }

                // Swap maximum row with current row (column by column)
                for (var k = i; k < n + 1; k++)
                {
                    var tmp = matrix[maxRow, k];
                    matrix[maxRow, k] = matrix[i, k];
                    matrix[i, k] = tmp;
                }

                // Make all rows below this one 0 in current column
                for (var k = i + 1; k < n; k++)
                {
                    var factor = matrix[k, i] * Mod.PowMod(matrix[i, i], Mod.MOD - 2, Mod.MOD) % Mod.MOD;

                    for (var j = i; j < n + 1; j++)
                        if (i == j)
                        {
                            matrix[k, j] = 0;
                        }
                        else
                        {
                            matrix[k, j] += Mod.MOD - factor * matrix[i, j] % Mod.MOD;
                            matrix[k, j] %= Mod.MOD;
                        }
                }
            }

            // Solve equation Ax=b for an upper triangular matrix A
            var coefficients = new ulong[n];

            for (var i = n - 1; i >= 0; i--)
            {
                coefficients[i] = matrix[i, n] * Mod.PowMod(matrix[i, i], Mod.MOD - 2, Mod.MOD) % Mod.MOD;

                for (var k = i - 1; k >= 0; k--)
                {
                    matrix[k, n] += Mod.MOD - matrix[k, i] * coefficients[i] % Mod.MOD;
                    matrix[k, n] %= Mod.MOD;
                }
            }

            return coefficients;
        }

        public static int[] SolveWithoutFiniteField(double[,] matrix)
        {
            var n = matrix.GetLength(0);

            for (var i = 0; i < n; i++)
            {
                // Search for maximum in this column
                var maxEl = Math.Abs(matrix[i, i]);
                var maxRow = i;
                for (var k = i + 1; k < n; k++)
                    if (Math.Abs(matrix[k, i]) > maxEl)
                    {
                        maxEl = Math.Abs(matrix[k, i]);
                        maxRow = k;
                    }

                // Swap maximum row with current row (column by column)
                for (var k = i; k < n + 1; k++)
                {
                    var tmp = matrix[maxRow, k];
                    matrix[maxRow, k] = matrix[i, k];
                    matrix[i, k] = tmp;
                }

                // Make all rows below this one 0 in current column
                for (var k = i + 1; k < n; k++)
                {
                    var factor = matrix[k, i] / matrix[i, i];
                    for (var j = i; j < n + 1; j++)
                        if (i == j)
                            matrix[k, j] = 0;
                        else
                            matrix[k, j] -= factor * matrix[i, j];
                }
            }


            // Solve equation Ax=b for an upper triangular matrix A
            var coefficients = new double[n];

            for (var i = n - 1; i >= 0; i--)
            {
                coefficients[i] = matrix[i, n] / matrix[i, i];

                for (var k = i - 1; k >= 0; k--) matrix[k, n] -= matrix[k, i] * coefficients[i];
            }

            var result = new int[n];
            for (var i = 0; i < n; i++) result[i] = Convert.ToInt32(coefficients[i]);
            return result;
        }
    }
}