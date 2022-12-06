namespace AdventOfCode
{
    public class Day4 : Problem {
        public Day4(string name, string[] input) : base(name, input) {

        }

        bool checkFullOverlap(int lb1, int ub1, int lb2, int ub2) {
            return lb1 <= lb2 && ub1 >= ub2;
        }

        protected override string solvePart1() {
            int total = 0;

            foreach (string line in input) {
                string[] parts = line.Split(',');

                string[] bounds1 = parts[0].Split('-');
                int lb1 = int.Parse(bounds1[0]);
                int ub1 = int.Parse(bounds1[1]);

                string[] bounds2 = parts[1].Split('-');
                int lb2 = int.Parse(bounds2[0]);
                int ub2 = int.Parse(bounds2[1]);

                if (checkFullOverlap(lb1, ub1, lb2, ub2)
                 || checkFullOverlap(lb2, ub2, lb1, ub1)) {
                    total++;
                }
            }

            return total.ToString();
        }

        bool checkPartialOverlap(int lb1, int ub1, int lb2, int ub2) {
            return lb1 <= lb2 && ub1 >= lb2;
        }

        protected override string solvePart2() {
            int total = 0;

            foreach (string line in input) {
                string[] parts = line.Split(',');

                string[] bounds1 = parts[0].Split('-');
                int lb1 = int.Parse(bounds1[0]);
                int ub1 = int.Parse(bounds1[1]);

                string[] bounds2 = parts[1].Split('-');
                int lb2 = int.Parse(bounds2[0]);
                int ub2 = int.Parse(bounds2[1]);

                if (checkPartialOverlap(lb1, ub1, lb2, ub2)
                 || checkPartialOverlap(lb2, ub2, lb1, ub1)) {
                    total++;
                }
            }

            return total.ToString();
        }
    }
}