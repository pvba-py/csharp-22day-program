using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcaDay3_Assignment
{
    internal class Task3_VitalSigns
    {
        public static string CheckStatus(double temp, int oxygen, int pulse)
        {
            if (temp > 39.0 || oxygen < 90 || pulse < 50 || pulse > 120)
            {
                return "CRITICAL";
            }
            else if (temp > 37.5 || oxygen < 95 || pulse > 100)
            {
                return "OBSERVATION NEEDED";
            }
            else
            {
                return "NORMAL";
            }
        }
        public static void Main(string[] args)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("           VITAL SIGNS MONITOR");
            Console.WriteLine("--------------------------------------------------");

            string patientName;
            while (true)
            {
                Console.Write("Enter Patient Name:");
                patientName = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(patientName))
                {
                    break;
                }
                Console.WriteLine("Error: Patient name cannot be empty.");
            }

            double temperature;
            while (true)
            {
                try
                {
                    Console.Write("Enter Body Temperature (°C):");
                    temperature = Convert.ToDouble(Console.ReadLine());
                    if (temperature > 0 && temperature < 50)
                    {
                        break;
                    }
                    Console.WriteLine("Error: Please enter a valid temperature.");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Invalid input. Please enter a numeric value for temperature.");
                }
            }

            int oxygen;
            while (true)
            {
                try
                {
                    Console.Write("Enter Oxygen Level (%):");
                    oxygen = Convert.ToInt32(Console.ReadLine());

                    if (oxygen >= 0 && oxygen <= 100)
                    {
                        break;
                    }
                    Console.WriteLine("Error: Please enter a valid oxygen level between 0 and 100.");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Invalid input. Please enter a numeric value for oxygen level.");
                }
            }

            int pulse;
            while (true)
            {
                try
                {
                    Console.Write("Enter Pulse Rate (BPM):");
                    pulse = Convert.ToInt32(Console.ReadLine());
                    if (pulse > 0 && pulse < 300)
                    {
                        break;
                    }
                    Console.WriteLine("Error: Please enter a valid pulse rate.");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Invalid input. Please enter a numeric value for pulse rate.");
                }
            }

            Console.WriteLine("\n[Analyzing Data...]\n");

            string status = CheckStatus(temperature, oxygen, pulse);

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("       MEDICAL ASSESSMENT REPORT");
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine($"Patient: {patientName}");
            Console.WriteLine();

            Console.WriteLine("Vitals Recorded:");
            Console.WriteLine($"- Temp:   {temperature} C");
            Console.WriteLine($"- Oxygen: {oxygen} %");
            Console.WriteLine($"- Pulse:  {pulse} BPM");
            Console.WriteLine();

            Console.WriteLine($"Status Assessment: {status}");

            if (status == "CRITICAL / EMERGENCY")
            {
                Console.WriteLine("(Reason: One or more vital signs are in the critical range)");
                Console.WriteLine("Action: Immediate medical attention required.");
            }
            else if (status == "OBSERVATION NEEDED")
            {
                Console.WriteLine("(Reason: Elevated temperature, oxygen concern, or high pulse)");
                Console.WriteLine("Action: Nurse to monitor every hour.");
            }
            else
            {
                Console.WriteLine("(Reason: All vitals are within normal limits)");
                Console.WriteLine("Action: Continue routine monitoring.");
            }

            Console.WriteLine("--------------------------------------------------");

            // --------------------------------------------------
            // MEMORY MANAGEMENT DEMONSTRATION
            // --------------------------------------------------

            Console.WriteLine("\nMEMORY MANAGEMENT DEMONSTRATION");
            Console.WriteLine("--------------------------------------------------");

            // Measure current managed memory usage.
            long beforeCleanup = GC.GetTotalMemory(true);

            Console.WriteLine($"Memory Before Cleanup: {beforeCleanup / 1024} KB");

            // Remove references to objects that are no longer needed.
            // After this point, these objects become eligible for garbage collection.
            //only the strings (patientName, status) are reference types managed on the heap.
            patientName = null;
            status = null;

            // Force the Garbage Collector to run.
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            // Measure memory again after cleanup.
            long afterCleanup = GC.GetTotalMemory(true);

            Console.WriteLine($"Memory After Cleanup:  {afterCleanup / 1024} KB");

            //Difference.
            Console.WriteLine($"Difference: {(afterCleanup - beforeCleanup) / 1024} KB");

            Console.WriteLine("Unused objects are now eligible for memory reclamation.");
            Console.WriteLine("--------------------------------------------------");
        }
    }
}
