using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homophonic_cipher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Homophonic cipher");

            Console.WriteLine("Enter word:");
            var s1 = Console.ReadLine();
            var s2 = string.Empty;
            if (s1 != null)
                foreach (var t in s1)
                {
                    if (t != ' ')
                        if (t == 1071)
                            s2 += (char)(1103);
                        else if (t == 1040)
                            s2 += (char)(1071);
                        else
                            s2 += (char)(t - 1);
                    else
                        s2 += t;
                }

            Console.WriteLine("Replaced word {0}", s2);
            Console.ReadKey(true);
        }
    }
}
