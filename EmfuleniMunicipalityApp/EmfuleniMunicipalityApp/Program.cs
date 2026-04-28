using System;
using System.Collections.Generic;
using System.Linq;

namespace EmfuleniMunicipalityApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Emfuleni Municipality Service Management";

            List<Resident> residents = new List<Resident>();
            List<ServiceRequest> pendingRequests = new List<ServiceRequest>();
            List<ServiceRequest> resolvedRequests = new List<ServiceRequest>();

            UtilitiesManager manager = new UtilitiesManager();

            Console.WriteLine("==================================================");
            Console.WriteLine("     EMFULENI MUNICIPALITY SERVICE MANAGEMENT     ");
            Console.WriteLine("==================================================");

            int residentCount = GetValidInt("Enter number of residents: ", 1, 100);

            for (int i = 0; i < residentCount; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"Resident {i + 1}");

                Console.Write("Enter resident name: ");
                string name = Console.ReadLine();

                Console.Write("Enter address: ");
                string address = Console.ReadLine();

                Console.Write("Enter account number: ");
                string accountNumber = Console.ReadLine();

                double usage = GetValidDouble("Enter monthly utility usage: ", 0, 100000);

                residents.Add(new Resident(name, address, accountNumber, usage));
            }

            Console.WriteLine();
            int requestCount = GetValidInt("Enter number of service requests: ", 1, 100);

            for (int i = 0; i < requestCount; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"Service Request {i + 1}");

                DisplayResidents(residents);

                int residentChoice = GetValidInt("Select resident number: ", 1, residents.Count);
                Resident selectedResident = residents[residentChoice - 1];

                Console.Write("Enter request type, for example Water Leak, Electricity Fault, Billing Query: ");
                string requestType = Console.ReadLine();

                int priority = GetValidInt("Enter priority level between 1 and 5: ", 1, 5);
                int severity = GetValidInt("Enter severity level between 1 and 10: ", 1, 10);
                double estimatedHours = GetValidDouble("Enter estimated resolution hours: ", 0.1, 1000);

                ServiceRequest request = new ServiceRequest(
                    selectedResident,
                    requestType,
                    priority,
                    severity,
                    estimatedHours
                );

                request.UrgencyScore = manager.CalculateUrgencyScore(request);
                pendingRequests.Add(request);
            }

            while (pendingRequests.Any(r => !r.IsProcessed))
            {
                Console.WriteLine();
                Console.WriteLine("==================================================");
                Console.WriteLine("             PENDING SERVICE REQUESTS            ");
                Console.WriteLine("==================================================");

                List<ServiceRequest> activePending = pendingRequests
                    .Where(r => !r.IsProcessed)
                    .OrderByDescending(r => r.UrgencyScore)
                    .ToList();

                for (int i = 0; i < activePending.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {activePending[i]}");
                }

                int selectedRequestNumber = GetValidInt("Select request number to process: ", 1, activePending.Count);

                ServiceRequest selectedRequest = activePending[selectedRequestNumber - 1];
                selectedRequest.IsProcessed = true;
                resolvedRequests.Add(selectedRequest);

                manager.GenerateServiceReport(selectedRequest);
            }

            manager.DisplaySummary(resolvedRequests);

            Console.WriteLine();
            Console.WriteLine("All service requests have been processed.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void DisplayResidents(List<Resident> residents)
        {
            Console.WriteLine();
            Console.WriteLine("Available Residents:");

            for (int i = 0; i < residents.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {residents[i].Name} - {residents[i].AccountNumber}");
            }
        }

        static int GetValidInt(string message, int min, int max)
        {
            int value;

            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (int.TryParse(input, out value))
                {
                    if (value >= min && value <= max)
                    {
                        return value;
                    }
                    else
                    {
                        Console.WriteLine($"Please enter a number between {min} and {max}.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a whole number.");
                }
            }
        }

        static double GetValidDouble(string message, double min, double max)
        {
            double value;

            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (double.TryParse(input, out value))
                {
                    if (value >= min && value <= max)
                    {
                        return value;
                    }
                    else
                    {
                        Console.WriteLine($"Please enter a value between {min} and {max}.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a numeric value.");
                }
            }
        }
    }
}