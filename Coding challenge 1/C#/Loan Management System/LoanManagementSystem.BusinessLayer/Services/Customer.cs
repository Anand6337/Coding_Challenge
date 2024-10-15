using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LoanManagementSystem.BusinessLayer.Services
{

        public class Customer
        {

        public int CustomerId;
        public string Name;
        public string Email;
        public string Phone;
        public string Address;
        public int CreditScore;

        // Default Constructor
        public Customer()
            {
            }

            // Parameterized Constructor
            public Customer(int customerId, string name, string email, string phone, string address, int creditScore)
            {
                CustomerId = customerId;
                Name = name;
                Email = email;
                Phone = phone;
                Address = address;
                CreditScore = creditScore;
            }

            // Method to display customer details
            public void DisplayCustomerInfo()
            {
                Console.WriteLine($"Customer ID: {CustomerId}");
                Console.WriteLine($"Name: {Name}");
                Console.WriteLine($"Email: {Email}");
                Console.WriteLine($"Phone: {Phone}");
                Console.WriteLine($"Address: {Address}");
                Console.WriteLine($"Credit Score: {CreditScore}");
            }
        }
    
}
