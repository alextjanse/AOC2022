namespace AdventOfCode
{
    public class Day3 : Problem, IComparer<char> {
        public Day3(string name, string[] input) : base(name, input) {

        }

        int priority(char c) {
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
            int px = priority(x);
            int py = priority(y);

            if (px < py) return -1;
            if (px == py) return 0;
            
            return 1;
        }

        string sortedString(string s) {
            char[] array = s.ToCharArray();

            Array.Sort(array, Compare);

            return string.Join("", array);
        }
        
        protected override void reset() {
            
        }

        protected override string solvePart1() {
            int total = 0;

            foreach (string line in input) {
                string comp1 = sortedString(line.Substring(0, line.Length / 2));
                string comp2 = sortedString(line.Substring(line.Length / 2));

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

        protected override string solvePart2()
        {
            int total = 0;

            for (int i = 0; i < input.Length; i += 3) {
                string comp1 = sortedString(input[i]);
                string comp2 = sortedString(input[i + 1]);
                string comp3 = sortedString(input[i + 2]);

                int j = 0, k = 0, l = 0;

                while (j < comp1.Length && k < comp2.Length && l < comp3.Length) {
                    int prio1 = priority(comp1[j]);
                    int prio2 = priority(comp2[k]);
                    int prio3 = priority(comp3[l]);

                    if (prio1 == prio2 && prio2 == prio3) {
                        total += prio1;
                        break;
                    }

                    if (prio1 == prio2) {
                        if (prio1 < prio3) {
                            j++;
                            k++;
                        } else {
                            l++;
                        }
                    } else if (prio1 == prio3) {
                        if (prio1 < prio2) {
                            j++;
                            l++;
                        } else {
                            k++;
                        }
                    } else if (prio2 == prio3) {
                        if (prio2 < prio1) {
                            k++;
                            l++;
                        } else {
                            j++;
                        }
                    } else if (prio1 < prio2 && prio1 < prio3) {
                        j++;
                    } else if (prio2 < prio1 && prio2 < prio3) {
                        k++;
                    } else {
                        l++;
                    }
                }
            }

            return total.ToString();
        }
    }
}