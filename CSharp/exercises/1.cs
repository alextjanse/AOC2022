namespace AdventOfCode {
    public class Exercise1 : Exercise {
        public Exercise1(string name, string[] input) : base(name, input) {
            
        }

        public override void solvePart1()
        {
            base.solvePart1();

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

            Console.WriteLine(max);
        }

        public override void solvePart2()
        {
            base.solvePart2();

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

            Console.WriteLine(top3.Sum());
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