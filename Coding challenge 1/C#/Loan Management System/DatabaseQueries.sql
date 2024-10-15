Create database LoanManagementSystemDB;

CREATE TABLE Customer (
  customerId INT PRIMARY KEY IDENTITY(1,1),
  name VARCHAR(100),
  email VARCHAR(100),
  phone VARCHAR(15),
  address TEXT,
  creditScore INT
);

CREATE TABLE Loan (
    loanId INT PRIMARY KEY IDENTITY(1,1),
    customerId INT,
    principalAmount FLOAT,
    interestRate FLOAT,
    loanTerm INT,
    loanType VARCHAR(50),
    loanStatus VARCHAR(20),
    FOREIGN KEY (customerId) REFERENCES Customer(customerId)
);

CREATE TABLE HomeLoan (
  loanId INT PRIMARY KEY,
  propertyAddress VARCHAR(255),
  propertyValue INT,
  FOREIGN KEY (loanId) REFERENCES Loan(loanId)
);

CREATE TABLE CarLoan (
  loanId INT PRIMARY KEY,
  carModel VARCHAR(100),
  carValue INT,
  FOREIGN KEY (loanId) REFERENCES Loan(loanId)
);



