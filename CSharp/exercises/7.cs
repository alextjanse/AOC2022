namespace AdventOfCode
{
    class Directory {
        /* 
        We need to make a directory tree. We could make it so that a node can
        either be a directory or a file, but since the only intersting thing
        about a file is its size, we can just store the files in a dictionary
        in every directory.
         */
        public string name;
        public Directory? parent; // Used for `$ cd ..`

        // Dictionary, because then we can easily find the subfolder for `$ cd sub`
        public Dictionary<string, Directory> subdirectories;
        // This doesn't have to be a dictionary, but would be nice to have for other purposes
        public Dictionary<string, int> files;
        public int size;

        public Directory(string name, Directory? parent) {
            this.name = name;
            this.parent = parent;

            subdirectories = new Dictionary<string, Directory>();
            files = new Dictionary<string, int>();
        }

        public void setSize() {
            size = 0;

            foreach (int fileSize in files.Values) {
                size += fileSize;
            }

            foreach (Directory sub in subdirectories.Values) {
                sub.setSize();

                size += sub.size;
            }
        }
    }

    public class Day7 : Problem {

        Directory root;

        public Day7(string name, string[] input) : base(name, input) {
            root = new Directory("/", null);

            Directory current = root;

            foreach (string line in input) {
                if (line == "$ cd /") {
                    current = root;
                }
                
                else if (line == "$ cd ..") {
                    if (current.parent != null) {
                        current = current.parent;
                    } else {
                        throw new Exception("No parent directory");
                    }
                }
                
                else if (line.StartsWith("$ cd ")) {
                    string dir = line.Substring(5);

                    current = current.subdirectories[dir];
                }

                else if (line == "$ ls") {
                    // We don't have to do anything here
                }

                else if (line.StartsWith("dir ")) {
                    string dirName = line.Substring(4);

                    Directory sub = new Directory(dirName, current);

                    current.subdirectories.Add(dirName, sub);
                }

                else {
                    // Line is a file
                    string[] split = line.Split();

                    int size = int.Parse(split[0]);
                    string fileName = split[1];

                    current.files.Add(fileName, size);
                }
            }

            root.setSize();
        }

        protected override string solvePart1() {
            return sumSmallDirectories(root).ToString();
        }

        int sumSmallDirectories(Directory directory, int limit = 100000) {
            // Recursively sum the directories with size < limit
            int sum = 0;

            if (directory.size < limit) {
                sum += directory.size;
            }

            foreach (Directory sub in directory.subdirectories.Values) {
                sum += sumSmallDirectories(sub, limit);
            }

            return sum;
        }

        protected override string solvePart2() {
            // Calculate space to free
            int totalDiskSpace = 70000000;
            int requiredSpace = 30000000;
            int freeSpace = totalDiskSpace - root.size;
            int spaceToFree = requiredSpace - freeSpace;

            if (spaceToFree < 0) {
                // Enough free space
                throw new Exception("There is already enough free space");
            }
            
            Directory min = root;
            findSmallestDirectory(root, spaceToFree, ref min);

            return min.size.ToString();
        }

        void findSmallestDirectory(Directory directory, int minSize, ref Directory min) {
            // Recursively seek for the smallest directory with size > minSize
            if (directory.size > minSize) {
                if (directory.size < min.size) {
                    min = directory;
                }

                foreach (Directory sub in directory.subdirectories.Values) {
                    findSmallestDirectory(sub, minSize, ref min);
                }
            }
        }
    }
}