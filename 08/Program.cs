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
            Instructions =
                File.ReadAllLines("input.txt")
                .Select(x => new Instruction
                {
                    Op = (Operation)Enum.Parse(typeof(Operation), x.Substring(0, 3)),
                    Value = int.Parse(x.Substring(4))
                })
                .ToArray();

            Part1();
            Part2();
        }

        static void Part1()
        {
            Console.WriteLine(GetValue().result);
        }

        static void Part2()
        {
            var jmps = Instructions.Where(x => x.Op == Operation.jmp);
            foreach(var jmp in jmps)
            {
                jmp.Op = Operation.nop;

                var newResult = GetValue();

                if (!newResult.didLoop)
                {
                    Console.WriteLine(newResult.result);
                    return;
                }

                jmp.Op = Operation.jmp;
                
                foreach(var i in Instructions)
                {
                    i.Visited = false;
                }
            }

            var nops = Instructions.Where(x => x.Op == Operation.nop);
            foreach(var nop in nops)
            {
                nop.Op = Operation.jmp;

                var newResult = GetValue();

                if (!newResult.didLoop)
                {
                    Console.WriteLine(newResult.result);
                    return;
                }

                nop.Op = Operation.nop;

                foreach(var i in Instructions)
                {
                    i.Visited = false;
                }
            }

            Console.WriteLine("Did not find");
        }

        static (int result, bool didLoop) GetValue()
        {
            var result = 0;
            var current = 0;

            while(current < Instructions.Length && !Instructions[current].Visited)
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

            return (result, current < Instructions.Length);
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
