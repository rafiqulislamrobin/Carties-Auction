namespace AuctionService.DTOs
{
    public class UpdateAuctionDto
    {
        public string? make { get; set; }
        public string? Model { get; set; }
        public string? Details { get; set; }
        public int? Year { get; set; }
        public string? Color { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime AuctionEnd { get; set; }
    }
}
