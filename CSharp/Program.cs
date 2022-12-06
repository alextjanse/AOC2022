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
            args = new string[] { "6" };

            if (args.Length == 0) {
                args = new string[] { "1", "2", "3", "4", "5" };
            }

            foreach (string s in args) {
                string[] input = readInput(s);
                Problem problem;

                switch (s) {
                    case "1":
                        problem = new Day1(s, input);
                        break;
                    case "2":
                        problem = new Day2(s, input);
                        break;
                    case "3":
                        problem = new Day3(s, input);
                        break;
                    case "4":
                        problem = new Day4(s, input);
                        break;
                    case "5":
                        problem = new Day5(s, input);
                        break;
                    case "6":
                        problem = new Day6(s, input);
                        break;
                    default:
                        throw new Exception("invalid argument");
                }

                problem.solve();
            }
        }

        static string[] readInput(string exercise) {
            StreamReader sr = new StreamReader($"../input/{exercise}.txt");

            return sr.ReadToEnd().Split('\n');
        }
    }
}