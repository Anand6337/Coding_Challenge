using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.BusinessLayer.Services
{
    public class HomeLoan : Loan
    {
        public string PropertyAddress;
        public int PropertyValue;

        // Default Constructor
        public HomeLoan() : base()
        {
        }

        // Parameterized Constructor

        public HomeLoan(int customerID, double principalAmount, double interestRate, int loanTerm, string loanType)
            : base(customerID, principalAmount, interestRate, loanTerm, "HomeLoan")
        {
            //PropertyAddress = propertyAddress;
            //PropertyValue = propertyValue;
        }
        public HomeLoan(int loanId, Customer customer, double principalAmount, double interestRate, int loanTerm, string propertyAddress, int propertyValue)
            : base(loanId, customer, principalAmount, interestRate, loanTerm, "HomeLoan", "Open")
        {
            PropertyAddress = propertyAddress;
            PropertyValue = propertyValue;
        }

        // Method to display home loan details
        public void DisplayHomeLoanInfo()
        {
            DisplayLoanInfo();
            Console.WriteLine($"Property Address: {PropertyAddress}");
            Console.WriteLine($"Property Value: {PropertyValue}");
        }
    }


}