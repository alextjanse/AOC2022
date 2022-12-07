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
                args = new string[] { "1", "2", "3", "4", "5", "6", "7" };
            }

            foreach (string s in args) {
                string assemblyName = $"AdventOfCode.Day{s}, AOC2022";
                
                Type? type = Type.GetType(assemblyName);

                if (type == null) {
                    throw new Exception("Unsolved/unknown problem: " + s);
                }
                
                string[] input = readInput(s);

                object? instance = Activator.CreateInstance(type, new object[] { s, input });

                if (instance == null) {
                    throw new Exception("Something went wrong while creating the problem instance");
                }

                Problem? problem = instance as Problem;

                if (problem == null) {
                    throw new Exception("Something went wrong while casting the instance to a problem");
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