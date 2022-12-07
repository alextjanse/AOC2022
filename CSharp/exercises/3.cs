namespace AdventOfCode
{
    public class Day3 : Problem, IComparer<char> {
        public Day3(string name, string[] input) : base(name, input) {

        }
        
        protected override string solvePart1() {
            int total = 0;

            foreach (string line in input) {
                // Split the line, and sort the parts
                string comp1 = sortedString(line.Substring(0, line.Length / 2));
                string comp2 = sortedString(line.Substring(line.Length / 2));

                /*
                Because the compartments are now sorted, we can easily check pairs. If
                we would leave them unsorted, we would have to check every item of
                compartment 1 with every item of compartment 2. There are n / 2 items
                in each compartment, so this yields O(n^2) possible comparisons.

                We can just look at the next items of the compartments. If they have
                a different priority, we can drop the item with lowest priority and
                check the other item with the next item from the compartment. This means
                that we have at most n / 2 + n / 2 comparisons => O(n). Note: we have to
                sort the compartments first, this takes O(n log n), so total running time
                is O(n log n).
                 */

                int i = 0, j = 0;

                while (i < comp1.Length && j < comp2.Length) {
                    int prio1 = priority(comp1[i]);
                    int prio2 = priority(comp2[j]);

                    if (prio1 == prio2) {
                        total += prio1;
                        break;
                    } else if (prio1 < prio2) {
                        i++;
                    } else {
                        j++;
                    }
                }
            }

            return total.ToString();
        }

        protected override string solvePart2() {
            int total = 0;

            for (int i = 0; i < input.Length; i += 3) {
                string comp1 = sortedString(input[i]);
                string comp2 = sortedString(input[i + 1]);
                string comp3 = sortedString(input[i + 2]);

                /* 
                Here we do the same, but now with three compartments. We still drop
                the item(s) with lowest priority (note, can be more than one). This
                makes the maximum number of comparisons 3 * n, where n is the maximum
                size of the three compartments. Again, we have to sort, so O(n log n).
                 */

                int j = 0, k = 0, l = 0;

                while (j < comp1.Length && k < comp2.Length && l < comp3.Length) {
                    int prio1 = priority(comp1[j]);
                    int prio2 = priority(comp2[k]);
                    int prio3 = priority(comp3[l]);

                    if (prio1 == prio2 && prio2 == prio3) {
                        total += prio1;
                        break;
                    }

                    // If item 1 has (shared) lowest prio, take next
                    if (prio1 <= prio2 && prio1 <= prio3) {
                        j++;
                    }

                    // Same for item 2 and 3
                    if (prio2 <= prio1 && prio2 <= prio3) {
                        k++;
                    }
                    
                    if (prio3 <= prio1 && prio3 <= prio2) {
                        l++;
                    }
                }
            }

            return total.ToString();
        }

        int priority(char c) {
            // Use ASCII code to get the score
            int ascii = (int)c;

            if (97 <= ascii && ascii <= 122) {
                return ascii - 96; // a -> 1, z -> 26
            }

            if (65 <= ascii && ascii <= 90) {
                return ascii - 38; // A -> 27, Z -> 52
            }

            throw new Exception("Unexpected character");
        }

        public int Compare(char x, char y) {
            // Compare characters based on their priority
            int px = priority(x);
            int py = priority(y);

            if (px < py)        return -1;
            else if (px == py)  return 0;
            else                return 1;
        }

        string sortedString(string s) {
            char[] array = s.ToCharArray();

            Array.Sort(array, Compare);

            return string.Join("", array);
        }
    }
}