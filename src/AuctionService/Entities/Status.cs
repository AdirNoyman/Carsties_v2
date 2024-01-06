namespace AuctionService.Entities
{
    // Status of the auction
    public enum Status
    {
        Live,
        Finished,
        // The bid price was not met with an offer and the auction is now closed
        ReserveNotMet

    }
}