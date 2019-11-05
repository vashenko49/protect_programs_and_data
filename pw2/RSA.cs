using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pw2
{
    public class RSA
    {
        internal static int[] primeNumbers = new int[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47 };
        internal static Random random;
        internal static int f;
        internal static int p;
        internal static int q;
        internal static int n;
        internal static int e;
        internal static int d;
        internal static int h;
        internal static int s;

        static RSA()
        {
            random = new Random(DateTimeHelper.CurrentUnixTimeMillis());
            p = getPrimeByIndex(random.Next(3 + (primeNumbers.Length - 3)));
            do
            {
                q = getPrimeByIndex(3 + (random.Next(primeNumbers.Length - 3)));
            } while (p == q);
            n = p * q;
            f = (p - 1) * (q - 1);
            for (int i = 0; ; i++)
            {
                if (f % getPrimeByIndex(i) != 0)
                {
                    e = getPrimeByIndex(i);
                    break;
                }
            }

            for (int i = 1; ; i++)
            {
                if ((i * e) % f == 1)
                {
                    d = i;
                    break;
                }
            }
        }
        private static int getPrimeByIndex(int index)
        {
            return primeNumbers[index];
        }
        private static void setHash()
        {
            h = 12;
        }
        private static int getHash(int h, int d)
        {
            int degree = h;
            for (int i = 0; i < d - 1; i++)
            {
                h = (int)h * degree;
                h = h % n;
            }

            return h;
        }

        public static void checkECP()
        {
            setHash();
            s = getHash(h, d);
            Console.WriteLine("Пользователь Алиса генерирует ключи:");
            Console.WriteLine("e: " + e);
            Console.WriteLine("d: " + d);
            Console.WriteLine("n: " + n);
            Console.WriteLine("\nВычисление хэш-образа сообщения:");
            Console.WriteLine("h(T): " + h);
            Console.WriteLine("\nВыработка цифровой подписи:");
            Console.WriteLine("s: " + s);
            Console.WriteLine("\nПользователь Алиса отправляет исходное \nсообщение и цифровую подпись пользователю Бобу");
            Console.WriteLine("\nПользователь Боб вычисляет хэш-образ \nпо полученному сообщению:");
            Console.WriteLine("h' = h(T') = " + h);
            Console.WriteLine("\nВычисление хэш-образа из цифровой подписи h:");
            Console.WriteLine("h: " + getHash(s, e));
            Console.WriteLine("\nСравнение h' и h: ");
            Console.WriteLine("h' = h (" + h + " = " + getHash(s, e) + ") ?\n");
            if (h == getHash(s, e))
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
