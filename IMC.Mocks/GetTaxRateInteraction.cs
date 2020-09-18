using System;
using System.Threading.Tasks;
using EasyConsole;
using IMC.Entities;

namespace IMC.Tax.Service
{
    public class GetTaxRateInteraction
    {
        private readonly ITaxJarClient _taxJarClient;

        public GetTaxRateInteraction(ITaxJarClient taxJarClient)
        {
            _taxJarClient = taxJarClient;
        }

        public async Task TaxRatePrompt()
        {
            Console.Clear();
            Console.WriteLine("IMC Tax Calculation Service: Find Tax Rate By Zip Code");
            Console.WriteLine("============================================================================");
            Console.WriteLine();

            string askZipCodeInput = Input.ReadString("Please enter your Zip Code:");

            TaxResponse<ReturnTaxRate> rate = await _taxJarClient.GetRateAsync(askZipCodeInput);

            if (rate.Data is null)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(rate.Message);
                Console.WriteLine();
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey(true);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"The Tax Rate for {askZipCodeInput} ({rate.Data.City}, {rate.Data.State}) is: {rate.Data.TaxRate}.");
                Console.WriteLine();
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey(true);
            }
        }
    }
}
