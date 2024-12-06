namespace AdventOfCode.twentytwentyfour {
    public static class day5 {
        // Place the input in bin/Debug/net8.0/input5.txt
        public static void Run() {
            Console.WriteLine("AdventOfCode 2024 day 5");
            List<int[]> rulesList = [];
            List<List<int>> updates = [];
            using (var reader = new StreamReader("input5.txt")) {
                string line;
                bool isRules = true;
                while ((line = reader.ReadLine()) != null) {
                    if(line == ""){
                        isRules = false;
                        continue;
                    }
                    if(isRules){
                        var rule = line.Split("|");
                        rulesList.Add([int.Parse(rule[0]), int.Parse(rule[1])]);
                    } else {
                        updates.Add([]);
                        var update = line.Split(",");
                        foreach (var u in update) {
                            updates.Last().Add(int.Parse(u));
                        }
                    }
                }
            }

            Lookup<int, int> rules = (Lookup<int, int>)rulesList.ToLookup(x => x[0], x => x[1]);
            int correctSum = 0;
            int inCorrectSum = 0;

            foreach (var update in updates) {
                List<int> pagesInOrder = new(update);

                foreach(int page in update){
                    List<int> ShouldBeBefore = rules[page].ToList();
                    List<int> ShouldBeAfter = rules.Where(g => g.Contains(page)).Select(g => g.Key).ToList();
                    Console.WriteLine($"Page: {page} Should be before: {string.Join(",", ShouldBeBefore)} and Should be after: {string.Join(",", ShouldBeAfter)}");
                    int shouldBeAtIndex = pagesInOrder.IndexOf(page);
                    foreach (int checkedPage in pagesInOrder) {
                        if(checkedPage == page){
                            continue;
                        }

                        int checkedIndex = pagesInOrder.IndexOf(checkedPage);

                        if(ShouldBeBefore.Contains(checkedPage)){
                            
                            if(checkedIndex < shouldBeAtIndex){
                                shouldBeAtIndex = checkedIndex;
                            }
                        }

                        if(ShouldBeAfter.Contains(checkedPage)){
                            if(checkedIndex > shouldBeAtIndex){
                                shouldBeAtIndex = checkedIndex;
                            }
                        }
                    }
                    pagesInOrder.RemoveAt(pagesInOrder.IndexOf(page));
                    try{
                        pagesInOrder.Insert(shouldBeAtIndex, page);
                    } catch (Exception e){
                        pagesInOrder.Add(page);
                    }
                    

                }

                int middlePage = (int)Math.Floor(update.Count / 2.0);
                Console.WriteLine($"Update: {string.Join(",", update)}");
                Console.WriteLine($"Pages:  {string.Join(",", pagesInOrder)}");
                Console.WriteLine("--------------------");
                if(Enumerable.SequenceEqual(pagesInOrder, update)){
                    correctSum += pagesInOrder[middlePage];
                } else {
                    inCorrectSum += pagesInOrder[middlePage];
                }
            }
            Console.WriteLine($"Correct Sum: {correctSum}");
            Console.WriteLine($"Incorrect Sum: {inCorrectSum}");
        }
    }
}