using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text.RegularExpressions;

namespace HcaDay3_Assignment
{
    internal class Task1_PatientRegistration
    {
        public class Patient
        {
            public string PatientID { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public string Gender { get; set; }
            public string PhoneNumber { get; set; }
            public string City { get; set; }
        }

        public static Patient RegisterPatient()
        {
            Patient patient = new Patient();

            //Patient ID generation logic
            patient.PatientID = "PAT-" + DateTime.UtcNow.Year + "-001";

            //Patient Name input and validation Logic
            while (true)
            {
                Console.Write("Enter Patient Name: ");
                string name = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(name))
                {
                    patient.Name = name;
                    break;
                }
                Console.WriteLine("Err: Name cannot be empty");
            }

            //Patient Age input and validation Logic
            while (true)
            {
                try
                {
                    Console.Write("Enter Patient age: ");
                    int age = Convert.ToInt32(Console.ReadLine());

                    if (age > 0 && age < 120)
                    {
                        patient.Age = age;
                        break;
                    }
                    Console.WriteLine("Error: Age must be between 1 and 119");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Invalid input. Please enter a valid age.");
                }
            }

            //Gender input logic
            Console.Write("Enter Gender (Male/Female/Other): ");
            patient.Gender = Console.ReadLine();

            //Phone number input and validation logic
            while (true)
            {
                try
                {
                    Console.Write("Enter Phone Number: ");
                    string phone = Console.ReadLine();

                    if (!Regex.IsMatch(phone, @"^\d{10}$"))
                    {
                        throw new FormatException();
                    }

                    patient.PhoneNumber = phone;
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Phone number must contain exactly 10 digits.");
                }
            }

            //City Input logic and validation logic
            while (true)
            {
                try
                {
                    Console.Write("Enter City: ");
                    string city = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(city))
                    {
                        throw new FormatException();
                    }

                    patient.City = city;
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Err: City cannot be empty");
                }
            }
            return patient;
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("       HOSPITAL PATIENT REGISTRATION SYSTEM");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();

            Patient patient = RegisterPatient();
            Console.WriteLine("\n[Registration Complete]\n");

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("            PATIENT REGISTRATION SLIP");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"Date: {DateTime.Now.ToShortDateString()}");
            Console.WriteLine();

            Console.WriteLine($"Patient ID: {patient.PatientID}");
            Console.WriteLine($"Name:       {patient.Name}");
            Console.WriteLine($"Age:        {patient.Age} years");
            Console.WriteLine($"Gender:     {patient.Gender}");
            Console.WriteLine($"Contact:    {patient.PhoneNumber}");
            Console.WriteLine($"Location:   {patient.City}");
            Console.WriteLine();

            Console.WriteLine("Instructions:");
            Console.WriteLine("Please proceed to the waiting area.");
            Console.WriteLine("--------------------------------------------------");

            //Console.WriteLine("\nMemory Management Demonstration");
            List<Patient> patients = new List<Patient>();

            for (int i = 1; i <= 1000; i++)
            {
                patients.Add(new Patient
                {
                    PatientID = $"PAT-{i}",
                    Name = $"Patient {i}",
                    Age = 25,
                    Gender = "Male",
                    PhoneNumber = "9876543210",
                    City = "Mumbai"
                });
            }

            Console.WriteLine($"Created {patients.Count} patient records.");

            // Memory before cleanup
            long before = GC.GetTotalMemory(true);

            // Remove reference to patient object
            patients.Clear();
            patients = null;

            // Force Garbage Collection (for demonstration only)
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            // Memory after cleanup
            long after = GC.GetTotalMemory(true);

            Console.WriteLine($"Memory Before GC: {before / 1024} KB");
            Console.WriteLine($"Memory After GC: {after / 1024} KB");
        }
    }
}
