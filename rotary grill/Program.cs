using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rotary_grill
{
    class Program
    {
        static void fillingInIable(ref Boolean[,] tempBools, ref char[,] array, ref string phrase, ref int position, ref int h)
        {
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    if (tempBools[i, j])
                    {
                        array[i, j] = position>=phrase.Length?'-':phrase[position++];
                    }
                }
            }
        }

        static void writeArray(ref Boolean [,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j] + " ");
                }

                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void writeArray(ref char[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j] + " ");
                }

                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void replaseColom(ref Boolean[,] array, int first, int second)
        {
            Boolean[] temp = new Boolean[array.GetLength(0)];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                temp[i] = array[i, first];
                array[i, first] = array[i, second];
                array[i, second] = temp[i];

            }
        }

        static void replaseLine(ref Boolean[,] array, int first, int second)
        {
            Boolean[] temp = new Boolean[array.GetLength(1)];
            for (int i = 0; i < array.GetLength(1); i++)
            {
                temp[i] = array[first, i];
                array[first, i] = array[second, i];
                array[second, i] = temp[i];

            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("rotary grill поворотной решетки");
            string phrase = Console.ReadLine();

            int h = Convert.ToInt32(Math.Ceiling(Math.Sqrt(phrase.Length)))%2==0? Convert.ToInt32(Math.Ceiling(Math.Sqrt(phrase.Length))): Convert.ToInt32(Math.Ceiling(Math.Sqrt(phrase.Length)))+1;


            Boolean[,] template = new bool[h,h];

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    template[i, j] = false;
                }
            }

            Random rnd= new Random();

            int count = 0;
            int pre = 0;
            for (int i = 0; i < h; i++)
            {
                while (count < h)
                {
                    int x = rnd.Next(i, h - 1);
                    if (!template[i, x]&& pre!=x)
                    {
                        pre = x;
                        template[i, x] = true;
                        count++;
                        break;
                    }
                }
            }


            writeArray(ref template);


            char[,] table = new char[h, h];



            count = 0;
            fillingInIable(ref template, ref table, ref phrase, ref count, ref h);
            writeArray(ref template);
            writeArray(ref table);

            for (int i = 0, j=h-1; i <h/2; i++, j--)
            {
                replaseColom(ref template,i,j);
            }
            fillingInIable(ref template, ref table, ref phrase, ref count, ref h);
            writeArray(ref template);
            writeArray(ref table);


            for (int i = 0, j = h - 1; i < h / 2; i++, j--)
            {
                replaseLine(ref template, i, j);
            }
            fillingInIable(ref template, ref table, ref phrase, ref count, ref h);
            writeArray(ref template);
            writeArray(ref table);

            for (int i = 0, j = h - 1; i < h / 2; i++, j--)
            {
                replaseColom(ref template, j, i);
            }
            fillingInIable(ref template, ref table, ref phrase, ref count, ref h);

            writeArray(ref template);
            writeArray(ref table);

            Console.ReadKey();
        }
    }
}
