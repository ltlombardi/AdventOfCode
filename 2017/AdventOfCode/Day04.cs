using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day4
    {
        public static void Do()
        {
            var input = System.IO.File.ReadAllLines("../../Day04.txt");
            var result = 0;

            foreach (var item in input)
            {
                var passwords = item.Split(' ');
                var set = new HashSet<string>();
                var isValid = true;
                foreach (var password in passwords)
                {
                    if (!set.Contains(password))
                    {
                        set.Add(password);
                    }
                    else
                    {
                        isValid = false;
                    }

                }
                result += isValid ? 1 : 0;
            }
            Console.WriteLine("Day4 Part1: result is " + result);

            result = 0;
            foreach (var item in input)
            {
                var passwords = item.Split(' ');
                var set = new HashSet<string>();
                var isValid = true;
                foreach (var password in passwords)
                {
                    if (!set.Contains(password))
                    {
                        foreach (var word in set)
                        {
                            if (IsAnagram(password, word))
                            {
                                isValid = false;
                                break;
                            }
                        }
                        if (isValid) set.Add(password);
                    }
                    else
                    {
                        isValid = false;
                        break;
                    }
                }
                result += isValid ? 1 : 0;
            }

            Console.WriteLine("Day4 Part2: result is " + result);
        }

        private static bool IsAnagram(string one, string other)
        {
            if (one.Length != other.Length) return false;
            foreach (var letter in one)
            {
                if (other.Contains(letter.ToString()))
                {
                    other = other.Remove(other.IndexOf(letter), 1);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }


}
