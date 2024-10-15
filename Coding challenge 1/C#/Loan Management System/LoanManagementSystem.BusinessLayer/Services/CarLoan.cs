using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.BusinessLayer.Services
{
    public class CarLoan : Loan
    {
        public string CarModel;
        public int CarValue;

        // Default Constructor
        public CarLoan() : base()
        {
        }
        public CarLoan(int customerId, double principalAmount, double interestRate, int loanTerm, string loanType)
      : base(customerId, principalAmount, interestRate, loanTerm, "CarLoan")
        {
            //CarModel = carModel;
            //CarValue = carValue;
        }

        // Parameterized Constructor
        public CarLoan(int loanId, Customer customer, double principalAmount, double interestRate, int loanTerm, string carModel, int carValue)
       : base(loanId, customer, principalAmount, interestRate, loanTerm, "CarLoan", "Open")
        {
            CarModel = carModel;
            CarValue = carValue;
        }

        // Method to display car loan details
        public void DisplayCarLoanInfo()
        {
            DisplayLoanInfo();
            Console.WriteLine($"Car Model: {CarModel}");
            Console.WriteLine($"Car Value: {CarValue}");
        }
    }

}