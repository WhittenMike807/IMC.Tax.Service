using System;
using System.Collections.Generic;
using System.Text;

namespace IMC.Entities
{
    public class ReturnOrderTax
    {
        public ReturnOrderTax(float subTotal, float taxRate, float tax, float shipping)
        {
            SubTotal = subTotal;
            TaxRate = taxRate;
            Tax = tax;
            Shipping = shipping;
        }

        public float SubTotal { get; set; }
        public float TaxRate { get; set; }
        public float Tax { get; set; }
        public float Shipping { get; set; }
        public float Total { get { return SubTotal + Tax; } }
    }
}
