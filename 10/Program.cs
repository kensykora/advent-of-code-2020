using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _10
{
    class Program
    {
        static void Main(string[] args)
        {
            var input =
                File.ReadAllLines("input.txt")
                .Select(x => int.Parse(x))
                .ToArray();

            Part1(input);
            Part2(input);
        }

        static void Part1(int[] input)
        {
            var current = 0;
            var oneJoltDiffs = 0;
            var threeJoltDiffs = 0;

            var list = input.OrderBy(x => x).ToList();
            list.Add(list.Last() + 3);

            foreach (var adapter in list)
            {
                var diff = adapter - current;
                if (diff == 1)
                {
                    oneJoltDiffs++;
                }
                else if (diff == 3)
                {
                    threeJoltDiffs++;
                }
                current = adapter;
            }

            Console.WriteLine(oneJoltDiffs * threeJoltDiffs);
        }

        static void Part2(int[] input)
        {
            input = input.OrderBy(x => x).ToArray();
            var root = new Node(0, input);
            Console.WriteLine(root.CountLeaves());
        }
    }

    public class Node
    {
        static Dictionary<int, Node> NodeMap = new Dictionary<int, Node>();

        public Node(int value, int[] input, int start = 0)
        {
            Value = value;
            int i = start;
            Options = new List<Node>();

            while (i < input.Length && input[i] <= value + 3)
            {
                if (!NodeMap.ContainsKey(input[i]))
                {
                    NodeMap.Add(input[i], new Node(input[i], input, i + 1));
                }
                Options.Add(NodeMap[input[i]]);
                i++;
            }
        }
        public int Value { get; set; }

        public List<Node> Options { get; set; }

        public bool IsLeaf => !Options.Any();

        private ulong? leaves = (ulong?)null;

        public ulong CountLeaves()
        {
            if (leaves.HasValue)
            {
                return leaves.Value;
            }

            ulong result = 0;
            foreach (var node in Options)
            {
                if (node.IsLeaf)
                {
                    result += 1;
                }
                else
                {
                    result += node.CountLeaves();
                }
            }

            leaves = result;

            return leaves.Value;
        }
    }
}
