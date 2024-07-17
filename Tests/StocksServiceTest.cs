using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System.Collections.Generic;
using System.ComponentModel;

namespace Tests
{
    public class StocksServiceTest
    {

        private readonly IStockService _stockService;
        public StocksServiceTest()
        {
            _stockService = new StocksService();
        }

        #region CreateBuyOrder
        // CreateBuyOrder function testing

        // When you supply BuyOrderRequest as null, it should throw ArgumentNullException.
        [Fact]
        public void CreateBuyOrder_NullBuyOrder_ToBeArgumentNullException()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = null;

            // Act
            Assert.Throws<ArgumentNullException>(() =>
                {
                    _stockService.CreateBuyOrder(buyOrderRequest);
                });
        }

        // When you supply buyOrderQuantity as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
        [Theory]
        [InlineData(0)]
        public void CreateBuyOrder_QuantityIsLessThanMinimum_ToBeArgumentException(uint buyOrderQuantity)
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = buyOrderQuantity };

            // Act
            Assert.Throws<ArgumentException>(() =>
            {
                _stockService.CreateBuyOrder(buyOrderRequest);
            });
        }

        // When you supply buyOrderQuantity as 100001 (as per the specification, maximum is 100000), it should throw ArgumentException.
        [Theory]
        [InlineData(100001)]
        public void CreateBuyOrder_Quantity_QuantityIsGreaterThanMaximum_ToBeArgumentException(uint buyOrderQuantity)
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = buyOrderQuantity };

            // Act
            Assert.Throws<ArgumentException>(() => 
            {
                _stockService.CreateBuyOrder(buyOrderRequest);
            });
        }

        // When you supply buyOrderPrice as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
        [Theory]
        [InlineData(0)]
        public void CreateBuyOrder_PriceIsLessThanMinimum_ToBeArgumentException(uint buyOrderPrice)
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest { StockSymbol = "MSFT", StockName = "Microsoft", Price = buyOrderPrice, Quantity = 1 };

            // Act
            Assert.Throws<ArgumentException>(() =>
            {
                _stockService.CreateBuyOrder(buyOrderRequest);
            });
        }

        // When you supply buyOrderPrice as 10001 (as per the specification, maximum is 10000), it should throw ArgumentException.
        [Theory]
        [InlineData(10001)]
        public void CreateBuyOrder_PriceIsGreaterThanMaximum_ToBeArgumentException(uint buyOrderPrice)
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest { StockSymbol = "MSFT", StockName = "Microsoft", Price = buyOrderPrice, Quantity = 1 };

            // Act
            Assert.Throws<ArgumentException>(() =>
            {
                _stockService.CreateBuyOrder(buyOrderRequest);
            });
        }

        // When you supply stock symbol=null (as per the specification, stock symbol can't be null), it should throw ArgumentException.
        [Fact]
        public void CreateBuyOrder_StockSymbolIsNull_ToBeArgumentException()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest { StockSymbol = null, StockName = "Microsoft", Price = 1, Quantity = 1 };

            // Act
            Assert.Throws<ArgumentException>(() =>
            {
                _stockService.CreateBuyOrder(buyOrderRequest);
            });
        }

        // When you supply dateAndTimeOfOrder as "1999-12-31" (YYYY-MM-DD) - (as per the specification, it should be equal or newer date than 2000-01-01),
        // it should throw ArgumentException.
        [Fact]
        public void CreateBuyOrder_DateOfOrderIsLessThanYear2000_ToBeArgumentException()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest { StockSymbol = "MSFT", StockName = "Microsoft", DateAndTimeOfOrder=Convert.ToDateTime("1999-12-31"), Price = 1, Quantity = 1 };

            // Act
            Assert.Throws<ArgumentException>(() =>
            {
                _stockService.CreateBuyOrder(buyOrderRequest);
            });
        }

        // If you supply all valid values, it should be successful and return an object of BuyOrderResponse type with auto-generated BuyOrderID (guid).
        [Fact]
        public void CreateBuyOrder_ValidData_ToBeSuccessful()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", 
                DateAndTimeOfOrder = Convert.ToDateTime("2024-12-31"), Price = 1, Quantity = 1 };

            // Act
            BuyOrderResponse buyOrderResponseFromCreate = _stockService.CreateBuyOrder(buyOrderRequest);

            // Assert
            Assert.NotEqual(Guid.Empty, buyOrderResponseFromCreate.BuyOrderID);
        }
        #endregion

        #region CreateSellOrder
            // CreateSellOrder function testing

            // When you supply SellOrderRequest as null, it should throw ArgumentNullException.
            [Fact]
            public void CreateSellOrder_NullSellOrder_ToBeArgumentNullException()
            {
                // Arrange
                SellOrderRequest? sellOrderRequest = null;

                // Act
                Assert.Throws<ArgumentNullException>(() =>
                {
                    _stockService.CreateSellOrder(sellOrderRequest);
                });
            }

            // When you supply sellOrderQuantity as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
            [Theory]
            [InlineData(0)]
            public void CreateSellOrder_QuantityIsLessThanMinimum_ToBeArgumentException(uint sellOrderQuantity)
            {
                // Arrange
                SellOrderRequest? sellOrderRequest = new SellOrderRequest { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = sellOrderQuantity };

                // Act
                Assert.Throws<ArgumentException>(() =>
                {
                    _stockService.CreateSellOrder(sellOrderRequest);
                });
            }

            // When you supply sellOrderQuantity as 100001 (as per the specification, maximum is 100000), it should throw ArgumentException.
            [Theory]
            [InlineData(100001)]
            public void CreateSellOrder_Quantity_QuantityIsGreaterThanMaximum_ToBeArgumentException(uint sellOrderQuantity)
            {
                // Arrange
                SellOrderRequest? sellOrderRequest = new SellOrderRequest { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = sellOrderQuantity };

                // Act
                Assert.Throws<ArgumentException>(() =>
                {
                    _stockService.CreateSellOrder(sellOrderRequest);
                });
            }

            // When you supply sellOrderPrice as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
            [Theory]
            [InlineData(0)]
            public void CreateSellOrder_PriceIsLessThanMinimum_ToBeArgumentException(uint sellOrderPrice)
            {
                // Arrange
                SellOrderRequest? sellOrderRequest = new SellOrderRequest { StockSymbol = "MSFT", StockName = "Microsoft", Price = sellOrderPrice, Quantity = 1 };

                // Act
                Assert.Throws<ArgumentException>(() =>
                {
                    _stockService.CreateSellOrder(sellOrderRequest);
                });
            }

            // When you supply sellOrderPrice as 10001 (as per the specification, maximum is 10000), it should throw ArgumentException.
            [Theory]
            [InlineData(10001)]
            public void CreateSellOrder_PriceIsGreaterThanMaximum_ToBeArgumentException(uint sellOrderPrice)
            {
                // Arrange
                SellOrderRequest? sellOrderRequest = new SellOrderRequest { StockSymbol = "MSFT", StockName = "Microsoft", Price = sellOrderPrice, Quantity = 1 };

                // Act
                Assert.Throws<ArgumentException>(() =>
                {
                    _stockService.CreateSellOrder(sellOrderRequest);
                });
            }

            // When you supply stock symbol=null (as per the specification, stock symbol can't be null), it should throw ArgumentException.
            [Fact]
            public void CreateSellOrder_StockSymbolIsNull_ToBeArgumentException()
            {
                // Arrange
                SellOrderRequest? sellOrderRequest = new SellOrderRequest { StockSymbol = null, StockName = "Microsoft", Price = 1, Quantity = 1 };

                // Act
                Assert.Throws<ArgumentException>(() =>
                {
                    _stockService.CreateSellOrder(sellOrderRequest);
                });
            }

            // When you supply dateAndTimeOfOrder as "1999-12-31" (YYYY-MM-DD) - (as per the specification, it should be equal or newer date than 2000-01-01),
            // it should throw ArgumentException.
            [Fact]
            public void CreateSellOrder_DateOfOrderIsLessThanYear2000_ToBeArgumentException()
            {
                // Arrange
                SellOrderRequest? sellOrderRequest = new SellOrderRequest { StockSymbol = "MSFT", StockName = "Microsoft", DateAndTimeOfOrder = Convert.ToDateTime("1999-12-31"), Price = 1, Quantity = 1 };

                // Act
                Assert.Throws<ArgumentException>(() =>
                {
                    _stockService.CreateSellOrder(sellOrderRequest);
                });
            }

            // If you supply all valid values, it should be successful and return an object of SellOrderResponse type with auto-generated SellOrderID (guid).
            [Fact]
            public void CreateSellOrder_ValidData_ToBeSuccessful()
            {
                // Arrange
                SellOrderRequest? sellOrderRequest = new SellOrderRequest()
                {
                    StockSymbol = "MSFT",
                    StockName = "Microsoft",
                    DateAndTimeOfOrder = Convert.ToDateTime("2024-12-31"),
                    Price = 1,
                    Quantity = 1
                };

                // Act
                SellOrderResponse sellOrderResponseFromCreate = _stockService.CreateSellOrder(sellOrderRequest);

                // Assert
                Assert.NotEqual(Guid.Empty, sellOrderResponseFromCreate.SellOrderID);
            }
        #endregion

        #region GetAllBuyOrders
        // GetAllBuyOrders function testing
        // When you invoke this method, by default, the returned list should be empty.
        [Fact]
        public void GetAllBuyOrders_DefaultList_ToBeEmpty()
        {
            // Act
            List<BuyOrderResponse> buyOrdersFromGet = _stockService.GetBuyOrders();

            // Assert
            Assert.Empty(buyOrdersFromGet);
        }

        // When you first add few buy orders using CreateBuyOrder() method; and then invoke GetAllBuyOrders() method; 
        // the returned list should contain all the same buy orders.
        [Fact]
        public void GetAllBuyOrders_WithFewBuyOrders_ToBeSuccessful()
        {
            // Hard code some data
            BuyOrderRequest buyOrder_request_1 = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = 1, DateAndTimeOfOrder = DateTime.Parse("2023-01-01 9:00") };
            BuyOrderRequest buyOrder_request_2 = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = 1, DateAndTimeOfOrder = DateTime.Parse("2023-01-01 9:00") };

            List<BuyOrderRequest> buyOrders_requests = new List<BuyOrderRequest> { buyOrder_request_1, buyOrder_request_2 };
            List<BuyOrderResponse> buyOrders_response_list_from_add = new List<BuyOrderResponse>();

            // Act
            List <BuyOrderResponse> buyOrders_list_from_get = _stockService.GetBuyOrders();

            // Assert
            foreach(BuyOrderResponse buyOrder_response_from_add in buyOrders_response_list_from_add)
            {
                Assert.Contains(buyOrder_response_from_add, buyOrders_list_from_get);
            }
        }
        #endregion

        #region GetAllSellOrders
        // GetAllSellOrders function testing

        // When you invoke this method, by default, the returned list should be empty.
        [Fact]
        public void GetAllSellOrders_DefaultList_ToBeEmpty()
        {
            // Act
            List<SellOrderResponse> sellOrdersFromGet = _stockService.GetSellOrders();

            // Assert
            Assert.Empty(sellOrdersFromGet);
        }

        // When you first add few buy orders using CreateSellOrder() method; and then invoke GetAllSellOrders() method; 
        // the returned list should contain all the same sell orders.
        [Fact]
        public void GetAllSellOrders_WithFewSellOrders_ToBeSuccessful()
        {
            // Hard code some data
            SellOrderRequest sellOrder_request_1 = new SellOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = 1, DateAndTimeOfOrder = DateTime.Parse("2023-01-01 9:00") };
            SellOrderRequest sellOrder_request_2 = new SellOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = 1, DateAndTimeOfOrder = DateTime.Parse("2023-01-01 9:00") };

            List<SellOrderRequest> sellOrders_requests = new List<SellOrderRequest> { sellOrder_request_1, sellOrder_request_2 };
            List<SellOrderResponse> sellOrders_response_list_from_add = new List<SellOrderResponse>();

            // Act
            List<SellOrderResponse> sellOrders_list_from_get = _stockService.GetSellOrders();

            // Assert
            foreach (SellOrderResponse sellOrder_response_from_add in sellOrders_response_list_from_add)
            {
                Assert.Contains(sellOrder_response_from_add, sellOrders_list_from_get);
            }
        }
        #endregion
    }
}