using System;
using System.Collections.Generic;
using System.Text;

namespace IMC.Tax.Service
{
    public static class MockOrderService
    {
        public static TaxForOrder GetSalesOrder(FakeOrder orderNumber) => orderNumber switch
        {
            FakeOrder.One => new TaxForOrder(0, 235.84, "US", "30327", "GA", "Atlanta", "1290 W Garmon Rd NW"),
            FakeOrder.Two => new TaxForOrder(10, 512.00, "US", "30327", "GA", "Atlanta", "1290 W Garmon Rd NW"),
            FakeOrder.Three => new TaxForOrder(20.5, 5572.11, "US", "30327", "GA", "Atlanta", "1290 W Garmon Rd NW"),
            _ => throw new ArgumentException(message: "invalid enum value", paramName: nameof(orderNumber))
        };
    }

    public enum FakeOrder : int
    {
        One     = 8251821,
        Two     = 8245436,
        Three   = 9545528
    }

}


