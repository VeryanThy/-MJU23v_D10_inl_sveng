using System;
using System.Collections.Generic;
using System.Reflection;

namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        static List<SweEngGloss> dictionary;
        class SweEngGloss
        {
            public string word_swe, word_eng;
            public SweEngGloss(string word_swe, string word_eng)
            {
                this.word_swe = word_swe; this.word_eng = word_eng;
            }
            public SweEngGloss(string line)
            {
                string[] words = line.Split('|');
                this.word_swe = words[0]; this.word_eng = words[1];
            }
        }
        static void Main(string[] args)
        {   //FIXME not universal file path
            string defaultFile = "../../../dict/sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!");
            do
            {
                Console.Write("> ");
                string[] argument = Console.ReadLine().Split();
                string command = argument[0];
                switch (command)
                {
                    case "quit":
                        {
                            Console.WriteLine("Goodbye!");
                            return;
                        }

                     
                    case "load":
                        {
                            if (argument.Length == 2)
                            {
                                LoadFile(argument[1]);
                            }
                            else if (argument.Length == 1)
                            {
                                LoadFile(defaultFile);
                            }
                            else
                            {
                                Console.WriteLine("Invalid file path!");
                            }
                            break;
                        }
                    case "list":
                        {
                            try
                            {
                                foreach (SweEngGloss gloss in dictionary)
                                {
                                    Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
                                }
                            }
                            catch (NullReferenceException)
                            {
                                Console.WriteLine("No file added to list!");
                                continue;
                            }
                            break;
                        }
                    case "new":
                        {
                            try
                            {
                                if (argument.Length == 3)
                                {
                                    dictionary.Add(new SweEngGloss(argument[1], argument[2]));
                                }
                                else
                                {
                                    string swedishWord = UserInput("Write word in Swedish: ");
                                    string englishWord = UserInput("Write word in English: ");
                                    dictionary.Add(new SweEngGloss(swedishWord, englishWord));
                                }
                            }
                            catch (NullReferenceException)
                            {
                                Console.WriteLine("No list loaded to add to!");
                                continue;
                            }
                            break;
                        }
                    case "delete":
                        
                        {
                            try
                            {
                                if (argument.Length == 3)
                                {
                                    DeleteEntry(argument[1], argument[2]);
                                }
                                else
                                {
                                    string swedishWord = UserInput("Write word in Swedish: ");

                                    string englishWord = UserInput("Write word in English: ");

                                    DeleteEntry(swedishWord, englishWord);
                                }
                            }
                            catch (NullReferenceException)
                            {
                                Console.WriteLine("No file added yet!");
                                continue;
                            }
                            break;
                        }
                    case "translate":
                        {
                            try
                            {
                                if (argument.Length == 2)
                                {
                                    Translate(argument[1]);
                                }
                                else if (argument.Length == 1)
                                {
                                    string wordToTranslate = UserInput("Write word to be translated: ");
                                    Translate(wordToTranslate);
                                }
                            }
                            catch (NullReferenceException)
                            {
                                Console.WriteLine("No file added to translate!");
                                continue;
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine($"Unknown command: '{command}'");
                            break;
                        }
                }
            }
            while (true);
        }
        static void LoadFile(string file)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    dictionary = new List<SweEngGloss>(); // Empty it!
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        SweEngGloss gloss = new SweEngGloss(line);
                        dictionary.Add(gloss);
                        line = sr.ReadLine();
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found, please check file name and try again!");
            }
            catch (FileLoadException)
            {
                Console.WriteLine("Sorry, unable to load that file, check file contents format and try again!");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Sorry, unable to load that file, check file format and try again!");
            }
        }

        static string UserInput(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine().Trim();
        }

        static void DeleteEntry(string swedish, string english)
        {
            try
            {
                int index = -1;
                for (int i = 0; i < dictionary.Count; i++)
                {
                    SweEngGloss gloss = dictionary[i];
                    if (gloss.word_swe == swedish && gloss.word_eng == english)
                    {
                        index = i;
                    }
                }
                dictionary.RemoveAt(index);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("That word combination not found in dictionary, please try again!");
            }
            }

        static void Translate(string wordToTranslate)
        {
            bool isFound = false;
            foreach (SweEngGloss gloss in dictionary)
            {
                if (gloss.word_swe == wordToTranslate)
                {
                    Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                    isFound = true;
                }
                if (gloss.word_eng == wordToTranslate)
                {
                    Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                    isFound = true;
                }
            }
            if (!isFound)
            {
                Console.WriteLine("Word not found in dictionary!");
            }

        }
    }
}

