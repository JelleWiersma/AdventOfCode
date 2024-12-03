using System.Text.RegularExpressions;

namespace AdventOfCode.twentytwentyfour {
    public static class day3 {
        // Place the input in bin/Debug/net8.0/input3.txt
        public static void Run() {
            Console.WriteLine("AdventOfCode 2024 day 3");
            var lines = new List<string>();
            using (var reader = new StreamReader("input3.txt")) {
                string line;
                while ((line = reader.ReadLine()) != null) {
                    lines.Add(line);
                }
            }

            int sum = 0;
            int matchesCount = 0;
            bool isDo = true;
            foreach (var l in lines) {
                var pattern = @"do\(\)|don't\(\)|mul\(([0-9]{1,3}),([0-9]{1,3})\)";
                var matches = Regex.Matches(l, pattern);
                foreach (Match match in matches) {
                    if (match.Success) {
                        if (match.Value == "do()") {
                            isDo = true;
                        } else if (match.Value == "don't()") {
                            isDo = false;
                        } else {
                            if (isDo) {
                                matchesCount++;
                                int firstInt = int.Parse(match.Groups[1].Value);
                                int secondInt = int.Parse(match.Groups[2].Value);
                                sum += firstInt * secondInt;
                            }
                        }
                    }
                }
            }
            Console.WriteLine($"the sum of {matchesCount} enabled matches is {sum}");
        }
    }
}