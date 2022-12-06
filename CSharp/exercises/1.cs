namespace AdventOfCode {
    public class Day1 : Problem {
        public Day1(string name, string[] input) : base(name, input) {
            
        }

        protected override string solvePart1() {
            /* 
            We need to find the elf that carries the most weight. For this,
            we only need to know the current maximum and replace it accordingly.
             */

            int max = 0;
            int current = 0;

            foreach (string line in input) {
                if (line == "") {
                    // This elf is done, check if it's max
                    if (current > max) {
                        max = current;
                    }

                    // Reset the weight of the current elf
                    current = 0;
                } else {
                    current += int.Parse(line);
                }
            }
            
            // Check if the last elf is the max
            if (current > max) {
                max = current;
            }

            return max.ToString();
        }

        protected override string solvePart2() {
            /* 
            We now need to keep track of a top 3. Same as before, we only
            need to store the max, but now update the top 3.
             */
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
            int i = 2;

            // Check if the value is even in the top 3
            if (top3[i] > value) return;

            /* 
            Check where we need to insert the new value. Do this bottom-up,
            and copy the next value if the value is bigger.
             */
            while (i > 0 && value > top3[i - 1]) {
                top3[i] = top3[--i];
            }

            // i is the index of the new value
            top3[i] = value;
        }
    }
}