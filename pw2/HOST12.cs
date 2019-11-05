using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pw2
{
    public class HOST12
    {
        private static int h = 26;
        private static int e;
        private static int es;
        private static int rs;
        private static int q = 47;
        private static int k;
        private static int xp = 7;
        private static int yp = 17;
        private static int xc = 16;
        private static int yc = 16;
        private static int r = 0;
        private static int s = 0;
        private static int d = 10;
        private static int v;
        private static int z1;
        private static int z2;
        private static int xcs = 16;
        private static int ycs = 16;
        internal static Random random;
        static HOST12()
        {
            random = new Random();
        }
        public static void sendMessage()
        {
            e = h % q;
            while (s == 0)
            {
                while (r == 0)
                {
                    k = random.Next(q - 2) + 2;
                    r = xc % q;
                }
                s = (r * d + k * e) % q;
            }
        }
        public static void getMessage()
        {
            es = h % q;
            es = 27;
            v = es % q;
            z1 = (s * v) % q;
            z2 = ((q - r) * v) % q;
            rs = xcs % q;
        }
        public static void checkECP()
        {
            Console.WriteLine("Пользователь Алиса генерирует ключи:");
            sendMessage();
            Console.WriteLine("e: " + e);
            Console.WriteLine("d: " + d);
            Console.WriteLine("k: " + k);
            Console.WriteLine("Точка эллиптической кривой C(xc, yc) =  (" + xc + ", " + yc + ")");
            Console.WriteLine("r: " + r);
            Console.WriteLine("\nВычисление хэш-образа сообщения:");
            Console.WriteLine("h(T): " + h);
            sendMessage();
            Console.WriteLine("\nВыработка цифровой подписи:");
            Console.WriteLine("s: " + s);
            Console.WriteLine("\nПользователь Алиса отправляет исходное \nсообщение и цифровую подпись пользователю Бобу");
            Console.WriteLine();
            Console.WriteLine("\nПользователь Боб вычисляет хэш-образ \nпо полученному сообщению:");
            Console.WriteLine("h' = h(T') = " + h);
            Console.WriteLine("e': " + es);
            Console.WriteLine("z1: " + z1);
            Console.WriteLine("z2: " + z2);
            Console.WriteLine("v: " + v);
            Console.WriteLine("Точка эллиптической кривой C'(xc', yc') =  (" + xcs + ", " + ycs + ")");
            Console.WriteLine("r': " + rs);
            if (r == rs)
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
