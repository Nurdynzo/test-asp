using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.ValueObjects;

namespace Plateaumed.EHR.Utility
{
    public static class CommonExtensions
    {

        public static Money ToMoney(this MoneyDto dto)
        {
            if (dto == null)
            {
                return new Money(0);
            }
            return new Money
            {
                Amount = dto.Amount,
                Currency = dto.Currency,
            };
        }
        
        public static MoneyDto ToMoneyDto(this Money money)
        {
            if (money == null)
            {
                return new MoneyDto
                {
                    Amount = 0,
                };
                
            }
            return new MoneyDto
            {
                Amount = money.Amount,
                Currency = money.Currency,
            };
        }

        public static Money ToMoney(this decimal amount, string currency)
        {
            return new Money(amount, currency);
        }
    }
}
