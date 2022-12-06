namespace AdventOfCode {
    public class Exercise5 : Exercise {
        Stack<char>[] stacks1, stacks2;
        List<(int, int, int)> exchanges;

        public Exercise5(string name, string[] input) : base(name, input) {
            int nStacks = (input[0].Length + 1) / 4;

            stacks1 = new Stack<char>[nStacks];
            stacks2 = new Stack<char>[nStacks];

            List<char>[] temps = new List<char>[nStacks];

            for (int i = 0; i < nStacks; i++) {
                stacks1[i] = new Stack<char>();
                stacks2[i] = new Stack<char>();
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
                    stacks1[i].Push(temps[i][j]);
                    stacks2[i].Push(temps[i][j]);
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

        public override void solvePart1()
        {
            base.solvePart1();

            foreach ((int amount, int from, int to) in exchanges) {
                for (int i = 0; i < amount; i++) {
                    stacks1[to].Push(stacks1[from].Pop());
                }
            }

            string output = "";

            foreach (Stack<char> stack in stacks1) {
                output += stack.Peek();
            }

            Console.WriteLine(output);
        }

        public override void solvePart2()
        {
            base.solvePart2();
            
            Stack<char> temp = new Stack<char>();

            foreach ((int amount, int from, int to) in exchanges) {
                for (int i = 0; i < amount; i++) {
                    temp.Push(stacks2[from].Pop());
                }

                while (temp.Count > 0) {
                    stacks2[to].Push(temp.Pop());
                }
            }

            string output = "";

            foreach (Stack<char> stack in stacks2) {
                output += stack.Peek();
            }

            Console.WriteLine(output);
        }
    }
}