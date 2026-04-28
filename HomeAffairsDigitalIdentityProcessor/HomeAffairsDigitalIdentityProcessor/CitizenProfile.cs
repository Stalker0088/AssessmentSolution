using System;
using System.Linq;

namespace HomeAffairsDigitalIdentityProcessor
{
    public class CitizenProfile
    {
        public string FullName { get; set; }
        public string IDNumber { get; set; }
        public int Age { get; set; }
        public string CitizenshipStatus { get; set; }

        public CitizenProfile(string fullName, string idNumber, string citizenshipStatus)
        {
            FullName = fullName;
            IDNumber = idNumber;
            CitizenshipStatus = citizenshipStatus;
            Age = CalculateAge();
        }

        private int CalculateAge()
        {
            if (string.IsNullOrWhiteSpace(IDNumber))
            {
                return 0;
            }

            if (IDNumber.Length < 6)
            {
                return 0;
            }

            string birthDatePart = IDNumber.Substring(0, 6);

            if (!birthDatePart.All(char.IsDigit))
            {
                return 0;
            }

            int year = int.Parse(birthDatePart.Substring(0, 2));
            int month = int.Parse(birthDatePart.Substring(2, 2));
            int day = int.Parse(birthDatePart.Substring(4, 2));

            int currentTwoDigitYear = DateTime.Now.Year % 100;
            int fullYear = year <= currentTwoDigitYear ? 2000 + year : 1900 + year;

            try
            {
                DateTime birthDate = new DateTime(fullYear, month, day);

                int age = DateTime.Now.Year - birthDate.Year;

                if (DateTime.Now.Date < birthDate.AddYears(age))
                {
                    age--;
                }

                return age;
            }
            catch
            {
                return 0;
            }
        }

        public string ValidateID()
        {
            if (string.IsNullOrWhiteSpace(IDNumber))
            {
                return "Invalid ID: ID number cannot be empty.";
            }

            if (IDNumber.Length != 13)
            {
                return "Invalid ID: ID number must contain exactly 13 digits.";
            }

            if (!IDNumber.All(char.IsDigit))
            {
                return "Invalid ID: ID number must contain numeric digits only.";
            }

            if (Age <= 0)
            {
                return "Invalid ID: Birth date or age could not be calculated correctly.";
            }

            return "Valid ID: The ID number is valid.";
        }

        public string GenerateProfileSummary()
        {
            string validationResult = ValidateID();

            return
                "==========================================" + Environment.NewLine +
                "       DIGITAL CITIZEN PROFILE SUMMARY    " + Environment.NewLine +
                "==========================================" + Environment.NewLine +
                $"Full Name          : {FullName}" + Environment.NewLine +
                $"ID Number          : {IDNumber}" + Environment.NewLine +
                $"Calculated Age     : {Age}" + Environment.NewLine +
                $"Citizenship Status : {CitizenshipStatus}" + Environment.NewLine +
                $"Validation Result  : {validationResult}" + Environment.NewLine +
                $"Processed On       : {DateTime.Now}" + Environment.NewLine +
                "==========================================";
        }
    }
}