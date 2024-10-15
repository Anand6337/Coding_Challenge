using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.BusinessLayer.Services
{
    public interface ILoanRepository
    {
        void ApplyLoan(Loan loan);
        double CalculateInterest(int loanId);
        double CalculateInterest(double principalAmount, double interestRate, int loanTerm);
        void LoanStatus(int loanId);
        double CalculateEMI(int loanId);
        double CalculateEMI(double principalAmount, double interestRate, int loanTerm);
        void LoanRepayment(int loanId, double amount);
        List<Loan> GetAllLoans();
        Loan GetLoanById(int loanId);
    }

}
