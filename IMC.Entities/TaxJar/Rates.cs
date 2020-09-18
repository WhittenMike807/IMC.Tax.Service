using Newtonsoft.Json;

namespace IMC.Tax.Service
{
    public partial class Rates
    {
        [JsonProperty("rate")]
        public Rate Rate { get; set; }
    }

    public partial class Rate
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("city_rate")]
        public float CityRate { get; set; }

        [JsonProperty("combined_district_rate")]
        public float CombinedDistrictRate { get; set; }

        [JsonProperty("combined_rate")]
        public float CombinedRate { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("country_rate")]
        public float CountryRate { get; set; }

        [JsonProperty("county")]
        public string County { get; set; }

        [JsonProperty("county_rate")]
        public float CountyRate { get; set; }

        [JsonProperty("freight_taxable")]
        public bool FreightTaxable { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("state_rate")]
        public float StateRate { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }
    }
}
