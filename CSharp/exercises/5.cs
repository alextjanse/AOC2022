namespace AdventOfCode {
    public class Day5 : Problem {
        Stack<char>[] stacks1, stacks2;
        List<(int, int, int)> exchanges;

        public Day5(string name, string[] input) : base(name, input) {
            int n = (input[0].Length + 1) / 4;

            // Two times the items, so we have a fresh version for part 2
            stacks1 = new Stack<char>[n];
            stacks2 = new Stack<char>[n];

            /* 
            The items of each stack. We need to store them first in lists,
            so we can then add them bottom-up to the stacks.
             */            
            List<char>[] items = new List<char>[n];

            for (int i = 0; i < n; i++) {
                stacks1[i] = new Stack<char>();
                stacks2[i] = new Stack<char>();
                items[i] = new List<char>();
            }

            int currentLine = 0;
            string line = input[currentLine++];

            // Add items to items
            while (line.Contains('[')) {
                for (int i = 0; i < n; i++) {
                    int index = 4 * i + 1;
                    
                    char value = line[index];

                    if (value == ' ') {
                        continue;
                    }

                    items[i].Add(value);
                }

                line = input[currentLine++];
            }

            // Add items in right order to the stacks
            for (int i = 0; i < n; i++) {
                for (int j = items[i].Count - 1; j >= 0; j--) {
                    stacks1[i].Push(items[i][j]);
                    stacks2[i].Push(items[i][j]);
                }
            }

            // Tuple of each exchange: (amount, fromIndex, toIndex)
            exchanges = new List<(int, int, int)>();

            currentLine++;

            while(currentLine < input.Length) {
                line = input[currentLine];
                
                string[] splits = line.Split();

                int amount = int.Parse(splits[1]);
                int from = int.Parse(splits[3]) - 1; // Adjust for 0-indexing
                int to = int.Parse(splits[5]) - 1;

                exchanges.Add((amount, from, to));

                currentLine++;
            }
        }

        string read(Stack<char>[] items) {
            string output = "";

            foreach (Stack<char> stack in items) {
                output += stack.Peek();
            }

            return output;
        }

        protected override string solvePart1() {
            /* 
            Apply the list of exhanges one by one
             */
            foreach ((int amount, int from, int to) in exchanges) {
                for (int i = 0; i < amount; i++) {
                    // Can this be done nicer? Probably.
                    stacks1[to].Push(stacks1[from].Pop());
                }
            }

            return read(stacks1);
        }

        protected override string solvePart2() {
            /* 
            We can't move multiple items at once, so we make a new stack,
            where we put all the items on one by one (order will reverse).
            After moving all items to this pile, we move them to the destination
            stack, reversing the order again, putting them in the correct order.
             */
            
            Stack<char> pile = new Stack<char>();

            foreach ((int amount, int from, int to) in exchanges) {
                for (int i = 0; i < amount; i++) {
                    pile.Push(stacks2[from].Pop());
                }

                while (pile.Count > 0) {
                    stacks2[to].Push(pile.Pop());
                }
            }

            return read(stacks2);
        }
    }
}