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
                if (command == "quit")
                {
                    Console.WriteLine("Goodbye!");
                }

                //FIXME File not found exception for invalid filename. fix 
                else if (command == "load")
                {
                    if (argument.Length == 2)
                    {
                        LoadFile(argument[1]);
                    }
                    else if (argument.Length == 1)
                    {
                        LoadFile(defaultFile);
                    }
                }
                else if (command == "list")
                {//FIXME null reference exception thrown if nothing has been loaded
                    foreach (SweEngGloss gloss in dictionary)
                    {
                        Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
                    }
                }
                else if (command == "new")
                {//FIXME null reference exception thrown if nothing has been loaded
                    if (argument.Length == 3)
                    {
                        dictionary.Add(new SweEngGloss(argument[1], argument[2]));
                    }
                    else if (argument.Length == 1)
                    {
                        string swedishWord = UserInput("Write word in Swedish: ");
                        string englishWord = UserInput("Write word in English: ");
                        dictionary.Add(new SweEngGloss(swedishWord, englishWord));
                    }
                }
                else if (command == "delete")
                //FIXME null reference exception thrown if nothing has been loaded
                {   //FIXME crashes if argument not 3 or 1
                    //FIXME crashes is english and swedish words dont match.
                    if (argument.Length == 3)
                    {
                        DeleteEntry(argument[1], argument[2]);
                    }
                    else if (argument.Length == 1)
                    {
                        string swedishWord = UserInput("Write word in Swedish: ");

                        string englishWord = UserInput("Write word in English: ");

                        DeleteEntry(swedishWord, englishWord);
                    }
                }
                else if (command == "translate")
                {//FIXME null reference exception thrown if nothing has been loaded
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
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (true);
        }
        static void LoadFile(string file)
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

        static string UserInput(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine().Trim();
        }

        static void DeleteEntry(string swedish, string english)
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

        static void Translate(string wordToTranslate)
        {
            foreach (SweEngGloss gloss in dictionary)
            {
                if (gloss.word_swe == wordToTranslate)
                    Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                if (gloss.word_eng == wordToTranslate)
                    Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
            }
        }
    }
}

