using System;

namespace Binary_representation_of_numbers
{
    internal class Program
    {
        private static int exp;
        private static int dotPos;

        public static int[] AddBinary(int[] op1, int[] op2)
        {
            if (op1.Length != op2.Length) throw new InvalidOperationException("Arrays must be equal in length");

            var result = new int[op1.Length];
            for (var i = 1; i < op1.Length + 1; i++)
                if (op1[op1.Length - i] > 1 || op2[op2.Length - i] > 1 || op1[op1.Length - i] < 0 ||
                    op2[op2.Length - i] < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        "Array has a number that is invalid in it, argument out of range.");
                }
                else
                {
                    result[result.Length - i] = op1[op1.Length - i] + op2[op2.Length - i] + result[result.Length - i];
                    if (result[result.Length - i] == 2)
                    {
                        if (result.Length - i - 1 < 0)
                        {
                            var message = "Binary numbers added together have grown beyond the range of the array.";
                            var caption = "Error, overflow.";
                            Console.WriteLine(message + " " + caption);
                            var escape = new int[1];
                            return escape;
                        }

                        try
                        {
                            result[result.Length - i] = 0;
                            result[result.Length - i - 1] = 1;
                        }
                        catch
                        {
                            // ignored
                        }
                    }

                    if (result[result.Length - i] == 3)
                    {
                        if (result.Length - i - 1 < 0)
                        {
                            var message = "Binary numbers added together have grown beyond the range of the array.";
                            var caption = "Error, overflow.";
                            Console.WriteLine(message + " " + caption);
                            var escape = new int[1];
                            return escape;
                        }

                        try
                        {
                            result[result.Length - i] = 1;
                            result[result.Length - i - 1] = 1;
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                }

            return result;
        }

        private static string SignedMagnitudeRepresentation(double input)
        {
            var result = "";
            if (input < 0)
            {
                input = Math.Abs(input);
                result += "1";
            }
            else
            {
                result += "0";
            }

            result += IntBitConvert(input, false, true);
            return result;
        }

        private static string OnesComplement(double input)
        {
            var res = SignedMagnitudeRepresentation(input);

            for (var i = 1; i < res.Length; i++)
            {
                res = res[i] == '0' ? res.Insert(i, "1") : res.Insert(i, "0");
                res = res.Remove(i + 1, 1);
            }

            return res;
        }

        private static string TwosComplement(double input)
        {
            var res = OnesComplement(input);

            var ones = new int[res.Length - 1];
            var addition = new int[res.Length - 1];
            addition[addition.Length - 1] = 1;
            for (var i = 1; i < res.Length; i++) ones[i - 1] = Convert.ToInt32(Convert.ToString(res[i]));

            var result = AddBinary(ones, addition);

            res = res[0] + string.Join("", result);

            return res;
        }

        private static string FloatingPoint(double input)
        {
            var result = "";
            if (input < 0)
            {
                input = Math.Abs(input);
                result += "1";
            }
            else
            {
                result += "0";
            }

            result += IntBitConvert(input, true, false);

            var indexComma = result.IndexOf('.');
            if (indexComma < 0) indexComma = result.Length - 1;
            result = result.Remove(indexComma, 1);
            result = result.Insert(2, ".");
            result += $"*2^{(indexComma > 2 ? indexComma - 2 : 0)}";

            return result;
        }

        private static string FloatConvert32(double input)
        {
            int sign;

            if (input < 0)
                sign = 1;
            else
                sign = 0;

            input = Math.Abs(input);
            var bits = IntBitConvert(input, false, false);
            var normalized = "";
            if (bits.Length > 1)
                normalized = bits.Insert(1, ".");
            else
                normalized = "0";
            var newExp = exp + 127;
            var newDotPos = dotPos;
            var expBits = "";
            if (input == 0)
            {
                expBits = "00000000";
            }
            else if (input == 1)
            {
                expBits = "01111111";
            }
            else
            {
                expBits = IntBitConvert(newExp, false, false);
                if (expBits.Length > 8)
                    expBits = expBits.Substring(0, 8);
                else
                    while (expBits.Length < 8)
                        expBits = expBits.Insert(0, "0");
            }

            var mantissa = "";
            if (normalized.Length > 2)
                mantissa = normalized.Substring(2);
            if (mantissa.Length > 23)
                mantissa = mantissa.Substring(0, 23);
            while (mantissa.Length < 23)
                mantissa += "0";
            return sign + " " + expBits + " " + mantissa;
        }

        private static string IntBitConvert(double input, bool comma, bool fractionalPart)
        {
            var floor = Math.Floor(input);
            var frac = input - floor;

            var e = 0;
            while (Math.Pow(2, e) <= floor)
                e++;
            e = e - 1;
            var bits = "";
            double temp;
            if (input > 1 || input < 0)
            {
                bits += "1";

                temp = Math.Pow(2, e);
                for (var i = e - 1; i >= 0; i--)
                    if (temp + Math.Pow(2, i) <= floor)
                    {
                        temp += Math.Pow(2, i);
                        bits += "1";
                    }
                    else
                    {
                        bits += "0";
                    }

                exp = bits.Length - 1;
            }

            if (frac == 0 || fractionalPart)
                return bits;
            dotPos = bits.Length;
            temp = 0;
            e = -1;
            if (comma) bits += ".";
            while (temp <= frac && e > -80)
            {
                if (temp + Math.Pow(2, e) <= frac)
                {
                    temp += Math.Pow(2, e);
                    bits += "1";
                }

                else
                {
                    bits += "0";
                }

                e--;
            }

            temp = 1;
            if (input < 1 && input > 0)
            {
                for (var i = dotPos; i < bits.Length; i++)
                {
                    if (bits[i] == '1')
                    {
                        exp = (int) temp * -1;
                        break;
                    }

                    temp++;
                }

                if (input < .5)
                    bits = bits.Remove(0, (int) temp - 1);
            }

            return bits;
        }


        private static void Main(string[] args)
        {
            Console.WriteLine("Введите число для перевода в в прямой код:");
            var inp = Console.ReadLine();
            double result = 0;
            Console.WriteLine(!double.TryParse(inp, out result)
                ? "Invalid input."
                : SignedMagnitudeRepresentation(result));

            Console.WriteLine("Введите число для перевода в обратный код:");
            inp = Console.ReadLine();
            Console.WriteLine(!double.TryParse(inp, out result)
                ? "Invalid input."
                : OnesComplement(result));

            Console.WriteLine("Введите число для перевода в дополнительный код:");
            inp = Console.ReadLine();
            Console.WriteLine(!double.TryParse(inp, out result)
                ? "Invalid input."
                : TwosComplement(result));

            Console.WriteLine(
                "Введите число для перевода двоичный в код в формате с плавающей запятой, включающего знаки порядка и мантиссы: ");
            inp = Console.ReadLine();
            Console.WriteLine(!double.TryParse(inp, out result)
                ? "Invalid input."
                : FloatingPoint(result));

            Console.WriteLine("Введите число для перевода в формат одинарной точности по стандарту IEEE 754: ");
            inp = Console.ReadLine();
            Console.WriteLine(!double.TryParse(inp, out result)
                ? "Invalid input."
                : FloatConvert32(result));

            Console.ReadKey();
        }
    }
}