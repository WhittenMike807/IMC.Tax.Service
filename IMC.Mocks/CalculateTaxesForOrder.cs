using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using EasyConsole;
using IMC.Entities;

namespace IMC.Tax.Service
{
    public class CalculateTaxesForOrder
    {
        private readonly ITaxJarClient _taxJarClient;

        public CalculateTaxesForOrder(ITaxJarClient taxJarClient)
        {
            _taxJarClient = taxJarClient;
        }

        public void SelectCustomerPrompt()
        {
            Console.Clear();
            Console.WriteLine("IMC Tax Calculation Service: Select Customer");
            Console.WriteLine("============================================================================");
            Console.WriteLine();

            var menu = new Menu()
                .Add("John Smith", () => SelectCustomerOrderPrompt("John Smith"))
                .Add("April Robinson", () => SelectCustomerOrderPrompt("April Robinson"))
                .Add("Dan Holmes", () => SelectCustomerOrderPrompt("Dan Holmes"));

            menu.Display();
        }

        public void SelectCustomerOrderPrompt(string customerName)
        {
            Console.Clear();
            Console.WriteLine($"IMC Tax Calculation Service: Customer: {customerName} || Select Order Number");
            Console.WriteLine("============================================================================");
            Console.WriteLine();

            var menu = new Menu()
                .Add("Order #: 8251821", async () => await CalculateTaxesForOrderPrompt(customerName, FakeOrder.One))
                .Add("Order #: 8245436", async () => await CalculateTaxesForOrderPrompt(customerName, FakeOrder.Two))
                .Add("Order #: 9545528", async () => await CalculateTaxesForOrderPrompt(customerName, FakeOrder.Three));

            menu.Display();
        }


        public async Task CalculateTaxesForOrderPrompt(string customerName, FakeOrder customerOrderNumber)
        {
            Console.Clear();
            Console.WriteLine($"IMC Tax Calculation Service: Customer: {customerName}, Sales Order: {(int) customerOrderNumber}");
            Console.WriteLine("============================================================================");
            Console.WriteLine();

            TaxForOrder taxForOrder = MockOrderService.GetSalesOrder(customerOrderNumber);
            
            Console.WriteLine("Processing...");

            TaxResponse<ReturnOrderTax> taxResponse = await _taxJarClient.CalculateSalesTaxAsync(taxForOrder);
            ReturnOrderTax returnOrderTax = taxResponse.Data;

            ClearProcessingLine(customerName, customerOrderNumber);

            Console.WriteLine($"SUBTOTAL: {returnOrderTax.SubTotal.ToString("C", CultureInfo.CurrentCulture)}");
            Console.WriteLine($"TAX RATE: {returnOrderTax.TaxRate.ToString("P", CultureInfo.CurrentCulture)}");
            Console.WriteLine($"TAX:      {returnOrderTax.Tax.ToString("C", CultureInfo.CurrentCulture)}");
            Console.WriteLine($"SHIPPING: {returnOrderTax.Shipping.ToString("C", CultureInfo.CurrentCulture)}");
            Console.WriteLine("==================");
            Console.WriteLine($"TOTAL:    {returnOrderTax.Total.ToString("C", CultureInfo.CurrentCulture)}");
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(true);

        }

        public static void ClearProcessingLine(string customerName, FakeOrder customerOrderNumber)
        {
            Console.Clear();
            Console.WriteLine($"IMC Tax Calculation Service: Customer: {customerName}, Sales Order: {(int)customerOrderNumber}");
            Console.WriteLine("============================================================================");
            Console.WriteLine();
        }


    }
}
