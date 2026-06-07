using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Xml.Linq;
using static HcaDay3_Assignment.Task1_PatientRegistration;

namespace HcaDay3_Assignment
{
    internal class Task5_HospitalReport
    {
        public class PatientRecord
        {
            public string Name { get; }
            public string Department { get; }
            public decimal BillAmount { get; }
            public string Status { get; }
            public PatientRecord(string name, string department, decimal billAmount, string status)
            {
                Name = name;
                Department = department;
                BillAmount = billAmount;
                Status = status;
            }            
        }
        public static void Main(string[] args)
        {
            var patients = new List<PatientRecord>
        {
            new("Arun", "General", 500m, "Discharged"),
            new("Nitya", "Dental", 1200m, "Admitted"),
            new("Kriti", "General", 400m, "Discharged"),
            new("Rohan", "Ortho", 2500m, "Admitted"),
            new("Karan", "Dental", 800m, "Discharged")
        };

            var totalRevenue = patients.Sum(patient => patient.BillAmount);
            var departmentCounts = patients
                .GroupBy(patient => patient.Department, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(group => group.Key, group => group.Count(), StringComparer.OrdinalIgnoreCase);


            Console.WriteLine(new string('-', 50));
            Console.WriteLine("       DAILY HOSPITAL ACTIVITY REPORT");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"Date: {DateTime.Today:d}\n");

            Console.WriteLine("Patient List:");
            for (var i = 0; i < patients.Count; i++)
            {
                var patient = patients[i];
                Console.WriteLine($"{i + 1}. {patient.Name} - {patient.Department} - ${patient.BillAmount}");
            }

            Console.WriteLine("\n--------------------------------------------------");
            Console.WriteLine("SUMMARY STATISTICS");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"Total Patients Visited:  {patients.Count}");
            Console.WriteLine($"Total Revenue:           ${totalRevenue}\n");

            Console.WriteLine("Traffic by Department:");
            foreach (var (key, value) in departmentCounts)
            {
                Console.WriteLine($"- {key}: {value}");
            }

            Console.WriteLine("\nEnd of Report.");
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("\nMEMORY MANAGEMENT DEMONSTRATION");
            Console.WriteLine("--------------------------------------------------");

            // Measure memory before allocation
            long before = GC.GetTotalMemory(true);

            Console.WriteLine($"Memory Before Allocation: {before / 1024} KB");

            // Create many patient records
            List<PatientRecord> demoPatients = new List<PatientRecord>();

            for (int i = 0; i < 100000; i++)
            {
                demoPatients.Add(
                    new PatientRecord(
                        $"Patient-{i}",
                        "General",
                        500m,
                        "Admitted"
                    )
                );
            }

            // Measure memory after allocation
            long after = GC.GetTotalMemory(true);

            Console.WriteLine($"Memory After Allocation: {after / 1024} KB");

            // Calculate allocated memory
            Console.WriteLine($"Allocated Approx: {(after - before) / 1024} KB");

            // Remove references
            demoPatients = null;

            // Force Garbage Collection (demo purposes only)
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            // Measure memory after cleanup
            long cleaned = GC.GetTotalMemory(true);

            Console.WriteLine($"Memory After Cleanup: {cleaned / 1024} KB");

            // Compare with starting point
            Console.WriteLine($"Difference From Start: {(cleaned - before) / 1024} KB");
        }
    }
}