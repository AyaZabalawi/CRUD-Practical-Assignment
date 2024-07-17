using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class SellOrderResponse
    {
        public Guid SellOrderID { get; set; }

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
            if (obj is not SellOrderResponse) return false;

            SellOrderResponse sellOrderResponse = (SellOrderResponse)obj;

            return this.SellOrderID == sellOrderResponse.SellOrderID &&
                   this.StockSymbol == sellOrderResponse.StockSymbol &&
                   this.StockName == sellOrderResponse.StockName &&
                   this.DateAndTimeOfOrder == sellOrderResponse.DateAndTimeOfOrder &&
                   this.Quantity == sellOrderResponse.Quantity &&
                   this.Price == sellOrderResponse.Price &&
                   this.TradeAmount == sellOrderResponse.TradeAmount;
        }

        public override int GetHashCode()
        {
            return StockSymbol.GetHashCode();
        }

        public override string ToString()
        {
            return $"SellOrderID: {SellOrderID}, StockSymbol: {StockSymbol}, StockName: {StockName}, DateAndTimeOfOrder: {DateAndTimeOfOrder}, Quantity: {Quantity}, Price: {Price}, TradeAmount: {TradeAmount}";
        }

    }

    public static class SellOrderExtensions
    {
        public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
        {
            return new SellOrderResponse()
            {
                SellOrderID = sellOrder.SellOrderID,
                StockSymbol = sellOrder.StockSymbol,
                StockName = sellOrder.StockName,
                DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder,
                Quantity = sellOrder.Quantity,
                Price = sellOrder.Price,
                TradeAmount = sellOrder.Quantity * sellOrder.Price
            };
        }
    }

}

/*
	Sell Order reponse object with the following properties:
    • Guid SellOrderID
	• string StockSymbol
	• string StockName
	• DateTime DateAndTimeOfOrder
	• uint Quantity
	• double Price
	• double TradeAmount
 */
