﻿namespace Backend.Models.Backend
{
    public class StockChartData
    {
        public Guid Id { get; set; }

        public Double Open { get; set; }

        public Double Close { get; set; }

        public Double Hight { get; set; }

        public Double Low { get; set; }

        public String Time { get; set; } = String.Empty;

    }
}
