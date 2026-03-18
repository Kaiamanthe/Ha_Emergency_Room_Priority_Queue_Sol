using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ha_Emergency_Room_Priority_Queue
{
    internal class ERQueue
    {
        private List<Patients> patients = new List<Patients>();

        public void EnqueuePatients(Patients patient)
        {
            patients.Add(patient);
            patients = patients
                .OrderByDescending(p => p.Priority)
                .ThenBy(p => p.DOB)
                .ToList();
        }

        public Patients DequeuePatient()
        {
            if (patients.Count == 0)
            {
                return null;
            }

            Patients patient = patients[0];
            patients.RemoveAt(0);
            return patient;
        }

        public void ListPatients()
        {
            if (patients.Count == 0)
            {
                Console.WriteLine("No patients in queue.");
                return;
            }

            Console.WriteLine("Patients in Queue:");
            Console.WriteLine();

            foreach (Patients patient in patients)
            {
                Console.WriteLine(patient);
            }
        }

        public void LoadPreRecords(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Error: Missing File");
                return;
            }

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines.Skip(1))
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    string[] parts = line.Split(',');

                    if (parts.Length < 4)
                    {
                        continue;
                    }

                    string lastName = parts[0].Trim();
                    string firstName = parts[1].Trim();

                    if (!DateTime.TryParse(parts[2].Trim(), out DateTime dob))
                    {
                        continue;
                    }

                    if (!int.TryParse(parts[3].Trim(), out int priority))
                    {
                        continue;
                    }

                    Patients patient = new Patients(lastName, firstName, dob, priority);
                    EnqueuePatients(patient);
                }
            }
            catch
            {
                Console.WriteLine("Error reading file.");
            }
        }
    }
}