namespace AdventOfCode.twentytwentyfour {
    public static class day1 {
        // Place the input in bin/Debug/net8.0/input1.txt
        public static void Run(){
            Console.WriteLine("AdventOfCode 2024 day 1");
            var lines = new List<string>();
            using (var reader = new StreamReader("input1.txt")) {
                string line;
                while ((line = reader.ReadLine()) != null) {
                    lines.Add(line);
                }

                List<int> numsLeft = new List<int>();
                List<int> numsRight = new List<int>();
                foreach (var l in lines) {
                    string[] nums = l.Split("   ");
                    numsLeft.Add(int.Parse(nums[0]));
                    numsRight.Add(int.Parse(nums[1]));
                }
                
                // Part 1
                Stack<int> stackLeft = new Stack<int>(numsLeft.OrderByDescending(x => x).ToList());
                Stack<int> stackRight = new Stack<int>(numsRight.OrderByDescending(x => x).ToList());

                int sum = 0;
                while (stackLeft.Count > 0 && stackRight.Count > 0) {
                    int left = stackLeft.Pop();
                    int right = stackRight.Pop();
                    int diff = Math.Abs(left - right);
                    sum += diff;
                }

                Console.WriteLine($"the sum of the differences is {sum}");

                // Part 2
                sum = 0;
                foreach(int num in numsLeft) {
                    int amount = numsRight.Where(x => x == num).Count();
                    sum += num * amount;
                }

                Console.WriteLine($"the sum of the same numbers is {sum}");
            }

            
        }
    }
}