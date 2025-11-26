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
                Id = $"usr-{new Random().Next(1, 10000).ToString()}",
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
                AccountType = Enumerations.AccountType.Current,
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
                AccountType = Enumerations.AccountType.Current,
                Balance = 1000.00m,
                Currency = Enumerations.Currency.GBP,
                CreatedTimestamp = DateTime.UtcNow,
                UpdatedTimestamp = DateTime.UtcNow
            };
        }
        public static CreateBankAccountRequest CreateBankAccountRequest()
        {
            return new CreateBankAccountRequest
            {
                AccountType = Constants.ACCOUNT_TYPE_CURRENT,
                Name = "Personal Bank Account"
            };
        }
        public static PatchBankAccountRequest PatchBankAccountRequest()
        {
            return new PatchBankAccountRequest
            {
                AccountType = Constants.ACCOUNT_TYPE_CURRENT,
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
                Id = $"tan-{new Random().Next(1, 10000).ToString()}",
                AccountNumber = mockAccountNumber,
                Amount = 100.00m,
                Currency = Enumerations.Currency.GBP,
                CreatedTimestamp = DateTime.UtcNow,
                Reference = "Test Transaction",
                Type = Enumerations.TransactionType.Deposit,
                UserId = mockUsername
            };
        }
        public static CreateTransactionRequest CreateTransactionRequest()
        {
            return new CreateTransactionRequest
            {
                Amount = 100.00m,
                Currency = Constants.CURRENCY_GBP,
                Reference = "Test Transaction",
                Type = Constants.TRANSACTION_TYPE_DEPOSIT,
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
