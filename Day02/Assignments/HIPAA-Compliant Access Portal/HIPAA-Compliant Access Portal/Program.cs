using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HIPAA_Compliant_Access_Portal
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
            while (true) {                 
                Console.WriteLine("Welcome to the CareBridge HIPAA-Compliant Access Portal");
                Console.WriteLine("Please select an option:");
                Console.WriteLine("1. Clinical Team");
                Console.WriteLine("2. Billing Team");
                Console.WriteLine("3. Analytics Team");
                Console.WriteLine("4. Exit");

                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        DisplayView("view_Clinical");
                        break;
                    case 2:
                        DisplayView("view_Billing");
                        break;
                    case 3:
                        DisplayView("view_Analytics");
                        break;
                    case 4:
                        Console.WriteLine("Exiting the portal. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
        static void DisplayView(string viewName)
        {
            using (SqlConnection conn =
                   new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM {viewName}";

                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(
                            $"{reader.GetName(i)}: {reader[i]}\t");
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}
