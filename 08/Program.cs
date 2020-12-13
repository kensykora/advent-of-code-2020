using System;
using System.Linq;
using System.IO;

namespace _08
{
    class Program
    {
        static Instruction[] Instructions;
        static void Main(string[] args)
        {
            Part1();
        }

        static void Part1()
        {
            Instructions =
                File.ReadAllLines("input.txt")
                .Select(x => new Instruction
                {
                    Op = (Operation)Enum.Parse(typeof(Operation), x.Substring(0, 3)),
                    Value = int.Parse(x.Substring(4))
                })
                .ToArray();

            Console.WriteLine(GetValue());
        }

        static int GetValue()
        {
            var result = 0;
            var current = 0;

            while(!Instructions[current].Visited)
            {
                var instruction = Instructions[current];
                instruction.Visited = true;

                switch (instruction.Op)
                {
                    case Operation.jmp:
                        current += instruction.Value;
                        break;
                    case Operation.acc:
                        result += instruction.Value;
                        current += 1;
                        break;
                    case Operation.nop:
                        current += 1;
                        break;
                }
            }

            return result;
        }
    }


    public class Instruction
    {
        public Operation Op { get; set; }
        public bool Visited { get; set; }
        public int Value { get; set; }
    }

    public enum Operation
    {
        nop,
        acc,
        jmp
    }
}
