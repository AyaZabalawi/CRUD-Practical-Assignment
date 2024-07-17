using System.ComponentModel.DataAnnotations;
using Entities;

namespace ServiceContracts.DTO
{
    public class BuyOrderRequest : IValidatableObject
    {
        [Required(ErrorMessage = "Stock symbol is required")]
        public string StockSymbol { get; set; }

        [Required(ErrorMessage = "Stock name is required")]
        public string StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 100000, ErrorMessage = "Quantity should be between 1 and 100000")]
        public uint Quantity { get; set; }

        [Range(1, 100000, ErrorMessage = "Price should be between 1 and 10000")]
        public double Price { get; set; }

        public BuyOrder ToBuyOrder()
        {
            return new BuyOrder()
            {
                StockSymbol = StockSymbol,
                StockName = StockName,
                Price = Price,
                Quantity = Quantity,
                DateAndTimeOfOrder = DateAndTimeOfOrder
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();

            if(DateAndTimeOfOrder < Convert.ToDateTime("2000-01-01"))
            {
                validationResults.Add(new ValidationResult("Date cannot be older than 2000-01-01"));
            }

            return validationResults;
        }
    }
}

/*
    Buy Order request properties:
	• string StockSymbol [Mandatory]
	• string StockName [Mandatory]
	• DateTime DateAndTimeOfOrder [Should not be older than Jan 01, 2000]
	• uint Quantity [Value should be between 1 and 100000]
    • double Price [Value should be between 1 and 10000]
 */
