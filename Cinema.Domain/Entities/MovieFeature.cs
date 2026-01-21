namespace onlineCinema.Domain.Entities
{
    public class MovieFeature
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        public int FeatureId { get; set; }
        public Feature Feature { get; set; } = null!;
    }
}
