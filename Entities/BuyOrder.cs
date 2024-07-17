using System.ComponentModel.DataAnnotations;
using System.Formats.Asn1;

namespace Entities
{
    public class BuyOrder
    {
		public Guid BuyOrderID { get; set; }

		[Required(ErrorMessage = "Stock symbol is required")]
		public string StockSymbol { get; set; }

		[Required(ErrorMessage = "Stock name is required")]
		public string StockName { get; set; }

		public DateTime DateAndTimeOfOrder { get; set; }

		[Range(1,100000,ErrorMessage = "Quantity should be between 1 and 100000")]
		public uint Quantity { get; set; }

        [Range(1, 100000, ErrorMessage = "Price should be between 1 and 10000")]
        public double Price { get; set; }
    }
}


/*
 Buy Order object entity with the following properties:
	• Guid BuyOrderID
	• string StockSymbol [Mandatory]
	• string StockName [Mandatory]
	• DateTime DateAndTimeOfOrder
	• uint Quantity [Value should be between 1 and 100000]
	• double Price [Value should be between 1 and 10000]
 */