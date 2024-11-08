﻿using AuctionService.Entity;

namespace AuctionService.DTOs
{
    public class AuctionDto
    {
        public Guid Id { get; set; }
        public int? ReservePrice { get; set; }
        public string? Seller { get; set; }
        public string? Winner { get; set; }
        public int? CurrentHighBid { get; set; }
        public int? SoldAmount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime AuctionEnd { get; set; } = DateTime.UtcNow;
        public string? Status { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public string? Details { get; set; }
        public int? Year { get; set; }
        public string? Color { get; set; }
        public string? ImageUrl { get; set; }
    }
}
