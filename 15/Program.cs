using System;
using System.Collections.Generic;
using System.Linq;

namespace _15
{
    class Program
    {
        static void Main(string[] args)
        {
            //Solve("0,3,6", 10); // Sample
            //Solve("1,3,2", 2020); // Sample

            //Solve("18,11,9,0,5,1", 2020); // Pt1 input

            //Solve("0,3,6", 30000000); // Sample

            Solve("18,11,9,0,5,1", 30000000); // Pt2 input
        }

        static void Solve(string input, int target)
        {
            var gameLog = input.Split(',').Select(x => int.Parse(x)).ToList();
            //Console.Write(input);

            // Key = Number spoken, // Value = Index it references
            var values = new Dictionary<int, (int prev, int current)>();

            for (var i = 0; i < gameLog.Count; i++)
            {
                values[gameLog[i]] = (-1, i);
            }

            (int prev, int current) history;
            int next = 0;

            for (var j = gameLog.Count; j < target; j++)
            {
                history = values[gameLog[j - 1]];
                next = history.prev == -1
                    ? 0
                    : history.current - history.prev;

                history = values.ContainsKey(next)
                    ? (values[next].current, j)
                    : (-1, j);

                gameLog.Add(next);
                values[next] = history;

                //Console.Write("," + next);
            }

            Console.WriteLine(next);
        }
    }
}
