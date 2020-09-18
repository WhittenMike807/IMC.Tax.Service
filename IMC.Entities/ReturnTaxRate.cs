using Newtonsoft.Json;

namespace IMC.Entities
{
    public class ReturnTaxRate
    {
        public ReturnTaxRate(string city, string state, string zipCode, float taxRate)
        {
            City = city;
            State = state;
            ZipCode = zipCode;
            TaxRate = taxRate;
        }

        [JsonProperty("city")]
        public string City { get; set; }
        
        [JsonProperty("state")]
        public string State { get; set; }
        
        [JsonProperty("zipcode")]
        public string ZipCode { get; set; }
        
        [JsonProperty("taxRate")]
        public float TaxRate { get; set; }
    }
}
