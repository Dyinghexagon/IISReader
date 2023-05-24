using Backend.Models.Backend;
using Backend.Models.Client;

namespace Backend.Mappers
{
    public class StockListMapper : IModelMapper
    {
        private readonly ActualStockMapper _mapper = new();

        public List<StockList> Map(List<StockListModel> stockLists)
        {
            var result = new List<StockList>();

            foreach (var stockList in stockLists)
            {
                var newStockList = MapStockList(stockList);
                if (newStockList is not null)
                {
                    result.Add(newStockList);
                }
            }

            return result;
        }

        public List<StockListModel> Map(List<StockList> stockLists)
        {
            var result = new List<StockListModel>();

            foreach (var stockList in stockLists)
            {
                var newStockList = MapStockList(stockList);
                if (newStockList is not null)
                {
                    result.Add(newStockList);
                }
            }

            return result;
        }

        public StockListModel MapStockList(StockList stockList)
        {
            return new()
            {
                Id = stockList.Id,
                Title = stockList.Title,
                Stocks = _mapper.MapToStockListModel(stockList.Stocks),
                IsNotificated = stockList.IsNotificated
            };
        }

        public StockList MapStockList(StockListModel stockList)
        {
            return new()
            {
                Id = stockList.Id,
                Title = stockList.Title,
                Stocks = _mapper.MapToStockList(stockList.Stocks),
                IsNotificated= stockList.IsNotificated
            };
        }
    }
}
