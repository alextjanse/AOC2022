const fs = require('fs');

fs.readFile('./input/1.txt', 'utf8', (err, data) => {
    if (err) {
        console.error(err);
        return;
    }

    const input = data.split('\n');

    solvePartOne(input);
    solvePartTwo(input);
});

const Moves = {
    Rock: 0,
    Paper: 1,
    Scissors: 2,
};

/**
 * Solve part one
 * @param {string[]} lines
 */
function solvePartOne(lines) {
    let total = 0;

    for (const line of lines) {
        const [s1, s2] = lines.split();
    }
}

/**
 * Solve part two
 * @param {string[]} lines
 */
function solvePartTwo(lines) {

}