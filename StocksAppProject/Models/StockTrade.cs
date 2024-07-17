namespace StocksAppProject.Models
{
    public class StockTrade
    {
        public string? StockSymbol { get; set; }
        public string? StockName { get; set; }
        public double Price { get; set; } = 0;
        public uint Quantity { get; set; } = 0;
    }
}

/*
 *
    This model class will be used to send model object from controller to the "Trade/Index" view.
    It contains the following properties:
    string? StockSymbol
    string? StockName
    double Price
    unit Quantity
 */
