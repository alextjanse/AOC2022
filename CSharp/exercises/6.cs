namespace AdventOfCode
{
    public class Day6 : Problem {
        string stream;

        public Day6(string name, string[] input) : base(name, input) {
            stream = input[0];
        }

        protected override string solvePart1() {
            return solveForLength(4);
        }

        protected override string solvePart2() {
            return solveForLength(14);
        }

        string solveForLength(int length) {
            // We keep track of the last sequence of unique characters
            string current = "";

            int i = 0;

            // Stop if we have the correct length
            while (current.Length < length) {
                char c = stream[i++];

                /* 
                Check back to front if the character is already in our current
                string. If there is any, we know where to split the string to
                keep the last unique characters. Otherwise, the characters are
                all unique.

                We have n characters in our stream. For each character, we make
                at most k comparisons, where k = length (constant). Running time:
                O(k n) = O(n).
                 */
                
                int j = current.Length - 1;

                while (j >= 0) {
                    if (current[j] == c) {
                        break;
                    }

                    j--;
                }

                current = current.Substring(j + 1) + c;
            }

            return i.ToString();
        }
    }
}