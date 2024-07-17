using ServiceContracts.DTO;

namespace StocksAppProject.Models
{
    public class Orders
    {
            public List<BuyOrderResponse> BuyOrders { get; set; } = new List<BuyOrderResponse>();
            public List<SellOrderResponse> SellOrders { get; set; } = new List<SellOrderResponse>();
    }
}

/*
 *
    This model class will be used to send list of buy orders and sell orders - from controller to the "Trade/Orders" view.
    It contains the following properties:
    • List<BuyOrderResponse> BuyOrders
	• List<SellOrderResponse> SellOrders
 */
