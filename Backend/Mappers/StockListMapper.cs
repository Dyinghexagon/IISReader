using Backend.Models.Backend;
using Backend.Models.Client;

namespace Backend.Mappers
{
    public class StockListMapper : IModelMapper
    {
        private readonly StockMapper _mapper = new();

        public List<StockList>? Map(List<StockListModel>? stocks)
        {
            if (stocks == null)
            {
                return null;
            }

            var result = new List<StockList>();

            foreach (var stock in stocks)
            {
                result.Add(new()
                {
                    Id = stock.Id,
                    Title = stock.Title,
                    Stocks = _mapper.MapToStockList(stock.Stocks),
                    IsNotificated = stock.IsNotificated
                });
            }

            return result;
        }

        public List<StockListModel>? Map(List<StockList>? stocks)
        {
            if (stocks == null)
            {
                return null;
            }

            var result = new List<StockListModel>();

            foreach (var stock in stocks)
            {
                result.Add(new()
                {
                    Id = stock.Id,
                    Title = stock.Title,
                    Stocks = _mapper.MapToStockListModel(stock.Stocks),
                    IsNotificated = stock.IsNotificated
                });
            }

            return result;
        }

        public StockListModel? MapStockList(StockList? stockList)
        {
            if (stockList is null)
            {
                return null;
            }

            return new()
            {
                Id = stockList.Id,
                Title = stockList.Title,
                Stocks = _mapper.MapToStockListModel(stockList.Stocks)
            };
        }

        public StockList? MapStockList(StockListModel? stockList)
        {
            if (stockList is null)
            {
                return null;
            }

            return new()
            {
                Id = stockList.Id,
                Title = stockList.Title,
                Stocks = _mapper.MapToStockList(stockList.Stocks)
            };
        }
    }
}
