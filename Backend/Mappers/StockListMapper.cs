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
                    Stocks = MapToStockList(stock.Stocks)
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
                    Stocks = MapToStockListModel(stock.Stocks)
                });
            }

            return result;
        }

        private List<Stock>? MapToStockList(List<StockModel>? stockList)
        {
            if (stockList is null)
            {
                return null;
            }

            var stocks = new List<Stock>();

            foreach(var stockModel in stockList)
            {
                var stock = _mapper.Map(stockModel);
                if (stock is not null) {
                    stocks.Add(stock);
                }
            }

            return stocks;
        }

        private List<StockModel>? MapToStockListModel(List<Stock>? stockList)
        {
            if (stockList is null)
            {
                return null;
            }

            var stocks = new List<StockModel>();

            foreach (var stock in stockList)
            {
                var stockModel = _mapper.Map(stock);
                if (stockModel is not null)
                {
                    stocks.Add(stockModel);
                }
            }

            return stocks;
        }
    }
}
