namespace AdventOfCode {
    public class Day1 : Problem {
        public Day1(string name, string[] input) : base(name, input) {
            
        }

        protected override string solvePart1() {
            int max = 0;

            int current = 0;

            foreach (string line in input) {
                if (line == "") {
                    if (current > max) {
                        max = current;
                    }

                    current = 0;
                    continue;
                }

                current += int.Parse(line);
            }

            if (current > max) {
                max = current;
            }

            return max.ToString();
        }

        protected override string solvePart2() {
            int[] top3 = new int[] {0, 0, 0};

            int current = 0;

            foreach (string line in input) {
                if (line == "") {
                    replaceTop3(top3, current);

                    current = 0;
                    continue;
                }

                current += int.Parse(line);
            }

            replaceTop3(top3, current);

            return top3.Sum().ToString();
        }

        void replaceTop3(int[] top3, int value) {
            for (int i = 0; i < 3; i++) {
                if (value > top3[i]) {
                    int temp = top3[i];
                    top3[i] = value;
                    value = temp;
                }
            }
        }
    }
}