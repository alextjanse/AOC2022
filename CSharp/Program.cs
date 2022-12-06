using System;
using System.IO;

namespace AdventOfCode
{
    public abstract class Problem {
        string name;
        protected string[] input;

        public Problem(string name, string[] input) {
            this.name = name;
            this.input = input;
        }

        public void solve() {
            Console.WriteLine($"Day {name}");
            Console.WriteLine("Part One: " + solvePart1());
            Console.WriteLine("Part Two: "+ solvePart2());
        }

        protected abstract string solvePart1();
        protected abstract string solvePart2();
    }

    internal class Program {
        static void Main(string[] args) {
            if (args.Length == 0) {
                args = new string[] { "1", "2", "3", "4", "5" };
            }

            foreach (string s in args) {
                string[] input = readInput(s);
                Problem p;

                switch (s) {
                    case "1":
                        p = new Day1(s, input);
                        break;
                    case "2":
                        p = new Day2(s, input);
                        break;
                    case "3":
                        p = new Day3(s, input);
                        break;
                    case "4":
                        p = new Day4(s, input);
                        break;
                    case "5":
                        p = new Day5(s, input);
                        break;
                    default:
                        throw new Exception("invalid argument");
                }

                p.solve();
            }
        }

        static string[] readInput(string exercise) {
            StreamReader sr = new StreamReader($"../input/{exercise}.txt");

            return sr.ReadToEnd().Split('\n');
        }
    }
}