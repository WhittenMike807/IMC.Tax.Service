using EasyConsole;


namespace IMC.Tax.Service
{
    /// <summary>
    /// Test
    /// </summary>
    public class ConsoleApplication
    {

        private readonly ITaxJarClient _taxJarClient;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taxJarClient"></param>
        public ConsoleApplication(ITaxJarClient taxJarClient)
        {
            _taxJarClient = taxJarClient;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Run()
        {
            var menu = new Menu()
                .Add("Get Tax Rate by Zip Code", async () => await new GetTaxRateInteraction(_taxJarClient).TaxRatePrompt())
                .Add("Calculate Taxes for an Order", () => new CalculateTaxesForOrder(_taxJarClient).SelectCustomerPrompt());

            menu.Display();
        }
    }
}
