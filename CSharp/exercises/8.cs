namespace AdventOfCode
{
    public class Day8 : Problem {
        int width, height;
        int[,] treeHeights;

        public Day8(string name, string[] input) : base(name, input) {
            width = input[0].Length;
            height = input.Length;

            treeHeights = new int[width, height];

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    treeHeights[x, y] = int.Parse(input[y][x].ToString());
                }
            }
        }

        protected override string solvePart1() {
            bool[,] visibleTop = new bool[width, height];
            bool[,] visibleBottom = new bool[width, height];
            bool[,] visibleLeft = new bool[width, height];
            bool[,] visibleRight = new bool[width, height];

            for (int x = 0; x < width; x++) {
                // Set edges first
                visibleTop[x, 0] = true;
                visibleBottom[x, height - 1] = true;

                // Check the column for visibility from top and bottom
                for (int y = 1; y < height; y++) {
                    if (visibleTop[x, y - 1] && treeHeights[x, y - 1] < treeHeights[x, y]) {
                        // The previous tree is visible, and this new one is taller
                        visibleTop[x, y] = true;
                    } else {
                        // Stop searching in this column
                        break;
                    }
                }

                for (int y = height - 2; y >= 0; y--) {
                    if (visibleBottom[x, y + 1] && treeHeights[x, y + 1] < treeHeights[x, y]) {
                        visibleBottom[x, y] = true;
                    } else {
                        break;
                    }
                }
            }

            for (int y = 0; y < height; y++) {
                visibleLeft[0, y] = true;
                visibleRight[width - 1, y] = true;

                for (int x = 1; x < width; x++) {
                    if (visibleLeft[x - 1, y] && treeHeights[x - 1, y] < treeHeights[x, y]) {
                        visibleLeft[x, y] = true;
                    } else {
                        break;
                    }
                }

                for (int x = width - 2; x >= 0; x--) {
                    if (visibleRight[x + 1, y] && treeHeights[x + 1, y] < treeHeights[x, y]) {
                        visibleRight[x, y] = true;
                    } else {
                        break;
                    }
                }
            }

            int count = 0;

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    if (visibleTop[x, y] || visibleBottom[x, y] || visibleLeft[x, y] || visibleRight[x, y]) {
                        count++;
                    }
                }
            }

            return count.ToString();
        }

        protected override string solvePart2() {
            return "";
        }
    }
}