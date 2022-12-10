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
            /* 
            Loop over the columns and rows, to see if a tree is visible from top/bottom/left/right.
            If we find a tree that is also visible and that is smaller than our current tree, we
            know that the current tree must be visible as well.
             */
            bool[,] visibleTop = new bool[width, height];
            bool[,] visibleBottom = new bool[width, height];
            bool[,] visibleLeft = new bool[width, height];
            bool[,] visibleRight = new bool[width, height];

            for (int x = 0; x < width; x++) {
                // Set edges to visible
                visibleTop[x, 0] = true;
                visibleBottom[x, height - 1] = true;

                // Check the column for visibility from top to bottom
                for (int y = 1; y < height; y++) {
                    // Check if the top neighbors are smaller to determine if (x, y) is visible
                    for (int j = y - 1; j >= 0; j--) {
                        if (treeHeights[x, y] <= treeHeights[x, j]) {
                            // The tree is blocked by neighbor (x, j)
                            break;
                        } else if (visibleTop[x, j]) {
                            // Tree (x, j) is visible and (x, y) is taller => (x, y) is visible
                            visibleTop[x, y] = true;
                            break;
                        }
                    }
                }

                // Check the column for visibility from bottom to top
                for (int y = height - 2; y >= 0; y--) {
                    // Check if the bottom neighbors are smaller to determine if (x, y) is visible
                    for (int j = y + 1; j < height; j++) {
                        if (treeHeights[x, y] <= treeHeights[x, j]) {
                            // The tree is blocked by neighbor (x, j)
                            break;
                        } else if (visibleBottom[x, j]) {
                            // Tree (x, j) is visible and (x, y) is taller => (x, y) is visible
                            visibleBottom[x, y] = true;
                            break;
                        }
                    }
                }
            }
            
            for (int y = 0; y < height; y++) {
                // Set edges to visible
                visibleLeft[0, y] = true;
                visibleRight[width - 1, y] = true;

                // Check the row for visibility from left to right
                for (int x = 1; x < width; x++) {
                    // Check if the left neighbors are smaller to determine if (x, y) is visible
                    for (int i = x - 1; i >= 0; i--) {
                        if (treeHeights[x, y] <= treeHeights[i, y]) {
                            // The tree is blocked by neighbor (i, y)
                            break;
                        } else if (visibleLeft[i, y]) {
                            // Tree (i, y) is visible and (x, y) is taller => (x, y) is visible.
                            visibleLeft[x, y] = true;
                            break;
                        }
                    }
                }

                // Check the column for visibility from right to left
                for (int x = width - 2; x >= 0; x--) {
                    // Check if the right neighbors are smaller to determine if (x, y) is visible
                    for (int i = x + 1; i < width; i++) {
                        if (treeHeights[x, y] <= treeHeights[i, y]) {
                            // The tree is blocked by neighbor (i, y)
                            break;
                        } else if (visibleRight[i, y]) {
                            // Tree (i, y) is visible and (x, y) is taller => (x, y) is visible
                            visibleRight[x, y] = true;
                            break;
                        }
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
            /* 
            This can probably be done smarter, but whatever. Loop over all the trees,
            and count how many trees are visible in any direction.

            New idea (not yet implemented): work the other way around. Loop over all
            the trees, and see which neighbors can see this tree. I'm not sure if this
            is faster, though.
             */
            int maxScore = 0;

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    int top = 0, bottom = 0, left = 0, right = 0;

                    for (int i = x - 1; i >= 0; i--) {
                        left++;

                        if (treeHeights[i, y] >= treeHeights[x, y]) {
                            // We have found the maximum view depth in this direction
                            break;
                        }
                    }

                    for (int i = x + 1; i < width; i++) {
                        right++;

                        if (treeHeights[i, y] >= treeHeights[x, y]) {
                            break;
                        }
                    }

                    for (int j = y - 1; j >= 0; j--) {
                        top++;

                        if (treeHeights[x, j] >= treeHeights[x, y]) {
                            break;
                        }
                    }

                    for (int j = y + 1; j < height; j++) {
                        bottom++;

                        if (treeHeights[x, j] >= treeHeights[x, y]) {
                            break;
                        }
                    }

                    // Calculate the scenic score and update the max score
                    int scenicScore = top * bottom * left * right;

                    if (scenicScore > maxScore) {
                        maxScore = scenicScore;
                    }
                }
            }

            return maxScore.ToString();
        }
    }
}