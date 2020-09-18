using Xunit;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Net.Http;
using IMC.Entities;
using System.Net;

namespace IMC.Tax.Service.Tests
{
    public class TaxJarClientTest
    {
        public IServiceProvider Services { get; private set; }

        private void Configure()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            //ExternalTaxService externalTaxService = configuration.GetSection("ExternalTaxServices:TaxJar").Get<ExternalTaxService>();

            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton(configuration);
            services.AddHttpClient<ITaxJarClient, TaxJarClient>();

            Services = services.BuildServiceProvider();
        }

        [Fact]
        public async Task Add_30115_ReturnsRateEntityForCantonGA()
        {
            // Arrange  
            Configure();
            ITaxJarClient taxJarClient = Services.GetService<ITaxJarClient>();
            string zipCode = "30115";

            // Act  
            TaxResponse<ReturnTaxRate> rate = await taxJarClient.GetRateAsync(zipCode);

            //Assert  
            Assert.IsType<TaxResponse<ReturnTaxRate>>(rate);
            Assert.Equal("CANTON", rate.Data.City);
        }
        [Fact]
        public async Task Add_EmptyString_ThrowsHttpRequestException()
        {
            // Arrange
            Configure();
            ITaxJarClient taxJarClient = Services.GetService<ITaxJarClient>();
            string zipCode = "";

            // Act
            TaxResponse<ReturnTaxRate> rate = await taxJarClient.GetRateAsync(zipCode);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, rate.ErrorCode);
        }

        [Fact]
        public async Task Add_TaxForOrderOne_ReturnsTaxes()
        {
            // Arrange
            Configure();
            TaxForOrder taxForOrder = MockOrderService.GetSalesOrder(FakeOrder.One);
            ITaxJarClient taxJarClient = Services.GetService<ITaxJarClient>();


            // Act
            TaxResponse<ReturnOrderTax> taxResponse = await taxJarClient.CalculateSalesTaxAsync(taxForOrder);
            ReturnOrderTax returnOrderTax = taxResponse.Data;

            // Assert
            Assert.IsType<TaxResponse<ReturnOrderTax>>(taxResponse);
            Assert.True(returnOrderTax.Shipping == 0f);
            Assert.True(returnOrderTax.SubTotal == 235.84f);
            Assert.True(returnOrderTax.Tax == 20.99f);
            Assert.True(returnOrderTax.TaxRate == 0.089f);
            Assert.True(returnOrderTax.Total == 256.83f);
        }

        [Fact]
        public async Task Add_TaxForOrderTwo_ReturnsTaxes()
        {
            // Arrange
            Configure();
            TaxForOrder taxForOrder = MockOrderService.GetSalesOrder(FakeOrder.Two);
            ITaxJarClient taxJarClient = Services.GetService<ITaxJarClient>();


            // Act
            TaxResponse<ReturnOrderTax> taxResponse = await taxJarClient.CalculateSalesTaxAsync(taxForOrder);
            ReturnOrderTax returnOrderTax = taxResponse.Data;

            // Assert
            Assert.IsType<TaxResponse<ReturnOrderTax>>(taxResponse);
            Assert.True(returnOrderTax.Shipping == 10f);
            Assert.True(returnOrderTax.SubTotal == 512f);
            Assert.True(returnOrderTax.Tax == 46.46f);
            Assert.True(returnOrderTax.TaxRate == 0.089f);
            Assert.True(returnOrderTax.Total == 558.46f);
        }

        [Fact]
        public async Task Add_TaxForOrderThree_ReturnsTaxes()
        {
            // Arrange
            Configure();
            TaxForOrder taxForOrder = MockOrderService.GetSalesOrder(FakeOrder.Three);
            ITaxJarClient taxJarClient = Services.GetService<ITaxJarClient>();


            // Act
            TaxResponse<ReturnOrderTax> taxResponse = await taxJarClient.CalculateSalesTaxAsync(taxForOrder);
            ReturnOrderTax returnOrderTax = taxResponse.Data;

            // Assert
            Assert.IsType<TaxResponse<ReturnOrderTax>>(taxResponse);
            Assert.True(returnOrderTax.Shipping == 20.5f);
            Assert.True(returnOrderTax.SubTotal == 5572.11f);
            Assert.True(returnOrderTax.Tax == 497.74f);
            Assert.True(returnOrderTax.TaxRate == 0.089f);
            Assert.True(returnOrderTax.Total == 6069.84961f);
        }
    }
}
