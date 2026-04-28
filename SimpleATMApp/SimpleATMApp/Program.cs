using System;
using System.Collections.Generic;

namespace SimpleATMApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Simple ATM Console Application";

            List<string> transactionHistory = new List<string>();

            Console.WriteLine("=======================================");
            Console.WriteLine("          SIMPLE ATM APPLICATION       ");
            Console.WriteLine("=======================================");
            Console.WriteLine();

            decimal balance = GetValidAmount("Enter your starting balance: R ");

            bool continueUsingATM = true;

            while (continueUsingATM)
            {
                Console.Clear();

                Console.WriteLine("=======================================");
                Console.WriteLine("          SIMPLE ATM APPLICATION       ");
                Console.WriteLine("=======================================");
                Console.WriteLine();
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. Deposit Money");
                Console.WriteLine("3. Withdraw Money");
                Console.WriteLine("4. View Transaction History");
                Console.WriteLine("5. Exit");
                Console.WriteLine();

                int choice = GetValidMenuChoice("Choose an option between 1 and 5: ");

                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        DisplayBalance(balance);
                        break;

                    case 2:
                        balance = DepositMoney(balance, transactionHistory);
                        break;

                    case 3:
                        balance = WithdrawMoney(balance, transactionHistory);
                        break;

                    case 4:
                        DisplayTransactionHistory(transactionHistory);
                        break;

                    case 5:
                        continueUsingATM = false;
                        Console.WriteLine("Thank you for using the ATM.");
                        break;
                }

                if (continueUsingATM)
                {
                    Console.WriteLine();
                    Console.WriteLine("Press any key to return to the main menu...");
                    Console.ReadKey();
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void DisplayBalance(decimal balance)
        {
            Console.WriteLine("=======================================");
            Console.WriteLine("             BALANCE ENQUIRY           ");
            Console.WriteLine("=======================================");
            Console.WriteLine($"Current Balance : R {balance:F2}");
            Console.WriteLine($"Checked On      : {DateTime.Now}");
            Console.WriteLine("=======================================");
        }

        static decimal DepositMoney(decimal balance, List<string> transactionHistory)
        {
            decimal depositAmount = GetValidAmount("Enter deposit amount: R ");

            decimal openingBalance = balance;
            balance += depositAmount;

            string transaction = $"Deposit | Amount: R {depositAmount:F2} | Opening Balance: R {openingBalance:F2} | Updated Balance: R {balance:F2} | Time: {DateTime.Now}";
            transactionHistory.Add(transaction);

            Console.WriteLine();
            Console.WriteLine("=======================================");
            Console.WriteLine("             DEPOSIT RECEIPT           ");
            Console.WriteLine("=======================================");
            Console.WriteLine($"Opening Balance : R {openingBalance:F2}");
            Console.WriteLine($"Deposit Amount  : R {depositAmount:F2}");
            Console.WriteLine($"Updated Balance : R {balance:F2}");
            Console.WriteLine($"Transaction Time: {DateTime.Now}");
            Console.WriteLine("=======================================");

            return balance;
        }

        static decimal WithdrawMoney(decimal balance, List<string> transactionHistory)
        {
            decimal withdrawalAmount;

            while (true)
            {
                withdrawalAmount = GetValidAmount("Enter withdrawal amount: R ");

                if (withdrawalAmount > balance)
                {
                    Console.WriteLine("Insufficient funds. Please enter a smaller amount.");
                }
                else
                {
                    break;
                }
            }

            decimal openingBalance = balance;
            balance -= withdrawalAmount;

            string transaction = $"Withdrawal | Amount: R {withdrawalAmount:F2} | Opening Balance: R {openingBalance:F2} | Updated Balance: R {balance:F2} | Time: {DateTime.Now}";
            transactionHistory.Add(transaction);

            Console.WriteLine();
            Console.WriteLine("=======================================");
            Console.WriteLine("           WITHDRAWAL RECEIPT          ");
            Console.WriteLine("=======================================");
            Console.WriteLine($"Opening Balance : R {openingBalance:F2}");
            Console.WriteLine($"Amount Withdrawn: R {withdrawalAmount:F2}");
            Console.WriteLine($"Updated Balance : R {balance:F2}");
            Console.WriteLine($"Transaction Time: {DateTime.Now}");
            Console.WriteLine("=======================================");

            return balance;
        }

        static void DisplayTransactionHistory(List<string> transactionHistory)
        {
            Console.WriteLine("=======================================");
            Console.WriteLine("          TRANSACTION HISTORY          ");
            Console.WriteLine("=======================================");

            if (transactionHistory.Count == 0)
            {
                Console.WriteLine("No transactions have been made yet.");
            }
            else
            {
                for (int i = 0; i < transactionHistory.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {transactionHistory[i]}");
                }
            }

            Console.WriteLine("=======================================");
        }

        static decimal GetValidAmount(string message)
        {
            decimal amount;

            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (decimal.TryParse(input, out amount))
                {
                    if (amount > 0)
                    {
                        return amount;
                    }
                    else
                    {
                        Console.WriteLine("Amount must be greater than zero.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a numeric amount.");
                }
            }
        }

        static int GetValidMenuChoice(string message)
        {
            int choice;

            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (int.TryParse(input, out choice))
                {
                    if (choice >= 1 && choice <= 5)
                    {
                        return choice;
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please choose a number between 1 and 5.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a whole number.");
                }
            }
        }
    }
}