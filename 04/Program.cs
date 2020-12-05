using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _04
{
    class Program
    {
        static void Main(string[] args)
        {
            //Part1();
            Part2();
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

        static void Part2()
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

                /*
                byr (Birth Year) - four digits; at least 1920 and at most 2002.
                iyr (Issue Year) - four digits; at least 2010 and at most 2020.
                eyr (Expiration Year) - four digits; at least 2020 and at most 2030.
                */
                if (
                    dict.ContainsKey("byr") && int.TryParse(dict["byr"], out var byr) && byr >= 1920 && byr <= 2002
                    && dict.ContainsKey("iyr") && int.TryParse(dict["iyr"], out var iyr) && iyr >= 2010 && iyr <= 2020
                    && dict.ContainsKey("eyr") && int.TryParse(dict["eyr"], out var eyr) && eyr >= 2020 && eyr <= 2030
                    && dict.ContainsKey("hgt") 
                    && dict.ContainsKey("hcl")
                    && dict.ContainsKey("ecl")
                    && dict.ContainsKey("pid"))
                {
                    // hgt (Height) - a number followed by either cm or in:
                    //     If cm, the number must be at least 150 and at most 193.
                    //     If in, the number must be at least 59 and at most 76.
                    var hgtMatch = Regex.Match(dict["hgt"], "(?<value>\\d+)(?<units>cm|in)");
                    if(!hgtMatch.Success)
                    {
                        continue;
                    }
                    
                    var units = hgtMatch.Groups["units"].Value;
                    if(!int.TryParse(hgtMatch.Groups["value"].Value, out var hgtValue)) 
                    {
                        continue;
                    }

                    switch(units) {
                        case "cm":
                            if (hgtValue < 150 || hgtValue > 193) continue;
                            break;
                        case "in":
                            if (hgtValue < 59 || hgtValue > 76) continue;
                            break;

                    }
                    
                    // hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
                    if(!Regex.IsMatch(dict["hcl"], "^#[0-9a-f]{6}$"))
                        continue;

                    // ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
                    if(!Regex.IsMatch(dict["ecl"], "^(amb|blu|brn|gry|grn|hzl|oth){1}$"))
                        continue;

                    // pid (Passport ID) - a nine-digit number, including leading zeroes.
                    if(!Regex.IsMatch(dict["pid"], "^[0-9]{9}$"))
                        continue;

                    // cid (Country ID) - ignored, missing or not.

                    validPassports++;
                }
            }

            Console.WriteLine("Valid Passports: " + validPassports);
        }
    }
}
