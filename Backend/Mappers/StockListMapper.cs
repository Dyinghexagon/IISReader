using Backend.Models.Backend;
using Backend.Models.Client;

namespace Backend.Mappers
{
    public class StockListMapper : IModelMapper
    {
        private readonly StockMapper _mapper = new StockMapper();

        public StockList? Map(StockListModel stockList)
        {
            if (stockList is null)
            {
                return null;
            }
            var stocks = new List<Stock>();

            foreach(var stockModel in stockList.Stocks)
            {
                var stock = _mapper.Map(stockModel);
                if (stock is not null) {
                    stocks.Add(stock);
                }
            }

            return new StockList()
            {
                Id = stockList.Id,
                Title = stockList.Title,
                Stocks = stocks
            };
        }

        public StockListModel? Map(StockList stockList)
        {
            if (stockList is null)
            {
                return null;
            }

            var stocks = new List<StockModel>();

            foreach (var stock in stockList.Stocks)
            {
                var stockModel = _mapper.Map(stock);
                if (stockModel is not null)
                {
                    stocks.Add(stockModel);
                }
            }

            return new StockListModel()
            {
                Id = stockList.Id,
                Title = stockList.Title,
                Stocks = stocks
            };
        }
    }
}
