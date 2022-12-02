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

/**
 * Solve part one
 * @param {string[]} lines
 */
function solvePartOne(lines) {
    let max = 0;
    let current = 0;

    for (const line of lines) {
        if (line === '') {
            if (current > max) {
                max = current;
            }

            current = 0;

            continue;
        }

        const i = parseInt(line);
        current += i;
    }

    if (current > max) {
        max = current;
    }

    console.log(max);
}

/**
 * Solve part two
 * @param {string[]} lines
 */
 function solvePartTwo(lines) {
    const top3 = [0, 0, 0];

    let current = 0;

    for (const line of lines) {
        if (line === '') {
            updateTop3(top3, current);

            current = 0;

            continue;
        }

        const i = parseInt(line);
        current += i;
    }

    updateTop3(top3, current);

    console.log(top3[0] + top3[1] + top3[2]);
}

function updateTop3(top3, newValue) {
    for (let i = 0; i < 3; i++) {
        if (newValue > top3[i]) {
            const temp = top3[i];
            top3[i] = newValue;
            newValue = temp;
        }
    }
}