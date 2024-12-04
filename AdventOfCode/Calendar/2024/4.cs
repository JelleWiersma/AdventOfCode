using System.Text.RegularExpressions;

namespace AdventOfCode.twentytwentyfour {
    public static class day4 {
        // Place the input in bin/Debug/net8.0/input4.txt
        public static void Run(bool part1) {
            Console.WriteLine("AdventOfCode 2024 day 4");
            var lines = new List<string>();
            using (var reader = new StreamReader("input4.txt")) {
                string line;
                while ((line = reader.ReadLine()) != null) {
                    lines.Add(line);
                }
            }

            char[][] matrix = new char[lines.Count][];
            for(int i = 0; i < lines.Count; i++) {
                matrix[i] = lines[i].ToCharArray();
            }

            if(part1){
                int sum = 0;
                var pattern = @"(?=(XMAS|SAMX))";

                // Check all horizontal lines
                Console.WriteLine("Checking horizontal lines");
                foreach (var l in lines) {
                    var matches = Regex.Matches(l, pattern);
                    foreach (Match match in matches) {
                        if (match.Success) {
                            sum++;
                        }
                    }
                    Console.WriteLine($"found {matches.Count} matches in\n{l}\nMaking the sum {sum}");
                }

                // Check all vertical lines
                Console.WriteLine("Checking vertical lines");
                for (int i = 0; i < matrix[0].Length; i++) {
                    string verticalLine = "";
                    for (int j = 0; j < matrix.Length; j++) {
                        verticalLine += matrix[j][i];
                    }
                    var matches = Regex.Matches(verticalLine, pattern);
                    foreach (Match match in matches) {
                        if (match.Success) {
                            sum++;
                        }
                    }
                    Console.WriteLine($"{i}: found {matches.Count} matches in\n{verticalLine}\nMaking the sum {sum}");
                }

                // Check all diagonals from top left to bottom right, starting with the left bottom half
                Console.WriteLine("Checking diagonals from top left to bottom right, first half");
                for (int i = 0; i < matrix.Length; i++) {
                    string diagonalLine = "";
                    for (int j = 0; j < matrix.Length; j++) {
                        if (i + j < matrix.Length) {
                            diagonalLine += matrix[i + j][j];
                        }
                    }
                    var matches = Regex.Matches(diagonalLine, pattern);
                    foreach (Match match in matches) {
                        if (match.Success) {
                            sum++;
                        }
                    }
                    Console.WriteLine($"{i}: found {matches.Count} matches in\n{diagonalLine}\nMaking the sum {sum}");
                }

                // Check all diagonals from top left to bottom right, starting from the second column to the top right.
                Console.WriteLine("Checking diagonals from top left to bottom right, second half");
                for (int i = 1; i < matrix.Length; i++) {
                    string diagonalLine = "";
                    for (int j = 0; j < matrix.Length; j++) {
                        if (i + j < matrix.Length) {
                            diagonalLine += matrix[j][i + j];
                        }
                    }
                    var matches = Regex.Matches(diagonalLine, pattern);
                    foreach (Match match in matches) {
                        if (match.Success) {
                            sum++;
                        }
                    }
                    Console.WriteLine($"{i}: found {matches.Count} matches in\n{diagonalLine}\nMaking the sum {sum}");
                }

                // Check all diagonals from top right to bottom left
                Console.WriteLine("Checking diagonals from top right to bottom left, first half");
                for (int i = 0; i < matrix.Length; i++) {
                    string diagonalLine = "";
                    for (int j = 0; j < matrix.Length; j++) {
                        if (i - j >= 0) {
                            diagonalLine += matrix[i - j][j];
                        }
                    }
                    var matches = Regex.Matches(diagonalLine, pattern);
                    foreach (Match match in matches) {
                        if (match.Success) {
                            sum++;
                        }
                    }
                    Console.WriteLine($"{i}: found {matches.Count} matches in\n{diagonalLine}\nMaking the sum {sum}");
                }

                // Check all diagonals from top right to bottom left, second half
                Console.WriteLine("Checking diagonals from top right to bottom left, second half");
                for(int i = 1; i < matrix.Length; i++) {
                    string diagonalLine = "";
                    for(int j = 0; j < matrix.Length; j++) {
                        if(i + j < matrix.Length) {
                            diagonalLine += matrix[matrix.Length - 1 - j][i + j];
                        }
                    }
                    var matches = Regex.Matches(diagonalLine, pattern);
                    foreach(Match match in matches) {
                        if(match.Success) {
                            sum++;
                        }
                    }
                    Console.WriteLine($"{i}: found {matches.Count} matches in\n{diagonalLine}\nMaking the sum {sum}");
                }
            } else {
                int sum = 0;
                List<char[][]> possibleMatches = new List<char[][]>{
                    new char[][] {
                        ['S', '*', 'M'],
                        ['*', 'A', '*'],
                        ['S', '*', 'M']
                    },
                    new char[][] {
                        ['S', '*', 'S'],
                        ['*', 'A', '*'],
                        ['M', '*', 'M']
                    },
                    new char[][] {
                        ['M', '*', 'S'],
                        ['*', 'A', '*'],
                        ['M', '*', 'S']
                    },
                    new char[][] {
                        ['M', '*', 'M'],
                        ['*', 'A', '*'],
                        ['S', '*', 'S']
                    }
                };
                for(int row = 0; row < matrix.Length - 2; row++) {
                    for(int col = 0; col < matrix[0].Length - 2; col++) {
                        char[][] checking = [
                            matrix[row][col..(col + 3)],
                            matrix[row + 1][col..(col + 3)],
                            matrix[row + 2][col..(col + 3)]
                        ];
                        Console.WriteLine($"checking\n{string.Join("",checking[0])}\n{string.Join("",checking[1])}\n{string.Join("",checking[2])}");
                        foreach(char[][] match in possibleMatches) {
                            bool isMatch = true;
                            for(int subRow = 0; subRow < 3; subRow++) {
                                for(int subCol = 0; subCol < 3; subCol++) {
                                    char checkChar = checking[subRow][subCol];
                                    // Console.WriteLine($"checking {checkChar}");
                                    if(match[subRow][subCol] != '*' && (checkChar != match[subRow][subCol])) {
                                        isMatch = false;
                                        // Console.WriteLine("not a match");
                                        break;
                                    }
                                    if(subRow == 2 && subCol == 2) {
                                        sum++;
                                        Console.WriteLine("found a match");
                                    }
                                }
                                if(!isMatch) {
                                    break;
                                }
                            } if(!isMatch) {
                                continue;
                            }
                        }
                    }
                }
                Console.WriteLine($"found {sum} matches");
            }

            
            
        }
    }
}