using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcaDay3_Assignment
{
    internal class Task2_AppointmentScheduling 
    { 
        public class Appointment
        {
            public string PatientName { get; set; }
            public string Department { get; set; }
            public string Doctor { get; set; }
            public string TimeSlot { get; set; }

            public Appointment(string patientName, string department, string doctor, string timeSlot)
            {
                PatientName = patientName;
                Department = department;
                Doctor = doctor;
                TimeSlot = timeSlot;
            }
        }

        public static void Main(string[] args)
        {
            string[] departments = { "General Medicine", "Dental", "Orthopedics" };

            string[] generalDoctors = { "Dr. A. Kumar", "Dr. B. Singh" };
            string[] dentalDoctors = { "Dr. C. Roy", "Dr. D. Gupta" };
            string[] orthoDoctors = { "Dr. E. Mehta", "Dr. F. Sharma" };

            string[] timeSlots = { "10:00 AM", "11:00 AM", "12:00 PM" };

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("       APPOINTMENT BOOKING SYSTEM");
            Console.WriteLine("--------------------------------------------------");

            Console.Write("Enter Patient Name: ");
            string patientName = Console.ReadLine();

            int deptChoice = 0;
            while (true)
            {
                Console.WriteLine("\nSelect Department:");
                for (int i = 0; i < departments.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {departments[i]}");
                }

                Console.Write("Enter Choice: ");
                if (int.TryParse(Console.ReadLine(), out deptChoice) &&
                    deptChoice >= 1 && deptChoice <= departments.Length)
                {
                    break;
                }

                Console.WriteLine("Invalid input. Try again.");
            }

            string[] selectedDoctors = deptChoice switch
            {
                1 => generalDoctors,
                2 => dentalDoctors,
                3 => orthoDoctors,
                _ => generalDoctors
            };

            int docChoice = 0;
            while (true)
            {
                Console.WriteLine("\nSelect Doctor:");
                for (int i = 0; i < selectedDoctors.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {selectedDoctors[i]}");
                }

                Console.Write("Enter Choice: ");
                if (int.TryParse(Console.ReadLine(), out docChoice) &&
                    docChoice >= 1 && docChoice <= selectedDoctors.Length)
                {
                    break;
                }

                Console.WriteLine("Invalid input. Try again.");
            }

            int timeChoice = 0;
            while (true)
            {
                Console.WriteLine("\nSelect Time Slot:");
                for (int i = 0; i < timeSlots.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {timeSlots[i]}");
                }

                Console.Write("Enter Choice: ");
                if (int.TryParse(Console.ReadLine(), out timeChoice) &&
                    timeChoice >= 1 && timeChoice <= timeSlots.Length)
                {
                    break;
                }

                Console.WriteLine("Invalid input. Try again.");
            }

            Appointment appointment = new Appointment(
                patientName,
                departments[deptChoice - 1],
                selectedDoctors[docChoice - 1],
                timeSlots[timeChoice - 1]
            );

            Console.WriteLine("\n[Booking Confirmed]\n");

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("            APPOINTMENT TICKET");
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine($"Patient:    {appointment.PatientName}");
            Console.WriteLine($"Department: {appointment.Department}");
            Console.WriteLine($"Doctor:     {appointment.Doctor}");
            Console.WriteLine($"Time:       {appointment.TimeSlot}");
            Console.WriteLine("Status:     Confirmed");
            Console.WriteLine();
            Console.WriteLine("Please arrive 15 mins before your slot.");
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("\nMEMORY MANAGEMENT CHECK");
            Console.WriteLine("--------------------------------------------------");

            // Before
            long before = GC.GetTotalMemory(true);
            Console.WriteLine($"Memory Before Cleanup: {before / 1024} KB");

            // Remove reference
            appointment = null;

            // Force GC
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            // After
            long after = GC.GetTotalMemory(true);

            Console.WriteLine($"Memory After Cleanup: {after / 1024} KB");
            Console.WriteLine($"Difference: {(after - before) / 1024} KB");

            Console.WriteLine("\nNote: Small objects may not show visible memory reduction.");
        }
    }
}
