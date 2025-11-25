using EagleBank.Models.common;
using EagleBank.Models.DTO.Request;
using EagleBank.Models.DTO.Response;
using EagleBank.Models.Entities;
using EagleBank.Repository.Entities;

namespace EagleBank.Repository.Tests.helpers
{
    public static class TestHelpers
    {
        public const string mockUsername = "usr-123456";
        public const string mockAccountNumber = "01234567";
        public const string mockTransactionId = "tr1-123";
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
                AccountType = "Savings",
                Balance = 1000.00M,
                Currency = "GBP",
                CreatedTimestamp = DateTime.UtcNow,
                Name = mockUsername,
                SortCode = "00-00-00",
                UpdatedTimestamp = DateTime.UtcNow
            };
        }
        public static BankAccountsEntity BankAccountsEntity()
        {
            return new BankAccountsEntity
            {
                Accounts = new List<BankAccountEntity>
                {
                    new BankAccountEntity
                    {
                        AccountNumber = mockAccountNumber,
                        AccountType = "Savings",
                        Balance = 1000.00M,
                        Currency = "GBP",
                        CreatedTimestamp = DateTime.UtcNow,
                        Name = mockUsername,
                        SortCode = "00-00-00",
                        UpdatedTimestamp = DateTime.UtcNow
                    }
                }
            };
        }
        public static UserDetailsRequestDTO UserDetailsRequestDTO()
        {
            return new UserDetailsRequestDTO
            {
                Name = mockUsername,
                Email = "usr@gmail.com",
                PhoneNumber = "0123456789",
                Address = new Address { County = "Yorkshire", Line1 = "1 Street", Postcode = "HX1 2HH", Town = "Halifax" },
                Id = Guid.NewGuid().ToString(),
                CreatedTimeStamp = DateTime.UtcNow,
                UpdatedTimeStamp = DateTime.UtcNow,

            };
        }
        public static UserEntity UserEntity()
        {
            return new UserEntity
            {
                Name = mockUsername,
                Email = "usr@gmail.com",
                PhoneNumber = "0123456789",
                Address = new AddressEntity { County = "Yorkshire", Line1 = "1 Street", Postcode = "HX1 2HH", Town = "Halifax" },
                Id = Guid.NewGuid().ToString(),
                CreatedTimeStamp = DateTime.UtcNow,
                UpdatedTimeStamp = DateTime.UtcNow,
            };
        }
        public static CreateTransactionRequestDTO CreateTransactionRequestDTO()
        {
            return new CreateTransactionRequestDTO
            {
                Amount = 100.00m,
                Currency = "GBP",
                Reference = "Test Transaction",
                Type = "deposit",
                AccountNumber = mockAccountNumber
            };
        }
        public static TransactionsEntity TransactionsEntity()
        {
            return new TransactionsEntity
            {
                Transactions = new List<TransactionEntity>
                {
                    new TransactionEntity
                    {
                        Id = mockTransactionId,
                        AccountNumber = mockAccountNumber,
                        Amount = 100.00m,
                        Currency = "GBP",
                        Reference = "Test Transaction",
                        Type = "deposit",
                        CreatedTimestamp = DateTime.UtcNow,
                        UserId= mockUsername
                    }
                }
            };
        }
    }
}
