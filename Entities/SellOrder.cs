using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class SellOrder
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
    }
}


/*
 Sell Order object entity with the following properties:
 	• Guid SellOrderID
	• string StockSymbol [Mandatory]
	• string StockName [Mandatory]
	• DateTime DateAndTimeOfOrder
	• uint Quantity [Range(1, 100000)]
    • double Price [Range(1, 10000)] 
 */