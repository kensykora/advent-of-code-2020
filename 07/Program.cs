using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace _07
{
    public class Bag
    {
        public string Color { get; set; }
        public (Bag bag, int qty)[] InnerBags { get; set; }
    }

    class Program
    {
        static Dictionary<string, Bag> bagList = new Dictionary<string, Bag>();
        static void Main(string[] args)
        {
            Part1();
            Part2();
        }

        static void Part2()
        {
            Console.WriteLine(getCount(bagList["shiny gold"]));
        }

        static Bag GetBag(string color)
        {
            if (!bagList.ContainsKey(color))
            {
                bagList.Add(color, new Bag { Color = color });
            }

            return bagList[color];
        }

        static (Bag, int qty)[] GetBags(string input)
        {
            var colorRegex = new Regex("(?<qty>\\d+) (?<bagColor>[\\w ]+) bag([s, ]+)?");
            var result = new List<(Bag, int)>();
            foreach (var bag in input.Split(", "))
            {
                var matches = colorRegex.Match(bag);
                result.Add((GetBag(matches.Groups["bagColor"].Value), int.Parse(matches.Groups["qty"].Value)));
            }

            return result.ToArray();
        }

        static void Part1()
        {
            var regex = new Regex("(?<containerColor>[\\w ]+) bags contain (?<containers>(?<qty>\\d+) (?<bagColor>[\\w ]+) bag([s, ]+)?)*(?<none>no other bags)*\\.");


            var input = File.ReadLines("input.txt");


            foreach (var bagLine in input)
            {
                var match = regex.Match(bagLine);

                var color = match.Groups["containerColor"].Value;
                var containers = match.Groups["containers"];
                var bag = GetBag(color);
                if (containers.Success)
                {
                    bag.InnerBags = GetBags(bagLine);
                }
                else
                {
                    bag.InnerBags = new (Bag bag, int qty)[0];
                }
            }

            var goldCount = 0;

            foreach (var bag in bagList.Values)
            {
                goldCount += getCount(bag, "shiny gold") > 0 ? 1 : 0;
            }

            Console.WriteLine(goldCount);
        }


        static int getCount(Bag bag, string color = null)
        {
            int result = 0;
            if (color != null)
            {

                foreach (var innerBag in bag.InnerBags)
                {
                    if (innerBag.bag.Color == color)
                    {
                        result += innerBag.qty;
                    }
                    else
                    {
                        result += getCount(innerBag.bag, color);
                    }
                }
            }
            else
            {
                foreach (var innerBag in bag.InnerBags)
                {
                    result += innerBag.qty + (getCount(innerBag.bag) * innerBag.qty);
                }
            }

            return result;
        }
    }
}
