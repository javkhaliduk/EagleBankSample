using EagleBank.Models.common;

namespace EagleBank.Orchestration.DataConversion
{
    public static class EnumerationsConverter
    {
        public static string ToStringValue(this Enumerations.AccountType accountType)
        {
            return accountType switch
            {
                Enumerations.AccountType.Personal => Constants.ACCOUNT_TYPE_PERSONAL,
                Enumerations.AccountType.Business => Constants.ACCOUNT_TYPE_BUSINESS,
                _ => throw new ArgumentOutOfRangeException(nameof(accountType), accountType, null)
            };
        }
        public static string ToStringValue(this Enumerations.TransactionType transactionType)
        {
            return transactionType switch
            {
                Enumerations.TransactionType.Deposit => Constants.TRANSACTION_TYPE_DEPOSIT,
                Enumerations.TransactionType.Withdrawal => Constants.TRANSACTION_TYPE_WITHDRAWAL,
                _ => throw new ArgumentOutOfRangeException(nameof(transactionType), transactionType, null)
            };
        }
        public static string ToStringValue(this Enumerations.Currency currency)
        {
            return currency switch
            {
                Enumerations.Currency.GBP => Constants.CURRENCY_GBP,
                _ => throw new ArgumentOutOfRangeException(nameof(currency), currency, null)
            };
        }

        public static Enumerations.AccountType ToAccountTypeEnum(this string accountType)
        {
            return accountType switch
            {
                Constants.ACCOUNT_TYPE_PERSONAL => Enumerations.AccountType.Personal,
                Constants.ACCOUNT_TYPE_BUSINESS => Enumerations.AccountType.Business,
                _ => throw new ArgumentOutOfRangeException(nameof(accountType), accountType, null)
            };
        }
        public static Enumerations.TransactionType ToTransactionTypeEnum(this string transactionType)
        {
            return transactionType switch
            {
                Constants.TRANSACTION_TYPE_DEPOSIT => Enumerations.TransactionType.Deposit,
                Constants.TRANSACTION_TYPE_WITHDRAWAL => Enumerations.TransactionType.Withdrawal,
                _ => throw new ArgumentOutOfRangeException(nameof(transactionType), transactionType, null)
            };
        }
        public static Enumerations.Currency ToCurrencyEnum(this string currency)
        {
            return currency switch
            {
                Constants.CURRENCY_GBP => Enumerations.Currency.GBP,
                _ => throw new ArgumentOutOfRangeException(nameof(currency), currency, null)
            };
        }
    }
}
