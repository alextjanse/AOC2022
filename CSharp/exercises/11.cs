namespace AdventOfCode
{
    public enum OperationType { Plus, Times }
    struct Operation {
        public long? lhs, rhs; // null if it's variable "old"
        public OperationType type;

        public long eval(long var) {
            long x = lhs ?? var;
            long y = rhs ?? var;

            switch (type) {
                case OperationType.Plus:
                    return x + y;
                case OperationType.Times:
                    return x * y;
                default:
                    throw new Exception("Unknown operation type");
            }
        }
    }

    class Monkey {
        public string id;
        public Queue<long> items;
        public Operation operation;
        public int condition;
        public int ifTrueIndex, ifFalseIndex;
        
        public Monkey(string id,
                      long[] items,
                      Operation operation,
                      int condition,
                      int ifTrueIndex,
                      int ifFalseIndex) {
            this.id = id;
            this.items = new Queue<long>(items);
            this.operation = operation;
            this.condition = condition;
            this.ifTrueIndex = ifTrueIndex;
            this.ifFalseIndex = ifFalseIndex;
        }

        public void addToQueue(long item) {
            items.Enqueue(item);
        }

        public int count() {
            return items.Count;
        }

        public void turn1(ref Monkey[] monkeys) {
            while (items.Count > 0) {
                long item = items.Dequeue();

                long worryLevel = operation.eval(item) / 3;

                if (worryLevel % condition == 0) {
                    monkeys[ifTrueIndex].addToQueue(worryLevel);
                } else {
                    monkeys[ifFalseIndex].addToQueue(worryLevel);
                }
            }
        }

        public void turn2(ref Monkey[] monkeys, int mod) {
            while (items.Count > 0) {
                long item = items.Dequeue();

                long worryLevel = operation.eval(item) % mod;

                if (worryLevel % condition == 0) {
                    monkeys[ifTrueIndex].addToQueue(worryLevel);
                } else {
                    monkeys[ifFalseIndex].addToQueue(worryLevel);
                }
            }
        }

        public void receiveItem(long item) {
            items.Enqueue(item);
        }
    }

    public class Day11 : Problem {
        // Two copies of the array, so we have a reset for part 2
        Monkey[] monkeys1, monkeys2;
        public Day11(string name, string[] input) : base(name, input) {
            int monkeys = (input.Length + 1) / 7;

            monkeys1 = new Monkey[monkeys];
            monkeys2 = new Monkey[monkeys];

            for (int i = 0; i < monkeys; i++) {
                string id    = input[7 * i].Substring(7, input[7 * i].Length - 8);
                string itemStr  = input[7 * i + 1].Substring(18);
                string opStr    = input[7 * i + 2].Substring(19);
                string conStr   = input[7 * i + 3].Substring(21);
                string ifTrue   = input[7 * i + 4].Substring(29);
                string ifFalse  = input[7 * i + 5].Substring(30);

                long[] items = itemStr.Split(", ").Select(long.Parse).ToArray();
                
                string[] opSplits = opStr.Split();
                OperationType type = opSplits[1] == "+" ? OperationType.Plus : OperationType.Times;
                int? lhs = opSplits[0] == "old" ? null : int.Parse(opSplits[0]);
                int? rhs = opSplits[2] == "old" ? null : int.Parse(opSplits[2]);
                Operation operation = new Operation() {
                    lhs = lhs,
                    rhs = rhs,
                    type = type,
                };

                int condition = int.Parse(conStr);
                int ifTrueIndex = int.Parse(ifTrue);
                int ifFalseIndex = int.Parse(ifFalse);

                monkeys1[i] = new Monkey(id, items, operation, condition, ifTrueIndex, ifFalseIndex);
                monkeys2[i] = new Monkey(id, items, operation, condition, ifTrueIndex, ifFalseIndex);
            }
        }

        protected override string solvePart1() {
            Monkey[] monkeys = monkeys1;

            int[] counts = new int[monkeys.Length];

            for (int round = 0; round < 20; round++) {
                for (int i = 0; i < monkeys.Length; i++) {
                    // We know the monkey is going to throw out each item it has
                    counts[i] += monkeys[i].count();
                    monkeys[i].turn1(ref monkeys);
                }
            }

            // Sort the array and take the last two values
            Array.Sort(counts);

            int n1 = counts[counts.Length - 1];
            int n2 = counts[counts.Length - 2];

            int product = n1 * n2;

            return product.ToString();
        }

        protected override string solvePart2() {
            Monkey[] monkeys = monkeys2;
            
            /* 
            We can cap the worry level at the product of all condition values.
            We can split w in two parts: w = k * m + (w % m). If we want to check
            if w is divisible by c, we can check both parts seperately. k * m is
            always divisible by c since c is a factor of m, so we only need to
            check if w % m is also divisible by c.
             */
            
            int mod = 1;

            foreach (Monkey monkey in monkeys) {
                mod *= monkey.condition;
            }

            long[] counts = new long[monkeys.Length];

            for (int round = 0; round < 10000; round++) {
                for (int i = 0; i < monkeys.Length; i++) {
                    counts[i] += monkeys[i].count();
                    monkeys[i].turn2(ref monkeys, mod);
                }
            }

            Array.Sort(counts);

            long n1 = counts[counts.Length - 1];
            long n2 = counts[counts.Length - 2];

            long product = n1 * n2;

            return product.ToString();
        }
    }
}