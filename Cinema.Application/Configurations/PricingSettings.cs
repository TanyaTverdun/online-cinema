namespace onlineCinema.Application.Configurations;

public class PricingSettings
{
    public decimal DefaultVipCoefficient { get; set; }
    // Можеш додати базову ціну, якщо захочеш
    public decimal DropInPriceDefault { get; set; }
}