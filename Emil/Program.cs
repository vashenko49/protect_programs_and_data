using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emil
{
    class Program
    {
        private static string _strIn = string.Empty;
        private static string _txtBPublicKey = string.Empty;
        private static string _txtBSecretKey = string.Empty;

        private static int Rand()
        {
            var random = new Random();
            return random.Next();
        }
        private static int Power(int a, int b, int n)
        { // a^b mod n - возведение a в степень b по модулю n
            var tmp = a;
            var sum = tmp;
            for (var i = 1; i < b; i++)
            {
                for (var j = 1; j < a; j++)
                {
                    sum += tmp;
                    if (sum >= n)
                        sum -= n;
                }
                tmp = sum;
            }
            return tmp;
        }
        private static int Mul(int a, int b, int n)
        { // a*b mod n - умножение a на b по модулю n
            var sum = 0;
            for (var i = 0; i < b; i++)
            {
                sum += a;
                if (sum >= n)
                    sum -= n;
            }
            return sum;
        }
        private static string Crypt(int p, int g, int x)
        {
            var txtBCrypt = string.Empty;
            var y = Power(g, x, p);
            _txtBPublicKey = "Открытый ключ (p,g,y) = (" + p + "," + g + "," + y + ")";
            _txtBSecretKey = "Закрытый ключ x = " + x;
            if (_strIn.Length <= 0) return txtBCrypt;
            var temp = _strIn.ToCharArray();
            for (var i = 0; i <= _strIn.Length - 1; i++)
            {
                var m = (int)temp[i];
                if (m <= 0) continue;
                var k = Rand() % (p - 2) + 1;
                var a = Power(g, k, p);
                var b = Mul(Power(y, k, p), m, p);
                txtBCrypt = txtBCrypt + a + " " + b + " ";
            }


            return txtBCrypt;
        }
        public static void Main(string[] args)
        {
            var p = Convert.ToInt32(Rand());
            var g = Convert.ToInt32(Rand());
            var x = Convert.ToInt32(Rand());
            Console.Write("Enter text to encrypt\t");
            _strIn = Console.ReadLine();

            Console.Write($"p = {p}\n");
            Console.Write($"g = {g}\n");
            Console.Write($"x = {x}\n");

            Console.WriteLine($"\nEncrypted text\t{Crypt(p, g, x)}");
            Console.ReadKey(true);
        }
    }
}
