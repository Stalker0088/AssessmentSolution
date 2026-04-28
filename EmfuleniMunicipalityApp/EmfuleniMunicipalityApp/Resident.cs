namespace EmfuleniMunicipalityApp
{
    public class Resident
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string AccountNumber { get; set; }
        public double MonthlyUtilityUsage { get; set; }

        public Resident(string name, string address, string accountNumber, double monthlyUtilityUsage)
        {
            Name = name;
            Address = address;
            AccountNumber = accountNumber;
            MonthlyUtilityUsage = monthlyUtilityUsage;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Address: {Address}, Account Number: {AccountNumber}, Monthly Usage: {MonthlyUtilityUsage} units";
        }
    }
}