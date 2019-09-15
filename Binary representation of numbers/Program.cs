using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binary_representation_of_numbers
{

    class Program
    {
        public static int[] AddBinary(int[] op1, int[] op2)
        {
            if (op1.Length != op2.Length)
            {
                throw new System.InvalidOperationException("Arrays must be equal in length");
            }
            int[] result = new int[op1.Length];
            for (int i = 1; i < op1.Length + 1; i++)
            {
                if (op1[op1.Length - i] > 1 || op2[op2.Length - i] > 1 || op1[op1.Length - i] < 0 || op2[op2.Length - i] < 0)
                {
                    throw new System.ArgumentOutOfRangeException("Array has a number that is invalid in it, argument out of range.");
                }
                else
                {
                    result[result.Length - i] = op1[op1.Length - i] + op2[op2.Length - i] + result[result.Length - i];
                    if (result[result.Length - i] == 2)
                    {
                        if (result.Length - i - 1 < 0)
                        {
                            string message = "Binary numbers added together have grown beyond the range of the array.";
                            string caption = "Error, overflow.";
                            Console.WriteLine(message + " " + caption);
                            int[] escape = new int[1];
                            return escape;
                        }
                        try
                        {
                            result[result.Length - i] = 0;
                            result[result.Length - i - 1] = 1;
                        }
                        catch { }
                    }
                    if (result[result.Length - i] == 3)
                    {
                        if (result.Length - i - 1 < 0)
                        {
                            string message = "Binary numbers added together have grown beyond the range of the array.";
                            string caption = "Error, overflow.";
                            Console.WriteLine(message + " " + caption);
                            int[] escape = new int[1];
                            return escape;
                        }
                        else
                        {
                            try
                            {
                                result[result.Length - i] = 1;
                                result[result.Length - i - 1] = 1;
                            }
                            catch { }
                        }
                    }
                }
            }
            return result;
        }
    

        static int exp = 0;
        static int dotPos = 0;

        static string FloatConvert32(double input)
        {
            int sign;

            if (input < 0)
                sign = 1;
            else
                sign = 0;

            input = Math.Abs(input);
            string bits = IntBitConvert(input);
            string normalized = "";
            if (bits.Length > 1)
                normalized = bits.Insert(1, ".");
            else
                normalized = "0";
            int newExp = exp + 127;
            int newDotPos = dotPos;
            string expBits = "";
            if (input == 0)
                expBits = "00000000";
            else if (input == 1)
                expBits = "01111111";
            else
            {
                expBits = IntBitConvert(newExp);
                if (expBits.Length > 8)
                    expBits = expBits.Substring(0, 8);
                else
                {
                    while (expBits.Length < 8)
                        expBits = expBits.Insert(0, "0");
                }
            }
            string mantissa = "";
            if (normalized.Length > 2)
                mantissa = normalized.Substring(2);
            if (mantissa.Length > 23)
                mantissa = mantissa.Substring(0, 23);
            while (mantissa.Length < 23)
                mantissa += "0";
            return sign + " " + expBits + " " + mantissa;

        }

        static String IntBitConvert(double input)
        {
            double floor = Math.Floor(input);
            double frac = input - floor;

            int e = 0;
            while (Math.Pow(2, e) <= floor)
                e++;
            e = e - 1;                           
            string bits = "";
            double temp;
            if (input > 1 || input < 0)
            {
                bits += "1";

                temp = Math.Pow(2, e);
                for (int i = e - 1; i >= 0; i--)
                {
                    if (temp + Math.Pow(2, i) <= floor)
                    {
                        temp += Math.Pow(2, i);
                        bits += "1";
                    }
                    else
                        bits += "0";
                }
                exp = bits.Length - 1;
            }

            if (frac == 0)
                return bits;
            dotPos = bits.Length;
            temp = 0;
            e = -1;
            bits += ".";
            while (temp <= frac && e > -80)
            {
                if (temp + Math.Pow(2, e) <= frac)
                {
                    temp += Math.Pow(2, e);
                    bits += "1";
                }

                else
                    bits += "0";
                e--;
            }
            temp = 1;
            if (input < 1 && input > 0)
            {
                for (int i = dotPos; i < bits.Length; i++)
                {
                    if (bits[i] == '1')
                    {
                        exp = (int)temp * -1;
                        break;
                    }
                    temp++;
                }
                if (input < .5)
                    bits = bits.Remove(0, (int)temp - 1);
            }
            return bits;
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Enter");
            string inp = Console.ReadLine();
            double result = 0;
            if (!Double.TryParse(inp, out result))
                Console.WriteLine("Invalid input.");
            else
            {
                string f32 = FloatConvert32(result);
                Console.WriteLine(f32);
            }

            double dfg = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine(IntBitConvert(dfg));

            int[] one = new[] {0, 0, 0, 1, 1};
            int[] two = new int[one.Length];
            two[one.Length - 1] = 1;
            int[] temp = AddBinary(one, two);


            Console.WriteLine(string.Join("",one));
            Console.WriteLine(string.Join("",two));
            Console.WriteLine(string.Join("",temp));
            Console.ReadKey();

        }
    }
}
