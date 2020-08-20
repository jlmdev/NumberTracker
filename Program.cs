using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using System.Globalization;

namespace NumberTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Number Tracker");

            TextReader reader;

            if (File.Exists("numbers.csv")) ;
            {
                // if the file exists
                // Assign a streamreader to read from the file
                reader = new StreamReader("numbers.csv");
            }
            else
            {
                // The file does not exist
                // Read the data from the empty string instead
                reader = new StringReader("");
            }

            // Creates a list of numbers we will be tracking
            // Creates a stream reader to get information from our file
            var fileReader = new StreamReader("numbers.csv");

            // Create a CSV reader to parse the stream into CSV format
            var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);

            // Tell the CSV reader not to interpret the first row as a header, otherwise the first number will be skipped.
            csvReader.Configuration.HasHeaderRecord = false;

            // Get the records from the CSV reader, as 'int' and finally as a 'list'
            var numbers = csvReader.GetRecords<int>().ToList();

            // Close the reader
            fileReader.Close();

            // Controls if we are still running our loop asking for more numbers
            var isRunning = true;

            // While we are running
            while (isRunning)
            {
                // Show the list of numbers
                Console.WriteLine("------------------");
                foreach (var number in numbers)
                {
                    Console.WriteLine(number);
                }
                Console.WriteLine($"Our list has: {numbers.Count()} entries");
                Console.WriteLine("------------------");

                // Ask for a new number or the word 'quit' to end
                Console.Write("Enter a number to store, or 'quit' to end: ");
                var input = Console.ReadLine().ToLower();

                if (input == "quit")
                {
                    // If the input is quit, turn off the flag to keep looking
                    isRunning = false;
                }
                else
                {
                    // Parse the number and add it to the list of numbers
                    var number = int.Parse(input);
                    numbers.Add(number);
                }
            }

            // Create a stream for writing information into a file
            var fileWriter = new StreamWriter("numbers.csv");

            // Create an object that can write CSV to the filewriter
            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);

            // Ask our csvWriter to write out our list of numbers
            csvWriter.WriteRecords(numbers);

            // Tell the file we are done
            fileWriter.Close();
        }
    }
}
