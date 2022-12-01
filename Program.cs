using System;
using System.IO;

namespace AdventOfCode
{
    public class Exercise {
        public string[] input;

        public Exercise(string name, string[] input) {
            Console.WriteLine($"Day {name}");
            this.input = input;
        }

        public virtual void solvePart1() {
            Console.Write("Part One: ");
        }

        public virtual void solvePart2() {
            Console.Write("Part Two: ");
        }
    }

    internal class Program {
        static void Main(string[] args) {
            foreach (string s in args) {
                string[] input = readInput(s);
                Exercise e;

                switch (s) {
                    case "1":
                        e = new Exercise1(s, input);
                        break;
                    default:
                        throw new Exception("invalid argument");
                }

                e.solvePart1();
                e.solvePart2();

                Console.WriteLine(); // empty line seperating the exercise outputs
            }
        }

        static string[] readInput(string exercise) {
            StreamReader sr = new StreamReader($"./input/{exercise}.txt");

            return sr.ReadToEnd().Split('\n');
        }
    }
}