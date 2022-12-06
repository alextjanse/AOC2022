namespace AdventOfCode
{
    public class Day6 : Problem {
        string stream;

        public Day6(string name, string[] input) : base(name, input) {
            stream = input[0];
        }

        protected override string solvePart1() {
            string current = stream.Substring(0, 3);

            int i = 3;
            while (current.Length < 4) {
                char c = stream[i++];

                int j;

                for (j = current.Length - 1; j >= 0; j--) {
                    if (current[j] == c) {
                        break;
                    }
                }

                current = current.Substring(j + 1) + c;
            }

            return i.ToString();
        }

        protected override string solvePart2() {
            string current = stream.Substring(0, 13);

            int i = 14;
            while (current.Length < 14) {
                char c = stream[i++];

                int j;

                for (j = current.Length - 1; j >= 0; j--) {
                    if (current[j] == c) {
                        break;
                    }
                }

                current = current.Substring(j + 1) + c;
            }

            return i.ToString();
        }
    }
}