using System;
using System.Collections.Generic;
using System.Linq;

namespace EmfuleniMunicipalityApp
{
    public class UtilitiesManager
    {
        public double CalculateUrgencyScore(ServiceRequest request)
        {
            double score = (request.PriorityLevel * 10) + (request.SeverityLevel * 5);

            if (request.EstimatedResolutionHours <= 2)
            {
                score += 20;
            }
            else if (request.EstimatedResolutionHours <= 6)
            {
                score += 10;
            }
            else
            {
                score += 5;
            }

            return score;
        }

        public void GenerateServiceReport(ServiceRequest request)
        {
            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("              SERVICE REQUEST REPORT          ");
            Console.WriteLine("==============================================");
            Console.WriteLine("Resident Details:");
            Console.WriteLine($"Name                 : {request.Resident.Name}");
            Console.WriteLine($"Address              : {request.Resident.Address}");
            Console.WriteLine($"Account Number       : {request.Resident.AccountNumber}");
            Console.WriteLine($"Monthly Utility Usage: {request.Resident.MonthlyUtilityUsage} units");
            Console.WriteLine();
            Console.WriteLine("Service Request Details:");
            Console.WriteLine($"Request Type         : {request.RequestType}");
            Console.WriteLine($"Priority Level       : {request.PriorityLevel}");
            Console.WriteLine($"Severity Level       : {request.SeverityLevel}");
            Console.WriteLine($"Estimated Hours      : {request.EstimatedResolutionHours}");
            Console.WriteLine($"Urgency Score        : {request.UrgencyScore:F2}");
            Console.WriteLine($"Status               : Processed");
            Console.WriteLine("==============================================");
        }

        public void DisplaySummary(List<ServiceRequest> resolvedRequests)
        {
            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("             RESOLVED REQUEST SUMMARY         ");
            Console.WriteLine("==============================================");

            if (resolvedRequests.Count == 0)
            {
                Console.WriteLine("No requests were processed.");
                return;
            }

            for (int i = 0; i < resolvedRequests.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {resolvedRequests[i]}");
            }

            ServiceRequest highestUrgency = resolvedRequests
                .OrderByDescending(r => r.UrgencyScore)
                .First();

            Console.WriteLine();
            Console.WriteLine("Highest Urgency Request:");
            Console.WriteLine(highestUrgency);
            Console.WriteLine("==============================================");
        }
    }
}