using System;
using System.IO;
using System.Linq;

namespace _04
{
    class Program
    {
        static void Main(string[] args)
        {
            Part1();
        }

        static void Part1()
        {
            var input = File.ReadAllText("input.txt");
            var passports = input.Split("\r\n\r\n");

            var validPassports = 0;
            foreach (var passport in passports)
            {
                var data = passport.Split(new[] { " ", "\r\n" }, StringSplitOptions.None);
                var dict = data.ToDictionary(x => x.Split(':')[0], y => y.Split(':')[1]);

                /*
                byr (Birth Year)
                iyr (Issue Year)
                eyr (Expiration Year)
                hgt (Height)
                hcl (Hair Color)
                ecl (Eye Color)
                pid (Passport ID)
                cid (Country ID)
                */

                if (
                    dict.ContainsKey("byr")
                    && dict.ContainsKey("iyr")
                    && dict.ContainsKey("eyr")
                    && dict.ContainsKey("hgt")
                    && dict.ContainsKey("hcl")
                    && dict.ContainsKey("ecl")
                    && dict.ContainsKey("pid"))
                {
                    validPassports++;
                }
            }

            Console.WriteLine("Valid Passports: " + validPassports);
        }
    }
}
