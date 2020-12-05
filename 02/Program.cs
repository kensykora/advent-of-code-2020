using System;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace _02
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadLines("input.txt");
            var validPasswords = 0;
            foreach (var line in input)
            {
                var capture = Regex.Match(line, "(?<low>\\d*)-(?<high>\\d*) (?<letter>[a-zA-Z])\\: (?<password>[a-zA-Z]*)");
                var low = int.Parse(capture.Groups["low"].Value);
                var high = int.Parse(capture.Groups["high"].Value);
                var letter = capture.Groups["letter"].Value[0];
                var password = capture.Groups["password"].Value;

                var count = password.Count(x => x == letter);
                if (count < low || count > high)
                {
                    Console.WriteLine("Bad: " + line);
                }
                else
                {
                    validPasswords++;
                }
            }

            Console.WriteLine("Valid Passwords: " + validPasswords);
        }
    }
}
