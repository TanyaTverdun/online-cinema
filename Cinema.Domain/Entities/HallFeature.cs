namespace onlineCinema.Domain.Entities;

public class HallFeature
{
    public int FeatureId { get; set; }
    public Feature Feature { get; set; } = null!;

    public int HallId { get; set; }
    public Hall Hall { get; set; } = null!;
}