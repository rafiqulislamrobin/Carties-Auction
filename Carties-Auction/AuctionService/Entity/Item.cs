﻿namespace AuctionService.Entity
{
    public class Item
    {
        public Guid Id { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public string? Details { get; set; }
        public int Year { get; set; }
        public string? Color { get; set; }
        public string? ImageUrl { get; set; }

        public Auction? Auction { get; set; }
        public Guid AuctionId { get; set; }
    }
}
