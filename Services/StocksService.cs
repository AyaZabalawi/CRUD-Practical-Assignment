using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using System;
using Entities;

namespace Services
{
    public class StocksService : IStockService
    {
        private readonly List<BuyOrder> _buyOrders;
        private readonly List<SellOrder> _sellOrders;

        public StocksService()
        {
            _buyOrders = new List<BuyOrder>();
            _sellOrders = new List<SellOrder>();
        }

        // CreateBuyOrder: Inserts a new buy order into the database table called 'BuyOrders'
        public BuyOrderResponse CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest == null) throw new ArgumentNullException(nameof(buyOrderRequest));
            ValidationHelper.ModelValidation(buyOrderRequest);
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            buyOrder.BuyOrderID = Guid.NewGuid();
            _buyOrders.Add(buyOrder);

            return buyOrder.ToBuyOrderResponse();
        }

        // CreateSellOrder: Inserts a new sell order into the database table called 'SellOrders'
        public SellOrderResponse CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null) throw new ArgumentNullException(nameof(sellOrderRequest));
            ValidationHelper.ModelValidation(sellOrderRequest);
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            sellOrder.SellOrderID = Guid.NewGuid();
            _sellOrders.Add(sellOrder);

            return sellOrder.ToSellOrderResponse();
        }

        // GetBuyOrders: Returns the existing list of buy orders retrieved from database table called 'BuyOrders' in the order of latest->oldest
        public List<BuyOrderResponse> GetBuyOrders()
        {
            return _buyOrders.OrderByDescending(temp => temp.DateAndTimeOfOrder).Select(temp => temp.ToBuyOrderResponse()).ToList();
        }

        // GetSellOrders: Returns the existing list of sell orders retrieved from database table called 'SellOrders' in the order of latest->oldest
        public List<SellOrderResponse> GetSellOrders()
        {
            return _sellOrders.OrderByDescending(temp => temp.DateAndTimeOfOrder).Select(temp => temp.ToSellOrderResponse()).ToList();
        }
    }
}


