using IMC.Entities;
using System.Threading.Tasks;

namespace IMC.Tax.Service
{
    public interface ITaxJarClient
    {
        Task<TaxResponse<ReturnTaxRate>> GetRateAsync(string zipCode);
        Task<TaxResponse<ReturnOrderTax>> CalculateSalesTaxAsync(TaxForOrder taxForOrder);
    }
}
