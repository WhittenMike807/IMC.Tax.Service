using System.Collections.Generic;
using Newtonsoft.Json;

namespace IMC.Tax.Service
{
    public partial class TaxesResponse
    {
        [JsonProperty("tax")]
        public Taxes Tax { get; set; }
    }

    public partial class Taxes
    {
        [JsonProperty("order_total_amount")]
        public float OrderTotalAmount { get; set; }

        [JsonProperty("shipping")]
        public float Shipping { get; set; }

        [JsonProperty("taxable_amount")]
        public float TaxableAmount { get; set; }

        [JsonProperty("amount_to_collect")]
        public float AmountToCollect { get; set; }

        [JsonProperty("rate")]
        public float Rate { get; set; }

        [JsonProperty("has_nexus")]
        public bool HasNexus { get; set; }

        [JsonProperty("freight_taxable")]
        public bool FreightTaxable { get; set; }

        [JsonProperty("tax_source")]
        public string TaxSource { get; set; }

        [JsonProperty("jurisdictions")]
        public Jurisdictions Jurisdictions { get; set; }

        [JsonProperty("breakdown")]
        public Breakdown Breakdown { get; set; }
    }

    public partial class Breakdown
    {
        [JsonProperty("taxable_amount")]
        public float TaxableAmount { get; set; }

        [JsonProperty("tax_collectable")]
        public float TaxCollectable { get; set; }

        [JsonProperty("combined_tax_rate")]
        public float CombinedTaxRate { get; set; }

        [JsonProperty("state_taxable_amount")]
        public float StateTaxableAmount { get; set; }

        [JsonProperty("state_tax_rate")]
        public float StateTaxRate { get; set; }

        [JsonProperty("state_tax_collectable")]
        public float StateTaxCollectable { get; set; }

        [JsonProperty("county_taxable_amount")]
        public float CountyTaxableAmount { get; set; }

        [JsonProperty("county_tax_rate")]
        public float CountyTaxRate { get; set; }

        [JsonProperty("county_tax_collectable")]
        public float CountyTaxCollectable { get; set; }

        [JsonProperty("city_taxable_amount")]
        public float CityTaxableAmount { get; set; }

        [JsonProperty("city_tax_rate")]
        public float CityTaxRate { get; set; }

        [JsonProperty("city_tax_collectable")]
        public float CityTaxCollectable { get; set; }

        [JsonProperty("special_district_taxable_amount")]
        public float SpecialDistrictTaxableAmount { get; set; }

        [JsonProperty("special_tax_rate")]
        public float SpecialTaxRate { get; set; }

        [JsonProperty("special_district_tax_collectable")]
        public float SpecialDistrictTaxCollectable { get; set; }

        [JsonProperty("line_items")]
        public List<TaxLineItem> LineItems { get; set; }
    }

    public partial class TaxLineItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("taxable_amount")]
        public float TaxableAmount { get; set; }

        [JsonProperty("tax_collectable")]
        public float TaxCollectable { get; set; }

        [JsonProperty("combined_tax_rate")]
        public float CombinedTaxRate { get; set; }

        [JsonProperty("state_taxable_amount")]
        public float StateTaxableAmount { get; set; }

        [JsonProperty("state_sales_tax_rate")]
        public float StateSalesTaxRate { get; set; }

        [JsonProperty("state_amount")]
        public float StateAmount { get; set; }

        [JsonProperty("county_taxable_amount")]
        public float CountyTaxableAmount { get; set; }

        [JsonProperty("county_tax_rate")]
        public float CountyTaxRate { get; set; }

        [JsonProperty("county_amount")]
        public float CountyAmount { get; set; }

        [JsonProperty("city_taxable_amount")]
        public float CityTaxableAmount { get; set; }

        [JsonProperty("city_tax_rate")]
        public float CityTaxRate { get; set; }

        [JsonProperty("city_amount")]
        public float CityAmount { get; set; }

        [JsonProperty("special_district_taxable_amount")]
        public float SpecialDistrictTaxableAmount { get; set; }

        [JsonProperty("special_tax_rate")]
        public float SpecialTaxRate { get; set; }

        [JsonProperty("special_district_amount")]
        public float SpecialDistrictAmount { get; set; }
    }

    public partial class Jurisdictions
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("county")]
        public string County { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }
    }
}
