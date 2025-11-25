using EagleBank.Models.common;
using EagleBank.Models.Response;
using System.Security.Claims;

namespace EagleBank.Tests.helpers
{
    public static class TestHelpers
    {
        public const string mockUsername = "usr-123456";
        public static ClaimsPrincipal GetClaimsPrincipal()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, mockUsername),
            };
            var identity = new ClaimsIdentity(claims, "Bearer");
            return new ClaimsPrincipal(identity);
        }

        public static UserDetailsResponse UserDetailsResponse()
        {
            return new UserDetailsResponse
            {
                Id = mockUsername,
                Email = "test@gmail.com",
                Name = "Test User",
                PhoneNumber = "1234567890",
                CreatedTimeStamp = DateTime.UtcNow,
                UpdatedTimeStamp = DateTime.UtcNow,
                Address = new Address { County = "Test County", Line1 = "Line 1", Line2 = "Line 2", Town = "Halifax", Postcode = "HX1 2HH" }

            };
        }
        public static TransactionsResponse TransactionsResponse()
        {
            return new TransactionsResponse
            {
                Transactions = new List<TransactionResponse>
               {
                   TransactionResponse()
               }
            };
        }
        public static TransactionResponse TransactionResponse()
        {
            return new TransactionResponse
            {
                Amount = 100,
                Currency = "GBP",
                CreatedTimestamp = DateTime.UtcNow,
                Id = "usr-123456",
                Reference = "Test Transaction",
                Type = "Deposit",
                UserId = mockUsername
            };
        }

        public static BankAccountResponse BankAccountResponse()
        {
            return new BankAccountResponse
            {
                AccountNumber = "acc-123456",
                AccountType = "Checking",
                Balance = 1000.00M,
                Currency = "USD",
                CreatedTimestamp = DateTime.UtcNow,
                UpdatedTimestamp = DateTime.UtcNow,
                Name = "John Doe",
                SortCode = "00-00-00"
            };
        }
        public static PatchBankAccountResponse PatchBankAccountResponse()
        {
            return new PatchBankAccountResponse
            {
                AccountNumber = "acc-123456",
                AccountType = "Checking",
                Balance = 1000.00M,
                Currency = "GBP",
                CreatedTimestamp = DateTime.UtcNow,
                UpdatedTimestamp = DateTime.UtcNow,
                Name = "John Doe",
                SortCode = "00-00-00",

            };
        }
        public static CreateBankAccountResponse CreateBankAccountResponse()
        {
            return new CreateBankAccountResponse
            {
                AccountNumber = "acc-123456",
                AccountType = "Checking",
                Balance = 1000.00M,
                Currency = "GBP",
                CreatedTimestamp = DateTime.UtcNow,
                UpdatedTimestamp = DateTime.UtcNow,
                Name = "John Doe",
                SortCode = "00-00-00",

            };
        }
        public static BankAccountsResponse BankAccountsResponse()
        {
            return new BankAccountsResponse
            {
                Accounts = new BankAccountResponse[]
               {
                   new BankAccountResponse
                   {
                       AccountNumber="acc-123456",
                       AccountType="Checking",
                       Balance=1000.00M,
                       Currency="GBP",
                       CreatedTimestamp=DateTime.UtcNow,
                          UpdatedTimestamp=DateTime.UtcNow,
                          Name="John Doe",
                            SortCode="00-00-00"


                   },
                   new BankAccountResponse
                   {
                       AccountNumber="acc-654321",
                       AccountType="Savings",
                       Balance=5000.00M,
                       Currency="GBP",
                               CreatedTimestamp=DateTime.UtcNow,
                          UpdatedTimestamp=DateTime.UtcNow,
                          Name="Jane Smith",
                            SortCode="00-00-00"
                   }
               }

            };
        }
    }
}
