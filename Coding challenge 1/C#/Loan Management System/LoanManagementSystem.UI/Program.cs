using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanManagementSystem.BusinessLayer.Repository;
using LoanManagementSystem.BusinessLayer.Services;
using LoanManagementSystem.Exception;

namespace LoanManagementSystem.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ILoanRepository loanRepo = new LoanRepositoryImpl();
            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Apply for Loan");
                Console.WriteLine("2. Get All Loans");
                Console.WriteLine("3. Get Loan by ID");
                Console.WriteLine("4. Loan Repayment");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Get customer details and loan details from the user
                        Console.WriteLine("Enter Customer ID:");
                        int customerId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Principal Amount:");
                        double principalAmount = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Enter Interest Rate:");
                        double interestRate = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Enter Loan Term (in months):");
                        int loanTerm = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Loan Type (CarLoan/HomeLoan):");
                        string loanType = Console.ReadLine();

                        Loan loan;
                        if (loanType.Equals("HomeLoan", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Enter Property Address:");
                            string propertyAddress = Console.ReadLine();
                            Console.WriteLine("Enter Property Value:");
                            int propertyValue = Convert.ToInt32(Console.ReadLine());
                            loan = new HomeLoan(customerId, principalAmount, interestRate, loanTerm, "HomeLoan");
                        }
                        else
                        {
                            Console.WriteLine("Enter Car Model:");
                            string carModel = Console.ReadLine();
                            Console.WriteLine("Enter Car Value:");
                            int carValue = Convert.ToInt32(Console.ReadLine());
                            loan = new CarLoan( customerId , principalAmount, interestRate, loanTerm, "CarLoan");
                        }
                        loanRepo.ApplyLoan(loan);
                        break;

                    case "2":
                        var loans = loanRepo.GetAllLoans();
                        foreach (var l in loans)
                        {
                            l.DisplayLoanInfo();
                        }
                        break;

                    case "3":
                        Console.WriteLine("Enter Loan ID:");
                        int loanId = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            Loan fetchedLoan = loanRepo.GetLoanById(loanId);
                            fetchedLoan.DisplayLoanInfo();
                        }
                        catch (InvalidLoanException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "4":
                        Console.WriteLine("Enter Loan ID for repayment:");
                        int repaymentLoanId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Amount to repay:");
                        double amount = Convert.ToDouble(Console.ReadLine());
                        loanRepo.LoanRepayment(repaymentLoanId, amount);
                        break;

                    case "5":
                        Console.WriteLine("Exiting the system...");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }

}
