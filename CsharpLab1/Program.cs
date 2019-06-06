using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace CsharpLab1
{
    class Program
    {
        static List<string> words = new List<string>();
        static StreamReader file = new StreamReader("Words.txt");
        static void Main(string[] args)
        {
            
            char choice = '0';
           

            while (true)
            {
                
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Hello World!!! My First C# App \nOption \n----------");

                Console.WriteLine("1 - Import Words From File\n2 - Bubble Sort words\n3 - LINQ / Lambda sort words\n4 - Count the Distinct Words\n5 - Take the first 10 words\n6 - Get the number of words that start with 'j' and display the count\n7 - Get and display of words that end with 'd' and display the count\n8 - Get and display of words that are greater than 4 characters long, and display the count\n9 - Get and display of words that are less than 3 characters long and start with the letter 'a', and display the count\nx - Exit\n\nMake a selection: ");

                choice = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (choice)
                {
                    case '1':
                        Console.Clear();
                        import();
                        Console.WriteLine("\n");
                        break;
                    case '2':
                        Console.Clear();
                        bubbleSort();
                        Console.WriteLine("\n");
                        break;
                    case '3':
                        Console.Clear();
                        lambdaSort();
                        Console.WriteLine("\n");
                        break;
                    case '4':
                        Console.Clear();
                        countDistinctWords();
                        Console.WriteLine("\n");
                        break;
                    case '5':
                        Console.Clear();
                        takeFirstWords();
                        Console.WriteLine("\n");
                        break;
                    case '6':
                        Console.Clear();
                        getWordsStartWith();
                        Console.WriteLine("\n");
                        break;
                    case '7':
                        Console.Clear();
                        getWordsEndWith();
                        Console.WriteLine("\n");
                        break;
                    case '8':
                        Console.Clear();
                        getWordsGreater();
                        Console.WriteLine("\n");
                        break;
                    case '9':
                        Console.Clear();
                        getWordsLess();
                        Console.WriteLine("\n");
                        break;
                    case 'x':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\n");
                        break;
                }
            }


            Console.ReadLine();
        }

        public static void import()
        {
            /*
            *i.	Import Words From File
                 1.	Create a method that takes the words from a text file and stores them in an array or List
                     a.	The namespace that provides this functionality is located in System.IO
                     b.	The easiest way to read a text file is to use the class System.IO.StreamReader, the constructor of which takes a path to the file you want to read
                     c.	For each word you find in the file add it to a List or array object. I recommend using a generic list of type List<string>. Coded as such: List<string> words = new List<string>(); Lists are a very powerful tool in .NET and provide dozens of extension methods that will help with this lab.
                 2.	Once the method runs, display the number of words you read from the file
                 3.	Test your method using the file ‘Words.txt’ provided in the example. 
            */
            string word;

            Console.WriteLine("Reading Words");

            while ((word = file.ReadLine()) != null)
            {
                words.Add(word);
            }

            Console.WriteLine("Reading Words complete");
            Console.WriteLine("Number of Words Found: " + words.Count);

        }

        public static string[] bubbleSort()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            for (int i = 0; i < words.Count - 1; i++)
            {
                for (int j = i + 1; j < words.Count; j++)
                {
                    if (words[j - 1].CompareTo(words[j]) > 0)
                    {
                        string temp = words[i];
                        words[i] = words[i + 1];
                        words[i + 1] = temp;
                    }
                }
            }

            watch.Stop();

            Console.Write("Time elapsed: " + watch.ElapsedMilliseconds + "ms\n");

            return words.ToArray();
        }

        public static void lambdaSort()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            var lengths = from word in words orderby word.Length select word;

            foreach (string result in lengths)
            {
                //Console.Write(result + " ");
            }
            watch.Stop();

            Console.Write("Time elapsed: " + watch.ElapsedMilliseconds + "ms\n");
        }

        public static void countDistinctWords()
        {
            var distinct = words.Distinct();
            int count = 0;

            foreach (string result in distinct)
            {
                //Console.Write(result + " ");
                ++count;
            }

            Console.Write("Number of Distinct Words Found: " + count+"\n");

        }

        public static void takeFirstWords()
        {
            var take = words.Take(10);

            foreach (var result in take)
            {
                Console.Write(result + "\n");
            }
        }

        public static void getWordsStartWith()
        {
            var sJ = from word in words where word.StartsWith("j") select word;
            int count = 0;

            foreach (var result in sJ)
            {
                Console.WriteLine(result);
                ++count;
            }
            Console.WriteLine("Number of words that start with 'j': " + count);
        }

        public static void getWordsEndWith()
        {
            var wE = from word in words where word.EndsWith("d") select word;
            int count = 0;
            foreach (var result in wE)
            {
                Console.WriteLine(result);
                ++count;
            }
            Console.WriteLine("Number of words that end with 'd': " + count);

        }

        public static void getWordsGreater()
        {
            var wG = from word in words where word.Length > 4 select word;
            int count = 0;

            foreach (var result in wG)
            {
                Console.WriteLine(result);
                ++count;
            }
            Console.WriteLine("Number of words longer than 4 characters: " + count);

        }

        public static void getWordsLess()
        {
            int count = 0;
            var wL = from word in words where word.StartsWith("a") && word.Length < 3 select word;

            foreach (var results in wL)
            {
                Console.WriteLine(results);
                ++count;
            }
            Console.WriteLine("Number of words longer less than 3 characters and start with 'a': " + count);
        }
    }
}
