namespace AdventOfCode
{
    public class Day10 : Problem {
        public Day10(string name, string[] input) : base(name, input) {

        }

        protected override string solvePart1() {
            int cycle = 1;
            int x = 1;

            int interestingCycle = 20;

            int sum = 0;

            foreach (string line in input) {
                if (line.StartsWith("addx")) {
                    // Add the value to the x register
                    int amount = int.Parse(line.Substring(5));

                    cycle += 2;

                    if (cycle > interestingCycle) {
                        sum += x * interestingCycle;
                        interestingCycle += 40;
                    }

                    x += amount;
                } else {
                    cycle += 1;

                    if (cycle > interestingCycle) {
                        sum += x * interestingCycle;
                        interestingCycle += 40;
                    }
                }
            }

            return sum.ToString();
        }

        protected override string solvePart2() {
            int x = 1;

            // The current line of input
            string line;
            int lineIndex = 0;

            // The value to add at the end of the operation
            int amountToAdd = 0;
            // The cycle where the operation is done
            int readyCycle = 0;

            string output = "";
            
            for (int crt = 0; crt < 240; crt++) {
                if (crt % 40 == 0) {
                    output += '\n';
                }

                /* 
                The previous operation has finished. Add the value to the
                x register, and prepare the next operation.
                 */
                if (crt == readyCycle) {
                    x += amountToAdd;

                    line = input[lineIndex++];
                    
                    if (line.StartsWith("addx")) {
                        amountToAdd = int.Parse(line.Substring(5));

                        readyCycle += 2;
                    } else {
                        amountToAdd = 0;

                        readyCycle += 1;
                    }
                }

                /* 
                Draw the pixel accordingly. Check distance between the crt
                and the sprite position.
                 */
                output += Math.Abs(crt % 40 - x) <= 1 ? '#' : '.';
            }

            return output;
        }
    }
}