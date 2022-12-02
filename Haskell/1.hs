import System.IO

main :: IO ()
main = do
    handle <- openFile "../input/1.txt" ReadMode
    contents <- hGetContents handle
    let input = lines contents
    print $ solvePartOne input
    print $ solvePartTwo input
    hClose handle

solvePartOne :: [String] -> Int
solvePartOne lines = helper lines 0 0
    where
        helper :: [String] -> Int -> Int -> Int
        helper [] current maximum = max current maximum
        helper ("" : xs) current maximum = helper xs 0 (max current maximum)
        helper (x : xs) current maximum = helper xs (current + (read x :: Int)) maximum

solvePartTwo :: [String] -> Int
solvePartTwo lines = helper lines 0 [0, 0, 0]
    where
        helper :: [String] -> Int -> [Int] -> Int
        helper [] current top = sum (top3 top current)
        helper ("" : xs) current top = helper xs 0 (top3 top current)
        helper (x : xs) current top = helper xs (current + (read x :: Int)) top

top3 :: [Int] -> Int -> [Int]
top3 [] _ = []
top3 (x : xs) n = max x n : top3 xs (min x n)