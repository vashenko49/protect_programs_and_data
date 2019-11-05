using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace pw2
{
    public class HOST94
    {
        private static int p = 79;
        private static int q = 13;
        private static int a;
        private static int x;
        private static int y;
        private static int h = 21;
        private static int w;
        private static int k;
        private static int s = 0;
        private static int ws = 0;
        private static int v;
        private static int z1;
        private static int z2;
        private static double u;
        internal static Random random;

        static HOST94()
        {
            random = new Random();
            a = random.Next(p - 3) + 2;
            while (getHash(a, q, p) % p != 1)
            {
                a = random.Next(p - 3) + 2;
            }
            x = random.Next(q - 2) + 2;
            y = getHash(a, x, p);
        }
        private static int getHash(int h, int d, int p)
        {
            int degree = h;
            for (int i = 0; i < d - 1; i++)
            {
                h = (int)h * degree;
                h = h % p;
            }

            return h;
        }
        public static void sendMessage()
        {
            while (s == 0)
            {
                while (ws == 0)
                {
                    k = random.Next(q - 2) + 2;
                    w = getHash(a, k, p);
                    ws = w % q;
                }
                s = (x * ws + k * h) % q;
            }
        }
        public static void getMessage()
        {
            v = getHash(h, q - 2, q);
            z1 = (s * v) % q;
            z2 = ((q - ws) * v) % q;
            u = Math.Pow(a, z1);
            u = u * Math.Pow(y, z2);
            u = u % (p);
            u = u % (q);
        }
        public static void checkECP()
        {
            Console.WriteLine("Пользователь Алиса генерирует ключи:");
            Console.WriteLine("p: " + p);
            Console.WriteLine("q: " + q);
            Console.WriteLine("a: " + a);
            Console.WriteLine("x: " + x);
            Console.WriteLine("y: " + y);

            Console.WriteLine("k: " + k);
            Console.WriteLine("w: " + w);
            Console.WriteLine("w': " + ws);
            Console.WriteLine("\nВычисление хэш-образа сообщения:");
            Console.WriteLine("h(T): " + h);
            sendMessage();
            Console.WriteLine("\nВыработка цифровой подписи:");
            Console.WriteLine("s: " + s);
            Console.WriteLine("\nПользователь Алиса отправляет исходное \nсообщение и цифровую подпись пользователю Бобу");
            Console.WriteLine();
            Console.WriteLine("\nПользователь Боб вычисляет хэш-образ \nпо полученному сообщению:");
            Console.WriteLine("h' = h(T') = " + h);
            Console.WriteLine("v: " + v);
            Console.WriteLine("u: " + u);
            if (u == ws)
            {
                Console.WriteLine("Проверка сертификата прошла успешно");
            }
            else
            {
                Console.WriteLine("Проверка сертификата провалилась");
            }
        }
    }

}
