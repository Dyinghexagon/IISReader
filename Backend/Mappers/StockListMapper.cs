﻿using Backend.Models.Backend;
using Backend.Models.Client;

namespace Backend.Mappers
{
    public class StockListMapper : IModelMapper
    {
        private readonly StockMapper _mapper = new();

        public List<StockList>? Map(List<StockListModel>? stockLists)
        {
            if (stockLists == null)
            {
                return null;
            }

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

        public List<StockListModel>? Map(List<StockList>? stockLists)
        {
            if (stockLists == null)
            {
                return null;
            }

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
                Stocks = _mapper.MapToStockListModel(stockList.Stocks),
                IsNotificated = stockList.IsNotificated
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
                Stocks = _mapper.MapToStockList(stockList.Stocks),
                IsNotificated= stockList.IsNotificated
            };
        }
    }
}
