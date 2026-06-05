using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CareBridgeConsole
{
    class Program
    {
        // Connection string to SQL Server

        static string connectionString =
            "Server=localhost;" +
            "Database=CareBridgeDB;" +
            "Trusted_Connection=True;" +
            "TrustServerCertificate=True;";

        static void Main(string[] args)
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("=== CareBridge Clinical Operations Console ===");
                Console.WriteLine("1. 30-Day Readmissions");
                Console.WriteLine("2. High-Risk Patients");
                Console.WriteLine("3. Provider Workload");
                Console.WriteLine("4. Revenue Analysis");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        ExecuteProcedure("sp_30DayReadmissions");
                        break;

                    case 2:
                        ExecuteProcedure("sp_HighRiskPatients");
                        break;

                    case 3:
                        ExecuteProcedure("sp_ProviderWorkload");
                        break;

                    case 4:
                        ExecuteProcedure("storedProcedure_RevenueLeakageReport");
                        break;

                    case 5:
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid Option");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        static void ExecuteProcedure(string procedureName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(procedureName, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write($"{reader.GetName(i)}: {reader[i]}\t");
                    }

                    Console.WriteLine();
                }

                reader.Close();
            }
        }
    }
}


