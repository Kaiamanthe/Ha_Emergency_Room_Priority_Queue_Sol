using System;

namespace Ha_Emergency_Room_Priority_Queue
{
    internal class Patients
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime DOB { get; set; }
        public int Priority { get; set; }

        public Patients(string lastName, string firstName, DateTime dob, int priority)
        {
            LastName = lastName;
            FirstName = firstName;
            DOB = dob;
            Priority = priority;
        }

        public override string ToString()
        {
            return $"{Priority} - {FirstName} {LastName} (DOB: {DOB.ToShortDateString()})";
        }
    }
}