import System.IO
import Prelude hiding ((>))
import Debug.Trace (trace)

main :: IO ()
main = do
    handle <- openFile "../input/2.txt" ReadMode
    contents <- hGetContents handle
    let input = lines contents
    print $ solvePartOne input
    print $ solvePartTwo input
    hClose handle

data Move
    = Rock 
    | Paper
    | Scissors

(>) :: Move -> Move -> Outcome
(>) Rock Paper = Win
(>) Rock Scissors = Lose
(>) Paper Scissors = Win
(>) Paper Rock = Lose
(>) Scissors Rock = Win
(>) Scissors Paper = Lose
(>) _ _ = Draw

moveScore :: Move -> Int
moveScore Rock = 1
moveScore Paper = 2
moveScore Scissors = 3

data Outcome
    = Win
    | Draw
    | Lose

outcomeScore :: Outcome -> Int
outcomeScore Win = 6
outcomeScore Draw = 3
outcomeScore Lose = 0

map0 :: String -> Move
map0 "A" = Rock
map0 "B" = Paper
map0 "C" = Scissors

map1 :: String -> Move
map1 "X" = Rock
map1 "Y" = Paper
map1 "Z" = Scissors

solvePartOne :: [String] -> Int
solvePartOne [] = 0
solvePartOne (x : xs) = outcomeScore (oppMove > plyrMove) + moveScore plyrMove + solvePartOne xs
    where
        [m1, m2] = words x
        oppMove = map0 m1
        plyrMove = map1 m2

map2 :: String -> Outcome
map2 "X" = Lose
map2 "Y" = Draw
map2 "Z" = Win

solvePartTwo :: [String] -> Int
solvePartTwo [] = 0
solvePartTwo (x : xs) = outcomeScore (oppMove > plyrMove) + moveScore plyrMove + solvePartTwo xs
    where
        [arg1, arg2] = words x
        oppMove = map0 arg1
        plyrMove = helper oppMove (map2 arg2)
            where
                helper oppMove Draw = oppMove
                helper oppMove Win = case oppMove of
                    Rock -> Paper
                    Paper -> Scissors
                    Scissors -> Rock
                helper oppMove Lose = case oppMove of
                    Rock -> Scissors
                    Paper -> Rock
                    Scissors -> Paper