using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rotary_grill2
{
    class Program
    {
        static string SwivelCipherEncryption(string str)
        {
            int[,] stencil = { { 1, 0, 1, 0 }, { 0, 0, 0, 0 }, { 0, 1, 0, 1 }, { 0, 0, 0, 0 } };
            string[,] table = new string[4, 4];
            for (int n = 1, m = 0; n <= 4; n++)
            {
                for (int i = 0; i < stencil.GetLength(0); i++)
                {
                    for (int j = 0; j < stencil.GetLength(1); j++)
                    {
                        if (stencil[i, j] == 1)
                        {
                            if (m < str.Length)
                            {
                                table[i, j] = str[m].ToString();
                                m++;
                            }
                            else table[i, j] = " ";
                        }
                    }
                }
                stencil = rotateStencil(stencil, n);
            }
            string result = "";
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    result += table[j, i];
                }
            }
            return result.Trim();
        }

        static int[,] rotateStencil(int[,] stencil, int number)
        {
            switch (number)
            {
                case 1:
                    for (int i = 0; i < stencil.GetLength(0); i++)
                    {
                        for (int j = 0; j < stencil.GetLength(1); j++)
                        {
                            if (i == 0 && stencil[i, j] == 1)
                            {
                                stencil[i, j] = 0;
                                stencil[i, j + 1] = 1;
                                j++;
                            }
                            else if (i == 2 && stencil[i, j] == 1)
                            {
                                stencil[i, j] = 0;
                                stencil[i, j - 1] = 1;
                            }
                        }
                    }
                    return stencil;
                case 2:
                    for (int i = 0; i < stencil.GetLength(0); i++)
                    {
                        for (int j = 0; j < stencil.GetLength(1); j++)
                        {
                            if (i == 0 && stencil[i, j] == 1)
                            {
                                stencil[i, j] = 0;
                                stencil[i + 1, j - 1] = 1;
                            }
                            else if (i == 2 && stencil[i, j] == 1)
                            {
                                stencil[i, j] = 0;
                                stencil[i + 1, j + 1] = 1;
                            }
                        }
                    }
                    return stencil;
                case 3:
                    for (int i = 0; i < stencil.GetLength(0); i++)
                    {
                        for (int j = 0; j < stencil.GetLength(1); j++)
                        {
                            if (i == 1 && stencil[i, j] == 1)
                            {
                                stencil[i, j] = 0;
                                stencil[i, j + 1] = 1;
                                j++;
                            }
                            else if (i == 3 && stencil[i, j] == 1)
                            {
                                stencil[i, j] = 0;
                                stencil[i, j - 1] = 1;
                                j++;
                            }
                        }
                    }
                    return stencil;
                default: return stencil;
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("rotary grill поворотной решетки");
            string phrase = Console.ReadLine();
            string swivel_cipher_encryption = SwivelCipherEncryption(phrase);
            Console.WriteLine(swivel_cipher_encryption);
            Console.ReadLine();

        }
    }
}
