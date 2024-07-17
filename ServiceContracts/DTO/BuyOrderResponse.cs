using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ServiceContracts.DTO
{
    public class BuyOrderResponse
    {
        public Guid BuyOrderID { get; set; }

        [Required(ErrorMessage = "Stock symbol is required")]
        public string StockSymbol { get; set; }

        [Required(ErrorMessage = "Stock name is required")]
        public string StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 100000, ErrorMessage = "Quantity should be between 1 and 100000")]
        public uint Quantity { get; set; }

        [Range(1, 100000, ErrorMessage = "Price should be between 1 and 10000")]
        public double Price { get; set; }

        public double TradeAmount { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not  BuyOrderResponse) return false;

            BuyOrderResponse buyOrderResponse = (BuyOrderResponse)obj;

            return this.BuyOrderID == buyOrderResponse.BuyOrderID &&
                   this.StockSymbol == buyOrderResponse.StockSymbol &&
                   this.StockName == buyOrderResponse.StockName &&
                   this.DateAndTimeOfOrder == buyOrderResponse.DateAndTimeOfOrder &&
                   this.Quantity == buyOrderResponse.Quantity &&
                   this.Price == buyOrderResponse.Price &&
                   this.TradeAmount == buyOrderResponse.TradeAmount;
        }

        public override int GetHashCode()
        {
            return StockSymbol.GetHashCode();
        }

        public override string ToString()
        {
            return $"BuyOrderID: {BuyOrderID}, StockSymbol: {StockSymbol}, StockName: {StockName}, DateAndTimeOfOrder: {DateAndTimeOfOrder}, Quantity: {Quantity}, Price: {Price}, TradeAmount: {TradeAmount}";
        }

    }

    public static class BuyOrderExtensions
    {
        public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder)
        {
            return new BuyOrderResponse()
            {
                BuyOrderID = buyOrder.BuyOrderID,
                StockSymbol = buyOrder.StockSymbol,
                StockName = buyOrder.StockName,
                DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
                Quantity = buyOrder.Quantity,
                Price = buyOrder.Price,
                TradeAmount = buyOrder.Quantity * buyOrder.Price
            };
        }
    }

}

/*
  Buy Order response with the following properties:
	• Guid BuyOrderID
	• string StockSymbol
	• string StockName
	• DateTime DateAndTimeOfOrder
	• uint Quantity
	• double Price
	• double TradeAmount
 */
