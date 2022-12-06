namespace AdventOfCode {
    public class Day2 : Problem {
        public Day2(string name, string[] input) : base(name, input) {
            
        }

        protected override string solvePart1() {
            int totalScore = 0;

            foreach (string line in input) {
                string[] moves = line.Split();

                Move opponent = stringToMove(moves[0]);
                Move player = stringToMove(moves[1]);

                totalScore += getScore(opponent, player);
            }

            return totalScore.ToString();
        }

        protected override string solvePart2() {
            int totalScore = 0;

            foreach (string line in input) {
                string[] moves = line.Split();

                Move opponent = stringToMove(moves[0]);
                
                Move player;

                switch (moves[1]) {
                    case "X":
                        player = (Move)(((int)opponent + 3 - 1) % 3);
                        break;
                    case "Y":
                        player = opponent;
                        break;
                    case "Z":
                        player = (Move)(((int)opponent + 1) % 3);
                        break;
                    default:
                        throw new Exception("Unexpected input");
                }

                totalScore += getScore(opponent, player);
            }

            return totalScore.ToString();
        }

        enum Move { Rock, Paper, Scissors };

        Move stringToMove(string s) {
            switch (s) {
                case "A":
                case "X":
                    return Move.Rock;
                case "B":
                case "Y":
                    return Move.Paper;
                case "C":
                case "Z":
                    return Move.Scissors;
                default:
                    throw new Exception("Unexpected input");
            }
        }

        int moveScore(Move move) {
            switch (move) {
                case Move.Rock:
                    return 1;
                case Move.Paper:
                    return 2;
                case Move.Scissors:
                    return 3;
                default:
                    throw new Exception("Unknown move");
            }
        }

        int getScore(Move opponent, Move player) {
            int iOpponent = (int)opponent;
            int iPlayer = (int)player;

            int score = moveScore(player);

            if (iPlayer == (iOpponent + 1) % 3) { // player wins
                score += 6;
            } else if (iPlayer == iOpponent) { // draw
                score += 3;
            } else { // player loses
                score += 0;
            }

            return score;
        }
    }
}