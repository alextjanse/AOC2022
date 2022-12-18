namespace AdventOfCode
{
    public class Day9 : Problem {
        enum Direction { Up, Down, Left, Right };
        List<(int, Direction)> steps;
        public Day9(string name, string[] input) : base(name, input) {
            steps = new List<(int, Direction)>();

            foreach (string line in input) {
                string[] parts = line.Split();

                Direction direction;

                switch (parts[0]) {
                    case "U":
                        direction = Direction.Up;
                        break;
                    case "D":
                        direction = Direction.Down;
                        break;
                    case "L":
                        direction = Direction.Left;
                        break;
                    case "R":
                        direction = Direction.Right;
                        break;
                    default:
                        throw new Exception("Invalid direction");
                }

                int amount = int.Parse(parts[1]);

                steps.Add((amount, direction));
            }
        }

        protected override string solvePart1() {
            HashSet<(int, int)> visitedPositions = new HashSet<(int, int)>();

            int xHead = 0, yHead = 0, xTail = 0, yTail = 0;

            // Add the beginning position to the set
            visitedPositions.Add((0, 0));

            foreach ((int amount, Direction direction) in steps) {
                for (int i = 0; i < amount; i++) {
                    // Update the head position
                    switch (direction) {
                        case Direction.Up:
                            yHead++;
                            break;
                        case Direction.Down:
                            yHead--;
                            break;
                        case Direction.Left:
                            xHead--;
                            break;
                        case Direction.Right:
                            xHead++;
                            break;
                    }

                    if ((xHead - xTail) * (xHead - xTail) + (yHead - yTail) * (yHead - yTail) > 2) {
                        // The tail and head are too far apart, move the tail
                        switch (direction) {
                            case Direction.Up:
                                xTail = xHead;
                                yTail = yHead - 1;
                                break;
                            case Direction.Down:
                                xTail = xHead;
                                yTail = yHead + 1;
                                break;
                            case Direction.Left:
                                xTail = xHead + 1;
                                yTail = yHead;
                                break;
                            case Direction.Right:
                                xTail = xHead - 1;
                                yTail = yHead;
                                break;
                        }

                        visitedPositions.Add((xTail, yTail));
                    }
                }
            }

            return visitedPositions.Count.ToString();
        }

        protected override string solvePart2() {
            HashSet<(int, int)> visitedPositions = new HashSet<(int, int)>();

            // Array of x, y coordinates per knot
            int[,] positions = new int[10, 2];

            // Add the beginning position to the set
            visitedPositions.Add((0, 0));

            foreach ((int amount, Direction direction) in steps) {
                for (int i = 0; i < amount; i++) {
                    // Update the head position
                    switch (direction) {
                        case Direction.Up:
                            positions[0, 1]++;
                            break;
                        case Direction.Down:
                            positions[0, 1]--;
                            break;
                        case Direction.Left:
                            positions[0, 0]--;
                            break;
                        case Direction.Right:
                            positions[0, 0]++;
                            break;
                    }
                    
                    // Check for every following knot if its position should be updated
                    for (int k = 1; k < positions.GetLength(0); k++) {
                        int xPrev = positions[k - 1, 0];
                        int yPrev = positions[k - 1, 1];
                        int xCurr = positions[k, 0];
                        int yCurr = positions[k, 1];

                        // Pythagoras
                        if ((xPrev - xCurr) * (xPrev - xCurr) + (yPrev - yCurr) * (yPrev - yCurr) > 2) {
                            int xDelta = Math.Abs(xPrev - xCurr) > 1 ? (xPrev - xCurr) / 2 : xPrev - xCurr;
                            int yDelta = Math.Abs(yPrev - yCurr) > 1 ? (yPrev - yCurr) / 2 : yPrev - yCurr;

                            positions[k, 0] = xCurr + xDelta;
                            positions[k, 1] = yCurr + yDelta;
                        } else {
                            // The knot doesn't move, so the following knots don't move either
                            break;
                        }
                    }

                    visitedPositions.Add((positions[9, 0], positions[9, 1]));
                }
            }

            return visitedPositions.Count.ToString();
        }
    }
}