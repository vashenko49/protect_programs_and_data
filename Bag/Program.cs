using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bag
{
    public class Item
    {
        private int Value { get; }
        private int Weight { get; }
        private bool IsChosen { get; set; }

        public Item(int value, int weight, bool isChosen)
        {
            Value = value;
            Weight = weight;
            IsChosen = isChosen;
        }
        public int GetValue()
        {
            return Value;
        }
        public int GetWeight()
        {
            return Weight;
        }
        public bool GetIsChosen()
        {
            return IsChosen;
        }
        public void GetIsChosen(bool isChosen)
        {
            IsChosen = isChosen;
        }
    }

    class Program
    {

        private static int _knapsackWeight;
        private static int _itemCount;
        private static int[,] _valuesMatrix;

        private static void KnapsackProcess(IReadOnlyList<Item> list)
        {
            for (var i = 1; i <= _itemCount; i++)
            {
                for (var j = 1; j <= _knapsackWeight; j++)
                {
                    if (list[i - 1].GetWeight() > j)
                        _valuesMatrix[i, j] = _valuesMatrix[i - 1, j];
                    else
                    {
                        _valuesMatrix[i, j] =
                            Math.Max(list[i - 1].GetValue() + _valuesMatrix[i - 1, j - list[i - 1].GetWeight()],
                                _valuesMatrix[i - 1, j]);
                    }
                }
            }
        }

        private static void PrepareValues(int knapsackWeight)
        {
            for (var i = 0; i <= _itemCount; i++)
            {
                for (var j = 0; j <= knapsackWeight; j++)
                    _valuesMatrix[i, j] = 0;
            }
        }

        private static List<Item> FindChosenItems(List<Item> list)
        {
            var indis = _itemCount;
            var weight = _knapsackWeight;
            while (indis > 0 && weight > 0)
            {
                if (_valuesMatrix[indis, weight] != _valuesMatrix[indis - 1, weight])
                {
                    list[indis - 1].GetIsChosen(true);
                    weight = weight - list[indis - 1].GetWeight();
                }
                indis--;
            }
            return list;
        }

        public static void Main(string[] args)
        {
            var list = new List<Item>();

            Console.Write("Enter Knapsack Max Weight: ");
            _knapsackWeight = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Item Count: ");
            _itemCount = Convert.ToInt32(Console.ReadLine());

            _valuesMatrix = new int[_itemCount + 1, _knapsackWeight + 1];

            for (var i = 1; i <= _itemCount; i++)
            {
                Console.Write("Enter " + i + ".Item Value: ");
                var value = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter " + i + ".Item Weight: ");
                var weight = Convert.ToInt32(Console.ReadLine());
                var item = new Item(value, weight, false);
                list.Add(item);
            }

            PrepareValues(_knapsackWeight);
            KnapsackProcess(list);

            for (var i = 0; i <= _itemCount; i++)
            {
                for (var j = 0; j <= _knapsackWeight; j++)
                {
                    Console.Write(_valuesMatrix[i, j]);
                    Console.Write("\t");
                }
                Console.WriteLine("\n");
            }

            var chosenList = FindChosenItems(list);

            Console.WriteLine("Maximum Value: " + _valuesMatrix[_itemCount, _knapsackWeight]);
            Console.WriteLine("Chosen Item(s)");

            for (var i = 1; i <= chosenList.Count; i++)
                if (chosenList[i - 1].GetIsChosen())
                    Console.WriteLine(i + ". Item - Value: " +
                                      chosenList[i - 1].GetValue() +
                                      " - " + "Weight: " + chosenList[i - 1].GetWeight());

            Console.ReadKey(true);
        }
    }
}
