namespace onlineCinema.Domain.Enums
{
    public enum MovieStatus : byte
    {
        ComingSoon = 1,
        Released = 2,
        Archived = 3
    }

    public enum AgeRating : byte
    {
        Age0 = 0,
        Age6 = 6,
        Age12 = 12,
        Age16 = 16,
        Age18 = 18
    }

    public enum SeatType : byte
    {
        Standard = 1,
        VIP = 2
    }

    public enum PaymentStatus : byte
    {
        Pending = 0,
        Completed = 1,
        Failed = 2,
        Refunded = 3
    }
}
