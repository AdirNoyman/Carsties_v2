

namespace AuctionService.Entities
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        // The year the car was manufactured
        public int Year { get; set; }
        public string Color { get; set; }
        public int Milage { get; set; }
        public string ImageUrl { get; set; }

        // nav properties set the connection between the two tables (Item and Auction)
        public Auction Auction { get; set; }
        public Guid AuctionId { get; set; }
    }
}