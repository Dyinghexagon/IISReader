﻿namespace Backend.Models
{
    public class StockBase
    {
        public string Id { get; set; } = string.Empty;
    }

    public enum CalculationType
    {
        Hormonic,
        Arifmetic,
        Square
    }
}