using EagleBank.Models.common;
using EagleBank.Models.DTO.Request;
using EagleBank.Models.DTO.Response;
using EagleBank.Models.Request;

namespace EagleBank.Orchestration.Tests.helpers
{
    public static class TestHelpers
    {
        public const string mockUsername = "usr-123456";
        public const string mockAccountNumber = "01234567";
        public static UserDetailsResponseDTO UserDetailsResponseDTO()
        {
            return new UserDetailsResponseDTO
            {
                Email = "usr@gmail.com",
                Address = new Address { County = "Yorkshire", Line1 = "1 Street", Postcode = "HX1 2HH", Town = "Halifax" },
                CreatedTimeStamp = DateTime.UtcNow,
                Id = Guid.NewGuid().ToString(),
                UpdatedTimeStamp = DateTime.UtcNow,
                Name = mockUsername,
                PhoneNumber = "0123456789"

            };
        }

        public static UserDetailsRequest UserDetailsRequest()
        {
            return new UserDetailsRequest
            {
                Name = mockUsername,
                Email = "usr@gmail.com",
                PhoneNumber = "0123456789",
                Address = new Address { County = "Yorkshire", Line1 = "1 Street", Postcode = "HX1 2HH", Town = "Halifax" }
            };
        }
        public static BankAccountRequestDTO BankAccountRequestDTO()
        {
            return new BankAccountRequestDTO
            {
                AccountType = "personal",
                Name = "Personal Bank Account"
            };
        }
        public static BankAccountResponseDTO BankAccountResponseDTO()
        {
            return new BankAccountResponseDTO
            {
                AccountNumber = mockAccountNumber,
                SortCode = "12-34-56",
                Name = "Personal Bank Account",
                AccountType = "personal",
                Balance = 1000.00m,
                Currency = "GBP",
                CreatedTimestamp = DateTime.UtcNow,
                UpdatedTimestamp = DateTime.UtcNow
            };
        }
        public static CreateBankAccountRequest CreateBankAccountRequest()
        {
            return new CreateBankAccountRequest
            {
                AccountType = "personal",
                Name = "Personal Bank Account"
            };
        }
        public static PatchBankAccountRequest PatchBankAccountRequest()
        {
            return new PatchBankAccountRequest
            {
                AccountType = "personal",
                Name = "Personal Bank Account"
            };
        }
        public static BankAccountsResponseDTO BankAccountsResponseDTO()
        {
            return new BankAccountsResponseDTO
            {
                Accounts = new List<BankAccountResponseDTO> { BankAccountResponseDTO() }
            };
        }
        public static TransactionResponseDTO TransactionResponseDTO()
        {
            return new TransactionResponseDTO
            {
                Id = Guid.NewGuid().ToString(),
                AccountNumber = mockAccountNumber,
                Amount = 100.00m,
                Currency = "GBP",
                CreatedTimestamp = DateTime.UtcNow,
                Reference = "Test Transaction",
                Type = "deposit",
                UserId = mockUsername
            };
        }
        public static CreateTransactionRequest CreateTransactionRequest()
        {
            return new CreateTransactionRequest
            {
                Amount = 100.00m,
                Currency = "GBP",
                Reference = "Test Transaction",
                Type = "deposit",
            };
        }
        public static TransactionsResponseDTO TransactionsResponseDTO()
        {
            return new TransactionsResponseDTO
            {
                Transactions = new List<TransactionResponseDTO> { TransactionResponseDTO() }
            };
        }
    }
}
