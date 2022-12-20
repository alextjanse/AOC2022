namespace AdventOfCode
{
    public class Day12 : Problem {
        int width, height;
        (int, int) startPosition, endPosition;
        int[,] heightmap;
        public Day12(string name, string[] input) : base(name, input) {
            width = input[0].Length;
            height = input.Length;
            heightmap = new int[width, height];

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    char c = input[y][x];

                    if (c == 'S') {
                        startPosition = (x, y);
                    } else if (c == 'E') {
                        endPosition = (x, y);
                    }

                    heightmap[x, y] = charToHeight(c);
                }
            }
        }

        int charToHeight(char c) {
            if (c == 'S') return charToHeight('a');
            if (c == 'E') return charToHeight('z');
            return (int)c - 97; // ASCII to int: a -> 0, z -> 25
        }

        protected override string solvePart1() {
            /* 
            I use breadth-first search for this puzzle: https://en.wikipedia.org/wiki/Breadth-first_search
            Make a queue and put in the starting position and the length of the path (start = 0).
            Then, dequeue an item of the queue (current position and path length), and add its
            valid neighbors to the queue. Because we use a queue, the order of items in the queue
            will always be ordered by the number of steps it took to get there. That means that if
            we pop an item from the queue, we can guarantee that it is the shortest path length to
            that position.
             */
            Queue<((int, int), int)> bfs = new Queue<((int, int), int)>();
            
            // Keep track of the visited positions. If we visit it again, we can skip it.
            HashSet<(int, int)> visited = new HashSet<(int, int)>();

            // Add the start position to the queue
            bfs.Enqueue((startPosition, 0));

            while (bfs.Count > 0) {
                ((int, int) currentPosition, int steps) = bfs.Dequeue();

                if (!visited.Add(currentPosition)) {
                    // Current position already visited, skip
                    continue;
                }

                if (currentPosition == endPosition) {
                    // Found the end position, exit
                    return steps.ToString();
                }

                (int x, int y) = currentPosition;
                int currentHeight = heightmap[x, y];

                // Check if neighbors are in bound and add if the slope isn't too steep
                if (x - 1 >= 0 && currentHeight + 1 >= heightmap[x - 1, y]) {
                    bfs.Enqueue(((x - 1, y), steps + 1));
                }

                if (x + 1 < width && currentHeight + 1 >= heightmap[x + 1, y]) {
                    bfs.Enqueue(((x + 1, y), steps + 1));
                }

                if (y - 1 >= 0 && currentHeight + 1 >= heightmap[x, y - 1]) {
                    bfs.Enqueue(((x, y - 1), steps + 1));
                }

                if (y + 1 < height && currentHeight + 1 >= heightmap[x, y + 1]) {
                    bfs.Enqueue(((x, y + 1), steps + 1));
                }
            }

            return "No path found";
        }

        protected override string solvePart2() {
            /* 
            Now we have to do it the other way around. Start from the end position,
            and climb down. If we come to an height of 0 ('a' tile), we have candidate
            to the shortest hiking path. Check if it is shorter and update accordingly.
             */
            Queue<((int, int), int)> bfs = new Queue<((int, int), int)>();

            HashSet<(int, int)> visited = new HashSet<(int, int)>();

            bfs.Enqueue((endPosition, 0));

            int shortestLength = int.MaxValue;

            while (bfs.Count > 0) {
                ((int, int) currentPosition, int steps) = bfs.Dequeue();

                if (!visited.Add(currentPosition)) {
                    continue;
                }

                (int x, int y) = currentPosition;
                int currentHeight = heightmap[x, y];

                if (currentHeight == 0 && steps < shortestLength) {
                    shortestLength = steps;
                    
                    /* 
                    We don't have to look at the neighbors since we are already at the lowest point.
                    If there is a path to another 'a' tile going through this one, then that path
                    is always longer. 
                     */
                    continue;
                }

                if (x - 1 >= 0 && heightmap[x - 1, y] + 1 >= currentHeight) {
                    bfs.Enqueue(((x - 1, y), steps + 1));
                }

                if (x + 1 < width && heightmap[x + 1, y] + 1 >= currentHeight) {
                    bfs.Enqueue(((x + 1, y), steps + 1));
                }

                if (y - 1 >= 0 && heightmap[x, y - 1] + 1 >= currentHeight) {
                    bfs.Enqueue(((x, y - 1), steps + 1));
                }

                if (y + 1 < height && heightmap[x, y + 1] + 1 >= currentHeight) {
                    bfs.Enqueue(((x, y + 1), steps + 1));
                }
            }

            return shortestLength.ToString();
        }
    }
}