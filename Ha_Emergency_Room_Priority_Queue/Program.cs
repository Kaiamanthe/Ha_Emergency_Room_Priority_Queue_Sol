using System;
using System.IO;

namespace Ha_Emergency_Room_Priority_Queue
{
    internal class Program
    {
        static ERQueue eRQueue = new ERQueue();

        static void Main(string[] args)
        {
            string preRecords = FindCsvPath("Patients-1.csv");
            eRQueue.LoadPreRecords(preRecords);

            string choice = string.Empty;

            do
            {
                Console.WriteLine("Main Menu");
                Console.WriteLine("(A)dd Patient");
                Console.WriteLine("(P)rocess Current Patient");
                Console.WriteLine("(L)ist All in Queue");
                Console.WriteLine("(Q)uit");

                choice = (Console.ReadLine() ?? "").Trim().ToUpper();

                if (choice == "A")
                {
                    Console.Clear();
                    AddPatient();
                }
                else if (choice == "P")
                {
                    Console.Clear();
                    ProcessPatient();
                }
                else if (choice == "L")
                {
                    Console.Clear();
                    eRQueue.ListPatients();
                }

            } while (choice != "Q");

            Console.WriteLine();
            Console.WriteLine("End Reached");
        }

        static string FindCsvPath(string fileName)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string[] possiblePaths =
            {
                Path.Combine(baseDir, fileName),
                Path.Combine(Directory.GetCurrentDirectory(), fileName),
                Path.Combine(Directory.GetCurrentDirectory(), "Ha_Emergency_Room_Priority_Queue", fileName),
                Path.Combine(baseDir, @"..\..\..\", fileName),
                Path.Combine(baseDir, @"..\..\..\..\",
                    "Ha_Emergency_Room_Priority_Queue", fileName)
            };

            foreach (string path in possiblePaths)
            {
                string fullPath = Path.GetFullPath(path);
                if (File.Exists(fullPath))
                {
                    return fullPath;
                }
            }

            return Path.Combine(baseDir, fileName);
        }

        static void AddPatient()
        {
            Console.Write("Enter First Name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine() ?? "";

            DateTime dob;
            while (true)
            {
                Console.Write("Enter Date of Birth (MM/DD/YYYY): ");
                if (DateTime.TryParse(Console.ReadLine(), out dob))
                {
                    break;
                }

                Console.WriteLine("Invalid date.");
            }

            int priority;
            while (true)
            {
                Console.Write("Enter Priority (1-5): ");
                if (int.TryParse(Console.ReadLine(), out priority) && priority >= 1 && priority <= 5)
                {
                    break;
                }

                Console.WriteLine("Invalid entry. Enter a number between 1 and 5.");
            }

            Patients patient = new Patients(lastName, name, dob, priority);
            eRQueue.EnqueuePatients(patient);

            Console.WriteLine("Patient Added.");
        }

        static void ProcessPatient()
        {
            Patients patient = eRQueue.DequeuePatient();

            if (patient != null)
            {
                Console.WriteLine($"Processing patient: {patient}");
            }
            else
            {
                Console.WriteLine("No patients in the queue.");
            }
        }
    }
}