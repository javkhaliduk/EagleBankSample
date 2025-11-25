
using System.Diagnostics.CodeAnalysis;

namespace EagleBank.Repository.Entities
{
    [ExcludeFromCodeCoverage]
    public class BankAccountsEntity
    {
        public List<BankAccountEntity>? Accounts { get; set; }

        internal object Select(Func<object, object> value)
        {
            throw new NotImplementedException();
        }
    }
}
