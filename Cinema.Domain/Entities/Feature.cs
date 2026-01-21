namespace onlineCinema.Domain.Entities
{
    public class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ICollection<MovieFeature> MovieFeatures { get; set; } = new List<MovieFeature>();
    }
}
