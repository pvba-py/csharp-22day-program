using System;
using System.Collections.Generic;
using System.Linq;

class Task5_HospitalReport
{
    static void Main(string[] args)
    {
        var patients = new List<PatientRecord>
        {
            new("John Doe", "General", 500m, "Discharged"),
            new("Jane Smith", "Dental", 1200m, "Admitted"),
            new("Bob Brown", "General", 400m, "Discharged"),
            new("Alice W.", "Ortho", 2500m, "Admitted"),
            new("Sam K.", "Dental", 800m, "Discharged")
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
    }

    public class PatientRecord
    {
        public PatientRecord(string name, string department, decimal billAmount, string status)
        {
            Name = name;
            Department = department;
            BillAmount = billAmount;
            Status = status;
        }

        public string Name { get; }
        public string Department { get; }
        public decimal BillAmount { get; }
        public string Status { get; }
    }
}
