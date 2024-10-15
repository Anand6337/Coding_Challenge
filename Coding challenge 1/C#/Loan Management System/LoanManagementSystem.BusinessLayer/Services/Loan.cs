using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.BusinessLayer.Services
{
    public class Loan
    {
        public int LoanId;
        public int CustomerId;
        public Customer Customer;
        public double PrincipalAmount;
        public double InterestRate;
        public int LoanTerm; // in months
        public string LoanType;
        public string LoanStatus; // Pending, Approved

        // Default Constructor
        public Loan()
        {
        }
        public Loan(int customerID, double principalAmount, double interestRate, int loanTerm, string loanType)
        {

            CustomerId = customerID;
            PrincipalAmount = principalAmount;
            InterestRate = interestRate;
            LoanTerm = loanTerm;
            LoanType = loanType;
        }

        // Parameterized Constructor
        public Loan(int loanId, Customer customer, double principalAmount, double interestRate, int loanTerm, string loanType, string loanStatus)
        {
            LoanId = loanId;
            Customer = customer;
            PrincipalAmount = principalAmount;
            InterestRate = interestRate;
            LoanTerm = loanTerm;
            LoanType = loanType;
            LoanStatus = loanStatus;
        }

        // Method to display loan details
        public void DisplayLoanInfo()
        {
            Console.WriteLine($"Loan ID: {LoanId}");
            Console.WriteLine($"Customer: {Customer.Name}");
            Console.WriteLine($"Principal Amount: {PrincipalAmount}");
            Console.WriteLine($"Interest Rate: {InterestRate}");
            Console.WriteLine($"Loan Term: {LoanTerm} months");
            Console.WriteLine($"Loan Type: {LoanType}");
            Console.WriteLine($"Loan Status: {LoanStatus}");
        }
    }

}