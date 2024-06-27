using System.Diagnostics;
using System.IO;
using System;
using System.Drawing;


namespace A3_AVL
{
    internal class Program
    {
        private static AVLTree avl = new AVLTree();
        static void Main(string[] args)
        {
            MyMenuUI();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();


        }

        #region MenuUI
        // Displays the menu for user interaction.
        static void MyMenuUI()
        {
            int opt = 0;
            while (opt < 1 || opt > 8)
            {
                Console.Clear();
                Console.WriteLine("**************************************************");
                Console.WriteLine("==== Assignment 3: Adelson-Velskii and Landis ====");
                Console.WriteLine("");
                Console.WriteLine(" This is the assignment 3 Part 2, \n" +
                    " focusing on AVL (Adelson-Velskii and Landis) Tree algorithm.\n" +
                    " - By Jung Kim");
                Console.WriteLine("");
                Console.WriteLine("**************************************************");
                Console.WriteLine("");

                Console.WriteLine("||================= MENU =================||");
                Console.WriteLine("");
                Console.WriteLine("1 - Load Files");
                Console.WriteLine("2 - Insert Operation");
                Console.WriteLine("3 - Find Operation");
                Console.WriteLine("4 - Delete Operation");
                Console.WriteLine("5 - Print Operation");
                Console.WriteLine("6 - Test Operation Demonstrations");
                Console.WriteLine("7 - Measuring Time");
                Console.WriteLine("8 - Quit");
                Console.WriteLine("");
                Console.WriteLine("||========================================||");
                Console.WriteLine("Select a menu (1-8): ");

                if (!int.TryParse(Console.ReadLine(), out opt) || opt < 1 || opt > 8)
                {
                    Console.WriteLine("*** ERROR: Invalid Option ***");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    continue;
                }

                EvalMenuOpt(opt);
            }
        }

        // Evaluates and performs the selected menu option.
        static void EvalMenuOpt(int opt)
        {
            //valuation-evaluating options
            switch (opt)
            {
                case 1:
                    Console.WriteLine("=== Load Files ===");
                    LoadFiles();
                    PrintList();
                    break;
                case 2:
                    Console.WriteLine("=== Insert Operation ===");
                    InsertOperation();
                    PrintList();
                    break;
                case 3:
                    Console.WriteLine("=== Find Operation ===");
                    FindOperation();
                    PrintList();
                    break;
                case 4:
                    Console.WriteLine("=== Delete Operation ===");
                    DeleteOperation();
                    PrintList();
                    break;
                case 5:
                    Console.WriteLine("=== Print Operation ===");
                    PrintOperation();
                    break;
                case 6:
                    Console.WriteLine("=== Test Operation Demonstrations ===");
                    TestDemonstrations();
                    break;
                case 7:
                    Console.WriteLine("=== Measure Time ===");
                    MeasureTime();

                    break;
                case 8:
                    Console.WriteLine("Exiting...");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("*** ERROR: Invalid Option. Please select an option (1-8).");
                    break;
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            MyMenuUI();
        }
        #endregion

        #region LoadFiles
        private static void LoadFiles()
        {
            try
            {
                avl.Clear();
                Console.Clear();
                Console.WriteLine("========= Load Files =========");
                Console.WriteLine("** Select a folder **");
                Console.WriteLine("1. Ordered Folder");
                Console.WriteLine("2. Random Folder");
                Console.WriteLine("-----------------------------------");
                Console.Write("Select a folder number(1-2): ");

                int folderChoice;
                if (!int.TryParse(Console.ReadLine(), out folderChoice) || folderChoice < 1 || folderChoice > 2)
                {
                    Console.WriteLine("Invalid choice.");
                    return;
                }

                string[] fileNames = {
                    "1000-words.txt",
                    "5000-words.txt",
                    "10000-words.txt",
                    "15000-words.txt",
                    "20000-words.txt",
                    "25000-words.txt",
                    "30000-words.txt",
                    "35000-words.txt",
                    "40000-words.txt",
                    "45000-words.txt",
                    "50000-words.txt",
                };
                string folderPath;
                if (folderChoice == 1)
                {
                    folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\TempFile\ordered");
                    Console.WriteLine("\n*** Ordered Folder ***");
                }
                else
                {
                    folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\TempFile\random");
                    Console.WriteLine("\n*** Random Folder ***");
                }

                for (int i = 0; i < fileNames.Length; i++)
                {   //show the file names
                    Console.WriteLine($"{i + 1}. {fileNames[i]}");
                }
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("Select a file to load(1-11): ");

                int fileChoice = int.Parse(Console.ReadLine());
                if (fileChoice < 1 || fileChoice > fileNames.Length)
                {
                    Console.WriteLine("Invalid file choice.");
                    return;
                }
                // Construct the file path of the selected file
                string fileName = fileNames[fileChoice - 1];
                string filePath = Path.Combine(folderPath, fileName);

                Console.WriteLine($"\n You selected '{fileNames[fileChoice - 1]}' ");

                Stopwatch sw1 = new Stopwatch();
                sw1.Start();
                //Load selected file into dictionary
                avl.ReadWordsFromFile(filePath);

                sw1.Stop();
                TimeSpan loadTime = sw1.Elapsed;
                Console.WriteLine("==> 'File loaded successfully.'");
                Console.WriteLine($"- Number of words:  {avl.GetNodeCount()}");
                Console.WriteLine($"- Time taken to perform insert: {loadTime.TotalMilliseconds,-5:F2} ms");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading files: {ex.Message}");
            }
        }
        #endregion

        #region Insert Operation
        private static void InsertOperation()
        {
            Console.WriteLine("Enter the word to insert: ");
            string word = Console.ReadLine();
            Stopwatch sw0 = new Stopwatch();
            sw0.Start();
            avl.Add(word);
            sw0.Stop();
            TimeSpan addTime = sw0.Elapsed;
            Console.WriteLine($"Word '{word}' inserted into tree.");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"- Time taken to perform adding word: {addTime.TotalMilliseconds,-5:F2} ms");

        }
        #endregion

        #region Find Operation
        private static void FindOperation()
        {
            Console.WriteLine("Enter the word to find: ");
            string word = Console.ReadLine();
            Stopwatch sw2 = new Stopwatch();
            sw2.Start();

            Console.WriteLine(avl.Find(word));

            sw2.Stop();
            TimeSpan findTime = sw2.Elapsed;
            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"- Time taken to perform find word: {findTime.TotalMilliseconds,-5:F2} ms");
        }
        #endregion

        #region Delete Operation

        private static void DeleteOperation()
        {
            Console.Write("Enter word to delete: ");
            string word = Console.ReadLine();
            Console.WriteLine(avl.Remove(word));
        }
        #endregion

        #region Print Operation
        private static void PrintOperation()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== Print Operation =====");
                Console.WriteLine("1. Print tree Pre Order");
                Console.WriteLine("2. Print tree In Order");
                Console.WriteLine("3. Print tree Post Order");
                Console.WriteLine("4. End Print Operation");
                Console.WriteLine("============================");
                Console.WriteLine("Enter your choice (1-4): ");
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid choice. Please enter a number (1-4).");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("===== Pre Order ===== ");
                        Console.WriteLine(avl.PreOrder());
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("===== In Order ===== ");
                        Console.WriteLine(avl.InOrder());
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("===== Post Order ===== ");
                        Console.WriteLine(avl.PostOrder());
                        break;
                    case 4:
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                        continue;
                }
                Console.WriteLine("------------------------------------------");
                Console.WriteLine($"- Number of words:  {avl.GetNodeCount()}");
                Console.WriteLine(" ");
                Console.WriteLine(avl.TreeDetails());
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("Press Enter to return to the Print Menu");
                Console.ReadLine();

            }
        }

        //ask user to print tree now
        private static void PrintList()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Do you want to print the tree? (y/n): ");
            string input = Console.ReadLine().ToLower();
            Console.Clear();
            if (input == "y")
            {
                PrintOperation();
            }
            else
            {
                Console.WriteLine("Tree printing canceled.");
            }
        }
        #endregion

        #region Test Operation Demonstrations
        private static void TestDemonstrations()
        {
            Console.Clear();
            int step = 1;

            while (step <= 5)
            {
                switch (step)
                {
                    case 1:
                        Console.WriteLine("== Load Files ==");
                        LoadFiles();
                        PrintList();
                        break;
                    case 2:
                        Console.WriteLine("== Insert Operation ==");
                        InsertOperation();
                        PrintList();
                        break;
                    case 3:
                        Console.WriteLine("== Find Operation ==");
                        FindOperation();
                        break;
                    case 4:
                        Console.WriteLine("== Delete Operation ==");
                        DeleteOperation();
                        break;
                    case 5:
                        Console.WriteLine("== Print Operation ==");
                        PrintList();
                        break;

                }

                // Ask user to go back and next
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("=> Enter 'B' for 'Back', 'N' for 'Next', 'M' for 'Menu':");
                char choice = char.ToLower(Console.ReadKey().KeyChar);

                switch (choice)
                {
                    case 'b':
                        if (step > 1)
                        {
                            step--;
                        }
                        break;
                    case 'n':
                        if (step < 5)
                        {
                            step++;
                        }
                        else if (step == 5)
                        {
                            return;
                        }
                        break;
                    case 'm':
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        continue;
                }

                Console.Clear();
            }
        }
        #endregion

        #region Measure Time
        private static void MeasureTime()
        {
            Console.Clear();
            Console.WriteLine("=== Measure Time ===");

            // Display menu for selecting folder
            Console.WriteLine("Select the folder:");
            Console.WriteLine("1. Ordered Folder");
            Console.WriteLine("2. Random Folder");
            Console.Write("Enter your choice (1-2): ");
            int folderChoice;
            if (!int.TryParse(Console.ReadLine(), out folderChoice) || (folderChoice != 1 && folderChoice != 2))
            {
                Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                return;
            }

            string folderPath;

            if (folderChoice == 1)
            {
                folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\TempFile\ordered");
                Console.WriteLine("\n*** Ordered Folder ***");
            }
            else
            {
                folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\TempFile\random");
                Console.WriteLine("\n*** Random Folder ***");
            }

            // List of file names in the desired order
            string[] fileNames =
            {
                "1000-words.txt",
                "5000-words.txt",
                "10000-words.txt",
                "15000-words.txt",
                "20000-words.txt",
                "25000-words.txt",
                "30000-words.txt",
                "35000-words.txt",
                "40000-words.txt",
                "45000-words.txt",
                "50000-words.txt"
            };

            // Create or append to a CSV file for insertion time
            string insertionCsvFilePath = @"..\..\..\Report\InsertionTime.csv";
            string searchCsvFilePath = @"..\..\..\Report\SearchTime.csv";

            using (StreamWriter insertionSw = new StreamWriter(insertionCsvFilePath))
            using (StreamWriter searchSw = new StreamWriter(searchCsvFilePath))
            {
                // Write the headers
                insertionSw.WriteLine("FileName,InsertionTime(ms)");
                searchSw.WriteLine("FileName,SearchTime(ms)");

                // Display insertion times
                Console.WriteLine("Measuring insertion time for each file:");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("FileName                  Insert Time(ms)");
                Console.WriteLine("-------------------------------------------");

                foreach (string fileName in fileNames)
                {
                    // Measure insertion time
                    double insertionTime = MeasureInsertionTime(folderPath, fileName);
                    insertionSw.WriteLine($"{fileName},{insertionTime}");

                    // Display the result for the current file
                    Console.WriteLine($"{Path.GetFileNameWithoutExtension(fileName),-25} {insertionTime,10:F2} ms");

                    // Clear BST after each measurement
                    avl.Clear();
                }

                Console.WriteLine("\nPress any key to measure search time...");
                Console.ReadKey();

                // Prompt the user to enter the word to search
                Console.Write("Enter a word to search: ");
                string word = Console.ReadLine();

                // Display search times
                Console.WriteLine("Measuring searching time for each file:");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("FileName                  Search Time(ms)");
                Console.WriteLine("-------------------------------------------");

                foreach (string fileName in fileNames)
                {
                    // Measure search time
                    double searchTime = MeasureSearchTime(folderPath, fileName, word);
                    searchSw.WriteLine($"{fileName},{searchTime}");

                    // Display the search time for the word
                    Console.WriteLine($"{Path.GetFileNameWithoutExtension(fileName),-25} {searchTime,10:F4} ms");

                    // Clear BST after each measurement
                    avl.Clear();
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Insertion and search time measurement completed.");
        }

        private static double MeasureInsertionTime(string folderPath, string fileName)
        {
            // Clear the BST before inserting new data
            avl.Clear();

            Stopwatch swTimer = new Stopwatch();
            string filePath = Path.Combine(folderPath, fileName);

            swTimer.Start();
            avl.ReadWordsFromFile(filePath);
            swTimer.Stop();

            return swTimer.Elapsed.TotalMilliseconds;
        }

        private static double MeasureSearchTime(string folderPath, string fileName, string word)
        {
            // Load the file into BST
            avl.Clear(); // Clear the BST before searching
            string filePath = Path.Combine(folderPath, fileName);
            avl.ReadWordsFromFile(filePath);

            // Measure search time
            Stopwatch swTimer = new Stopwatch();
            swTimer.Start();
            avl.Find(word);
            swTimer.Stop();

            return swTimer.Elapsed.TotalMilliseconds;
        }

        #endregion























    }
}
