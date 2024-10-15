using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanManagementSystem.BusinessLayer.Services;
using LoanManagementSystem.Util;
using LoanManagementSystem.Exception;

namespace LoanManagementSystem.BusinessLayer.Repository
{
    public class LoanRepositoryImpl : ILoanRepository
    {
        private SqlConnection connection;

        public LoanRepositoryImpl()
        {
            connection = DBConnectionUtil.GetDBConn();
        }

        public void ApplyLoan(Loan loan)
        {
            Console.WriteLine("Do you want to apply for the loan? (Yes/No)");
            string confirmation = Console.ReadLine();

            if (confirmation.Equals("Yes", StringComparison.OrdinalIgnoreCase))
            {
                // Store loan in the database
                string query = "INSERT INTO Loan (CustomerId, PrincipalAmount, InterestRate, LoanTerm, LoanType, LoanStatus) VALUES (@CustomerId, @PrincipalAmount, @InterestRate, @LoanTerm, @LoanType, @LoanStatus)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", loan.Customer.CustomerId);
                    command.Parameters.AddWithValue("@PrincipalAmount", loan.PrincipalAmount);
                    command.Parameters.AddWithValue("@InterestRate", loan.InterestRate);
                    command.Parameters.AddWithValue("@LoanTerm", loan.LoanTerm);
                    command.Parameters.AddWithValue("@LoanType", loan.LoanType);
                    command.Parameters.AddWithValue("@LoanStatus", "Open");
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("Loan application submitted successfully.");
            }
            else
            {
                Console.WriteLine("Loan application cancelled.");
            }
        }

        public double CalculateInterest(int loanId)
        {
            Loan loan = GetLoanById(loanId);
            return CalculateInterest(loan.PrincipalAmount, loan.InterestRate, loan.LoanTerm);
        }

        public double CalculateInterest(double principalAmount, double interestRate, int loanTerm)
        {
            return (principalAmount * interestRate * loanTerm) / 12;
        }

        public void LoanStatus(int loanId)
        {
            Loan loan = GetLoanById(loanId);
            if (loan.Customer.CreditScore > 650)
            {
                loan.LoanStatus = "Approved";
                UpdateLoanStatus(loanId, loan.LoanStatus);
                Console.WriteLine("Loan approved.");
            }
            else
            {
                loan.LoanStatus = "Rejected";
                UpdateLoanStatus(loanId, loan.LoanStatus);
                Console.WriteLine("Loan rejected due to low credit score.");
            }
        }

        private void UpdateLoanStatus(int loanId, string status)
        {
            string query = "UPDATE Loan SET LoanStatus = @LoanStatus WHERE LoanId = @LoanId";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LoanStatus", status);
                command.Parameters.AddWithValue("@LoanId", loanId);
                command.ExecuteNonQuery();
            }
        }

        public double CalculateEMI(int loanId)
        {
            Loan loan = GetLoanById(loanId);
            return CalculateEMI(loan.PrincipalAmount, loan.InterestRate, loan.LoanTerm);
        }

        public double CalculateEMI(double principalAmount, double interestRate, int loanTerm)
        {
            double monthlyRate = interestRate / 12 / 100;
            return (principalAmount * monthlyRate * Math.Pow(1 + monthlyRate, loanTerm)) / (Math.Pow(1 + monthlyRate, loanTerm) - 1);
        }

        public void LoanRepayment(int loanId, double amount)
        {
            Loan loan = GetLoanById(loanId);
            double emi = CalculateEMI(loanId);

            if (amount < emi)
            {
                Console.WriteLine("Payment amount is less than the EMI. Payment rejected.");
            }
            else
            {
                Console.WriteLine($"Payment of {amount} accepted. Your remaining loan balance will be updated.");
                // Here you could implement logic to reduce the principal amount.
            }
        }

        public List<Loan> GetAllLoans()
        {
            List<Loan> loans = new List<Loan>();
            string query = "SELECT * FROM Loan";
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    // Assuming you have a method to get customer by ID
                    Customer customer = GetCustomerById(reader.GetInt32(1));
                    Loan loan = new Loan(
                        reader.GetInt32(0),
                        customer,
                        reader.GetDouble(2),
                        reader.GetDouble(3),
                        reader.GetInt32(4),
                        reader.GetString(5),
                        reader.GetString(6)
                    );
                    loans.Add(loan);
                }
            }
            return loans;
        }

        public Loan GetLoanById(int loanId)
        {
            string query = "SELECT * FROM Loan WHERE LoanId = @LoanId";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LoanId", loanId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Customer customer = GetCustomerById(reader.GetInt32(1));
                        return new Loan(
                            reader.GetInt32(0),
                            customer,
                            reader.GetDouble(2),
                            reader.GetDouble(3),
                            reader.GetInt32(4),
                            reader.GetString(5),
                            reader.GetString(6)
                        );
                    }
                }
            }
            throw new InvalidLoanException("Loan not found.");
        }

        private Customer GetCustomerById(int customerId)
        {
            // Implementation to retrieve a Customer by ID from the database
            return new Customer(); // Placeholder, implement actual retrieval logic
        }
    }

}
