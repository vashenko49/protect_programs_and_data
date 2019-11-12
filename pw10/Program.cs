using java.lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringBuilder = System.Text.StringBuilder;

namespace pw10
{
    class Program
    {
        public static void Main(string[] args)
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUWXYZ";

            string lastName = "LYUZNYAK";

            string full = "LYUZNYAKKLYMKO";

            int[] positions = new int[full.Length];

            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = alphabet.IndexOf(full[i]) + 1;
            }

            Bits(lastName);

            Lun(MakeArray(positions, 14));
            EAN13(MakeArray(positions, 13));
            INN(MakeArray(positions, 10));
            RailwayTransport(MakeArray(positions, 5));

            CRC(new int[] { alphabet.IndexOf(lastName[0]) + 1, alphabet.IndexOf(lastName[1]) + 1, alphabet.IndexOf(lastName[2]) + 1 });

            string message = lastName.Substring(0, 2);
            sbyte[] messageInASCII = GetBytes(message);
            string newMessage = "";


            for (int i = 0; i < messageInASCII.Length; i++)
            {
                newMessage += Integer.toBinaryString(Convert.ToInt32(messageInASCII[i]));
            }

            newMessage = newMessage.Substring(0, 11);

            ECC(newMessage);


            Console.ReadLine();

        }


        private static void ECC(string newMessage)
        {
            Console.WriteLine("Алгоритм ECC. Входные данные:");
            Console.WriteLine(newMessage);

            string result = "";

            result = "11" + newMessage[0] + "0" + newMessage.Substring(1, 2) + "0" + newMessage.Substring(4, (newMessage.Length - 4) - 4);

            int pb = 0;

            for (int i = 0; i < result.Length; i++)
            {
                pb += Convert.ToInt32(result[i].ToString());
            }

            pb %= 2;
            Console.WriteLine(result + ", pb = " + pb);
        }

        private static void CRC(int[] v)
        {
            Console.WriteLine("G(x) = x^4 + x^1 + x^0");
            Console.WriteLine("Алгоритм CRC. Входные данные:");

            foreach (int item in v)
            {
                Console.Write(item + "\t");
            }
            Console.WriteLine();

            string dect = "1011";
            Console.WriteLine("dect = " + dect + "(" + Integer.toBinaryString(1011) + ")"); //dect.toString(2); Integer. Convert.ToInt32(dect, 2) + ")");
            Console.WriteLine();

            Console.WriteLine("Делимое \t Формула \t Частное \t Остаток \t Результат");


            for (int i = 0; i < v.Length; i++)
            {
                string input = Integer.toBinaryString(v[i]);

                string input2 = input + "0000";

                string chastnoe = GetChastnoeAndOstatokForCRC(input2, dect)[0];
                string ostatok = GetChastnoeAndOstatokForCRC(input2, dect)[1];

                int input2Integer = Convert.ToInt32(input2);

                int ostatokInteger = Convert.ToInt32(ostatok, 2);

                int chastnoeInteger = Convert.ToInt32(chastnoe, 2);

                string result = input + ostatok;

                int finalRes = Convert.ToInt32(result, 2);

                Console.WriteLine((v[i] + "(" + input + ")" + "\t" + input2 + "(" + input2Integer + ")" + "\t" + chastnoe + "(" + chastnoeInteger + ")" + "\t" + ostatok + "(" + ostatokInteger + ")" + "\t" + result + "(" + finalRes + ")"));
            }
        }

        private static string[] GetChastnoeAndOstatokForCRC(string imya2, string dect)
        {
            string delit, resultat = "";
            bool f = true;
            StringBuilder sb;
            do
            {
                delit = "";
                int l1 = imya2.Length - 1;
                int l2 = dect.Length - 1;
                int raz = l1 - l2;

                string dect2 = dect;
                for (int i = 0; i < raz; i++)
                {
                    dect2 = dect2 + "0";
                }

                for (int i = 0; i <= l1; i++)
                {
                    delit = delit + (Convert.ToInt32(Integer.toBinaryString(imya2[i])) ^ Convert.ToInt32(Integer.toBinaryString(dect2[i]))).ToString();
                }

                int counter = 0;

                bool h = true;
                if (delit.IndexOf('1') >= 0)
                {
                    do
                    {
                        if (delit[1] != '0')
                        {
                            h = false;
                        }
                        if (delit[0] == '0' && delit.Length >= dect.Length)
                        {
                            delit = delit.Substring(1, (delit.Length - 1) - 1);
                            counter++;
                        }
                        else
                        {
                            h = false;
                        }
                    } while (h);
                }
                else
                {
                    imya2 = "0000";
                    sb = new StringBuilder("1");
                    for (int i = 0; i < dect.Length - 1; i++)
                    {
                        sb.Append('0');
                    }
                    resultat = sb.ToString();
                    f = false;
                    break;
                }
                sb = new StringBuilder("1");
                for (int i = 0; i < counter - 1; i++)
                {
                    sb.Append('0');
                }
                resultat += sb.ToString();


                if (delit.Length < dect.Length)
                {
                    f = false;
                }

                imya2 = delit;
            } while (f);

            return new string[] { resultat, imya2 };
        }

        private static void RailwayTransport(int[] v)
        {
            Console.WriteLine("Алгоритм для кодов станций на ж/д транспорте. Входные данные:");

            foreach (int item in v)
            {
                Console.Write(item + "\t");
            }

            Console.WriteLine();

            int controlNumber = (1 * v[0] + 2 * v[1] + 3 * v[2] + 4 * v[3]) % 11;

            if (controlNumber == 10)
            {
                controlNumber = (3 * v[0] + 4 * v[1] + 5 * v[2] + 6 * v[3]) % 11;

                if (controlNumber == 10)
                {
                    controlNumber = 0;
                }
            }

            Console.WriteLine("Контрольная цифра должна равняться  " + controlNumber);
        }

        private static void INN(int[] v)
        {
            Console.WriteLine("Алгоритм ИНН. Входные данные:");

            foreach (int item in v)
            {
                Console.Write(item + "\t");
            }

            Console.WriteLine();

            int result = ((2 * v[0] + 4 * v[1] + 10 * v[2] + 3 * v[3] + 5 * v[4] + 9 * v[5] + 5 * v[6] + 6 * v[7] + 8 * v[8]) % 11) % 10;

            Console.WriteLine("Контрольная цифра должна равняться  " + result);
        }

        private static void EAN13(int[] v)
        {
            Console.WriteLine("Алгоритм EAN-13. Входные данные:");

            foreach (int item in v)
            {
                Console.Write(item + "\t");
            }

            Console.WriteLine();

            int sumNeChet = 0;
            int sumChet = 0;

            for (int i = 1; i <= v.Length; i++)
            {
                if (i % 2 == 0)
                {
                    sumChet += v[i - 1];
                }
                else if (i != v.Length)
                {
                    sumNeChet += v[i - 1];
                }
            }

            sumChet *= 3;

            int result = 0;

            for (int i = 0; ; i++)
            {
                if ((sumChet + sumNeChet + i) % 10 == 0)
                {
                    result = i;
                    break;
                }
            }


            Console.WriteLine("Контрольная цифра должна равняться  " + result);
        }

        private static void Lun(int[] v)
        {
            Console.WriteLine("Алгоритм Луна. Входные данные:");

            foreach (int item in v)
            {
                Console.Write(item + "\t");
            }

            Console.WriteLine();

            int sumNeChet = 0;
            int sumChet = 0;

            for (int i = 1; i <= v.Length; i++)
            {
                if (i % 2 == 0)
                {
                    v[i - 1] = (v[i - 1] * 2) % 9;

                    sumChet += v[i - 1];
                }
                else
                {
                    sumNeChet += v[i - 1];
                }
            }

            int result = 0;

            for (int i = 0; ; i++)
            {
                if ((sumChet + sumNeChet + i) % 10 == 0)
                {
                    result = i;
                    break;
                }
            }


            Console.WriteLine("Контрольная цифра должна равняться " + result);
        }
        private static sbyte[] GetSBytesForEncoding(System.Text.Encoding encoding, string s)
        {
            sbyte[] sbytes = new sbyte[encoding.GetByteCount(s)];
            encoding.GetBytes(s, 0, s.Length, (byte[])(object)sbytes, 0);
            return sbytes;
        }

        internal static sbyte[] GetBytes(string self)
        {
            return GetSBytesForEncoding(System.Text.Encoding.UTF8, self);
        }

        private static void Bits(string lastName)
        {
            Console.WriteLine("Биты четности");
            Console.WriteLine("Буква \t Битовая строка \t Четный бит \t Нечетный бит");
            foreach (char item in lastName.ToCharArray())
            {
                sbyte[] messageInASCII = GetBytes( item.ToString()); //new char[] { item }).;Encoding.ASCII.GetBytes

                string newMessage = "";

                for (int i = 0; i < messageInASCII.Length; i++)
                {
                    newMessage += Integer.toBinaryString(Convert.ToInt32(messageInASCII[i]));
                }

                int EvenBit = (newMessage.Length - newMessage.Replace("1", "").Length) % 2;

                int OddBit = EvenBit == 1 ? 0 : 1;

                Console.WriteLine(string.Format("{0} \t {1} \t {2:D} \t {3:D}", item, newMessage, EvenBit, OddBit));
            }

        }

        private static int[] MakeArray(int[] array, int count)
        {
            int[] result = new int[count];

            for (int i = 0; i < count; i++)
            {
                result[i] = array[i];
            }

            return result;
        }
    }
}
