using IMC.Entities;
using IMC.Exceptions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IMC.Tax.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class TaxJarClient: ITaxJarClient
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="configuration"></param>
        public TaxJarClient(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            ExternalTaxService externalTaxService = _configuration.GetSection("ExternalTaxServices:TaxJar").Get<ExternalTaxService>();
            httpClient.BaseAddress = new Uri(externalTaxService.BaseUrl);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", externalTaxService.BearerToken);
            _httpClient = httpClient;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userZipCodeInput"></param>
        /// <returns></returns>
        public async Task<TaxResponse<ReturnTaxRate>> GetRateAsync(string userZipCodeInput)
        {
            try
            {
                if (userZipCodeInput.Length != 5)
                {
                    throw new StringTooLongException($"The Zip Code needs to be 5 digits.");
                }

                int zipCode = Int32.Parse(userZipCodeInput);

                HttpResponseMessage response = await _httpClient.GetAsync($"rates/{userZipCodeInput}").ConfigureAwait(false);
                
                response.EnsureSuccessStatusCode();

                Rates rates = JsonConvert.DeserializeObject<Rates>(response.Content.ReadAsStringAsync().Result);
                ReturnTaxRate returnTaxRate = new ReturnTaxRate(rates.Rate.City, rates.Rate.State, rates.Rate.Zip, rates.Rate.CombinedRate);

                return new TaxResponse<ReturnTaxRate>(returnTaxRate, response.StatusCode, response.ReasonPhrase);
            }
            catch (HttpRequestException HRException)
            {
                return new TaxResponse<ReturnTaxRate>(null, HttpStatusCode.BadRequest, "HttpRequestException : " + HRException.Message);
            }
            catch (StringTooLongException e)
            {
                return new TaxResponse<ReturnTaxRate>(null, HttpStatusCode.BadRequest, e.Message);
            }
            catch (FormatException)
            {
                return new TaxResponse<ReturnTaxRate>(null, HttpStatusCode.BadRequest, "Zip Codes can only be numbers.");
            }
            catch (Exception ex)
            {
                return new TaxResponse<ReturnTaxRate>(null, HttpStatusCode.BadRequest, ex.Message);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taxForOrder"></param>
        /// <returns></returns>
        public async Task<TaxResponse<ReturnOrderTax>> CalculateSalesTaxAsync(TaxForOrder taxForOrder)
        {
            try
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(taxForOrder), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("taxes", content).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                TaxesResponse taxesResponse = JsonConvert.DeserializeObject<TaxesResponse>(response.Content.ReadAsStringAsync().Result);
                ReturnOrderTax returnOrderTax = new ReturnOrderTax((float)taxForOrder.Amount, taxesResponse.Tax.Rate, taxesResponse.Tax.AmountToCollect, taxesResponse.Tax.Shipping);
                return new TaxResponse<ReturnOrderTax>(returnOrderTax, response.StatusCode, response.ReasonPhrase);
            }
            catch (HttpRequestException HRException)
            {
                return new TaxResponse<ReturnOrderTax>(null, HttpStatusCode.BadRequest, "HttpRequestException : " + HRException.Message);
            }
        }
    }
}
