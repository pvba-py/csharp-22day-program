using System;

namespace HcaDay3_Assignment
{
    internal class Task4_BillingModule
    {
        public class Bill
        {
            public decimal TotalAmount { get; set; }
        }

        const decimal ConsultationFee = 500;
        const decimal BloodTestFee = 200;
        const decimal XRayFee = 1000;

        public static void Main(string[] args)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("       HOSPITAL BILLING CALCULATOR");
            Console.WriteLine("--------------------------------------------------");

            string name;
            while (true)
            {
                Console.Write("Patient Name: ");
                name = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(name))
                    break;

                Console.WriteLine("Error: Name cannot be empty.");
            }

            int age;
            while (true)
            {
                try
                {
                    Console.Write("Patient Age: ");
                    age = Convert.ToInt32(Console.ReadLine());

                    if (age > 0 && age < 120)
                        break;

                    Console.WriteLine("Error: Age must be between 1 and 119.");
                }
                catch
                {
                    Console.WriteLine("Error: Enter valid numeric age.");
                }
            }

            string category = (age > 60) ? "Senior Citizen" :
                              (age < 10) ? "Child" : "Normal";

            Bill bill = new Bill();

            while (true)
            {
                Console.WriteLine("\nAdd Services:");
                Console.WriteLine("1. Consultation (500)");
                Console.WriteLine("2. Blood Test (200)");
                Console.WriteLine("3. X-Ray (1000)");
                Console.WriteLine("4. Done");

                int choice;

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Error: Enter a number only.");
                    continue;
                }

                if (choice == 1)
                {
                    bill.TotalAmount += ConsultationFee;
                    Console.WriteLine("[Added Consultation]");
                }
                else if (choice == 2)
                {
                    bill.TotalAmount += BloodTestFee;
                    Console.WriteLine("[Added Blood Test]");
                }
                else if (choice == 3)
                {
                    bill.TotalAmount += XRayFee;
                    Console.WriteLine("[Added X-Ray]");
                }
                else if (choice == 4)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice");
                }
            }

            decimal discount = 0;

            if (age > 60)
            {
                discount = bill.TotalAmount * 0.20m;
            }
            else if (age < 10)
            {
                discount = ConsultationFee * 0.50m;
            }

            decimal afterDiscount = bill.TotalAmount - discount;
            decimal tax = afterDiscount * 0.05m;
            decimal net = afterDiscount + tax;

            Console.WriteLine("\n[Calculating Bill...]");

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("            FINAL BILL INVOICE");
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine($"Patient: {name} ({category})");
            Console.WriteLine();
            Console.WriteLine($"Base Amount:     {bill.TotalAmount}");
            Console.WriteLine($"Discount:       -{discount}");
            Console.WriteLine($"Tax (5%):       +{tax}");
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"TOTAL PAYABLE:   {net}");
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("\nMEMORY MANAGEMENT CHECK");
            Console.WriteLine("--------------------------------------------------");

            long before = GC.GetTotalMemory(true);
            Console.WriteLine($"Memory Before Cleanup: {before / 1024} KB");

            bill = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            long after = GC.GetTotalMemory(true);

            Console.WriteLine($"Memory After Cleanup: {after / 1024} KB");
            Console.WriteLine($"Difference: {(after - before) / 1024} KB");
        }
    }
}