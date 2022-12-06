namespace AdventOfCode {
    public class Day5 : Problem {
        Stack<char>[] starting_stacks, stacks;
        List<(int, int, int)> exchanges;

        public Day5(string name, string[] input) : base(name, input) {
            int nStacks = (input[0].Length + 1) / 4;

            starting_stacks = new Stack<char>[nStacks];

            List<char>[] temps = new List<char>[nStacks];

            for (int i = 0; i < nStacks; i++) {
                starting_stacks[i] = new Stack<char>();
                temps[i] = new List<char>();
            }

            int currentLine = 0;
            string line = input[currentLine++];

            while (line.Contains('[')) {
                for (int i = 0; i < nStacks; i++) {
                    int index = 4 * i + 1;
                    
                    char value = line[index];

                    if (value == ' ') {
                        continue;
                    }

                    temps[i].Add(value);
                }

                line = input[currentLine++];
            }

            for (int i = 0; i < nStacks; i++) {
                for (int j = temps[i].Count - 1; j >= 0; j--) {
                    starting_stacks[i].Push(temps[i][j]);
                }
            }

            exchanges = new List<(int, int, int)>();

            currentLine++;

            for (; currentLine < input.Length; currentLine++) {
                line = input[currentLine];
                
                string[] splits = line.Split();

                int amount = int.Parse(splits[1]);
                int from = int.Parse(splits[3]) - 1; // 0-indexing
                int to = int.Parse(splits[5]) - 1; // 0-indexing

                exchanges.Add((amount, from, to));
            }
        }

        void init() {
            stacks = new Stack<char>[starting_stacks.Length];

            for (int i = 0; i < stacks.Length; i++) {
                stacks[i] = new Stack<char>(starting_stacks[i].Reverse());
            }
        }

        string read(Stack<char>[] stacks) {
            string output = "";

            foreach (Stack<char> stack in stacks) {
                output += stack.Peek();
            }

            return output;
        }

        protected override string solvePart1() {
            init();

            foreach ((int amount, int from, int to) in exchanges) {
                for (int i = 0; i < amount; i++) {
                    stacks[to].Push(stacks[from].Pop());
                }
            }

            return read(stacks);
        }

        protected override string solvePart2() {
            init();

            Stack<char> pile = new Stack<char>();

            foreach ((int amount, int from, int to) in exchanges) {
                for (int i = 0; i < amount; i++) {
                    pile.Push(stacks[from].Pop());
                }

                while (pile.Count > 0) {
                    stacks[to].Push(pile.Pop());
                }
            }

            return read(stacks);
        }
    }
}