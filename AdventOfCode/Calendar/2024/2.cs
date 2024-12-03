namespace AdventOfCode.twentytwentyfour
    {
        public class Report {
            public List<int> Levels { get; set; }
            public string Input { get; set; }
            

            public Report(string input){
                Input = input;
                Levels = [];
                foreach(string level in input.Split(' ')){
                    Levels.Add(int.Parse(level));
                }
            }
            public bool AreAscending(int? removeAt, out int? removeCharacter){
                bool finalResult = false;
                int removedCharacter = 0;
                removeCharacter = null;

                //If we already removed a character, remove it from the list
                List<int> levels = new List<int>(Levels);
                if(removeAt != null){
                    levels.RemoveAt((int)removeAt);
                }

                for(int i = 0; i <= Levels.Count; i++){
                    bool usedRemove = false;
                    //If a valid config is found, break the loop
                    if(finalResult){
                        break;
                    }

                    //Try if removing the character at i makes the list ascending
                    if(i - 1 >= 0){
                        if(removeAt != null){
                            break;
                        }
                        usedRemove = true;
                        removedCharacter = levels[i-1];
                        levels.RemoveAt(i-1);
                    }

                    //Check if the list is ascending
                    bool result = true;
                    for(int j = 0; j < levels.Count - 1; j++){
                        if(levels[j] >= levels[j+1]){
                            result = false;
                            break;
                        }
                    }

                    //If the list is ascending, set the final result to true and break the loop
                    if(result){
                        finalResult = true;
                        if(usedRemove){
                            removeCharacter = i-1;
                        }
                        continue;
                    }

                    //If the list is not ascending, add the removed character back to the list
                    if(usedRemove){
                        levels.Insert(i-1, removedCharacter);
                    }
                    
                }
                return finalResult;
            }

            public bool AreDescending(int? removeAt, out int? removeCharacter){
                bool finalResult = false;
                int removedCharacter = 0;
                removeCharacter = null;

                //If we already removed a character, remove it from the list
                List<int> levels = new List<int>(Levels);
                if(removeAt != null){
                    levels.RemoveAt((int)removeAt);
                }

                //Test all possible configurations
                for(int i = 0; i <= Levels.Count; i++){
                    bool usedRemove = false;
                    //If a valid config is found, break the loop
                    if(finalResult){
                        break;
                    }

                    //Try if removing the character at i makes the list descending
                    if(i - 1 >= 0){
                        usedRemove = true;
                        removedCharacter = levels[i-1];
                        levels.RemoveAt(i-1);
                    }

                    //Check if the list is descending
                    bool result = true;
                    for(int j = 0; j < levels.Count - 1; j++){
                        if(levels[j] <= levels[j+1]){
                            result = false;
                            break;
                        }
                    }

                    //If the list is descending, set the final result to true and break the loop
                    if(result){
                        finalResult = true;
                        if(usedRemove){
                            removeCharacter = i-1;
                        }
                        continue;
                    }

                    //If the list is not descending, add the removed character back to the list
                    if(usedRemove){
                        levels.Insert(i-1, removedCharacter);
                    }
                    
                }
                return finalResult;
            }

            public bool HaveValidDifference(bool ascending, int? removeAt, out int? removeCharacter){
                bool finalResult = false;
                removeCharacter = null;
                int removedCharacter = 0;
                List<int> levels = new List<int>(Levels);

                //If a character is removed, remove it from the list
                if(removeAt != null){
                    levels.RemoveAt((int)removeAt);
                }

                //Try to remove a character and check if the difference between the levels is valid
                for(int i = 0; i <= levels.Count; i++){
                    //If a valid config is found, break the loop
                    if(finalResult){
                        break;
                    }

                    //Remove a character if we still can
                    bool usedRemove = false;
                    if(i - 1 >= 0){
                        if(removeAt != null){
                            break;
                        }
                        usedRemove = true;
                        removedCharacter = levels[i-1];
                        levels.RemoveAt(i-1);
                    }

                    bool result = true;
                    for(int j = 0; j < levels.Count - 1; j++){
                        if(ascending){
                            if(1 > levels[j+1] - levels[j] || levels[j+1] - levels[j] > 3){
                                result = false;
                                break;
                            }
                        } else {
                            if(1 > levels[j] - levels[j+1] || levels[j] - levels[j+1] > 3){
                                result = false;
                                break;
                            }
                        }
                    }

                    if(result){
                        finalResult = true;
                        if(usedRemove){
                            removeCharacter = i-1;
                        }
                        continue;
                    }

                    if(usedRemove){
                        levels.Insert(i-1, removedCharacter);
                    }
                }
                return finalResult;
            }

            public bool IsSafe(out int? removeCharacterAsc, out int? removeCharacterDesc, out int? removeCharacterDiff){
                removeCharacterAsc = null;
                removeCharacterDesc = null;
                removeCharacterDiff = null;

                if(AreAscending(null, out removeCharacterAsc)){
                    Console.WriteLine($"{Input} is ascending" + (removeCharacterAsc != null ? $" if you remove {Levels[(int)removeCharacterAsc]}" : ""));
                    if(HaveValidDifference(true, removeCharacterAsc, out removeCharacterDiff)){
                        Console.WriteLine($"Difference is valid" + (removeCharacterDiff != null ? $" if you remove {Levels[(int)removeCharacterDiff]}" : ""));
                        return true;
                    } else if(HaveValidDifference(true, null, out var removeCharacterDiff2) && AreAscending(removeCharacterDiff2, out var removeCharacterAsc2)){
                        Console.WriteLine($"Difference is valid" + (removeCharacterDiff2 != null ? $" if you remove {Levels[(int)removeCharacterDiff2]}" : ""));
                        removeCharacterDiff = removeCharacterDiff2;
                        removeCharacterAsc = removeCharacterAsc2;
                        return true;
                    }
                    return false;
                } else if (AreDescending(null, out removeCharacterDesc)) {
                    Console.WriteLine($"{Input} is descending" + (removeCharacterDesc != null ? $" if you remove {Levels[(int)removeCharacterDesc.Value]}" : ""));
                    if(HaveValidDifference(false, removeCharacterDesc, out removeCharacterDiff)){
                        Console.WriteLine($"Difference is valid" + (removeCharacterDiff != null ? $" if you remove {Levels[(int)removeCharacterDiff]}" : ""));
                        return true;
                    } else if(HaveValidDifference(false, null, out var removeCharacterDiff2) && AreDescending(removeCharacterDiff2, out var removeCharacterDesc2)){
                        Console.WriteLine($"Difference is valid" + (removeCharacterDiff2 != null ? $" if you remove {Levels[(int)removeCharacterDiff2]}" : ""));
                        removeCharacterDiff = removeCharacterDiff2;
                        removeCharacterDesc = removeCharacterDesc2;
                        return true;
                    }
                    return false;
                } 
                return false;         
            }
        }

        public static class day2 {
            public static void Run(){
                Console.WriteLine("AdventOfCode 2024 day 2\nEnter input:");
                string line;
                List<Report> reports = new List<Report>();
                while((line = Console.ReadLine()) != null){
                    if(line == ""){
                        break;
                    }
                    reports.Add(new Report(line));
                }

                int saveReports = 0;
                using (StreamWriter writer = new StreamWriter("output.txt"))
                {
                    for(int i = 0; i < reports.Count; i += 1){
                        var report = reports[i];
                        string levelsString = string.Join(' ', report.Levels);
                        if (report.IsSafe(out int? removeCharacterAsc, out int? removeCharacterDesc, out int? removeCharacterDiff))
                        {
                            saveReports += 1;
                            string output = $"Report {i}: {levelsString} is safe";
                            Console.WriteLine(output);
                            writer.WriteLine(output + (removeCharacterAsc != null ? $" if you remove {report.Levels[(int)removeCharacterAsc]}" : "") + (removeCharacterDesc != null ? $" if you remove {report.Levels[(int)removeCharacterDesc.Value]}" : "") + (removeCharacterDiff != null ? $" if you remove {report.Levels[(int)removeCharacterDiff]}" : ""));
                            continue;
                        }
                        string notSafeOutput = $"Report {i}: {levelsString} is not safe";
                        Console.WriteLine(notSafeOutput);
                    }
                    writer.WriteLine($"Safe reports: {saveReports}");
                }
                Console.WriteLine($"Safe reports: {saveReports}");
            }
        }
    }