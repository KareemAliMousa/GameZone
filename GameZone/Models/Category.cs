namespace GameZone.Models
{
    public class Category : BaseEntity
    {
        public ICollection<Game> Gemes { get; set; } = new List<Game>();
    }
}
