namespace GameZone.Models
{
    public class Device : BaseEntity
    {
        [MaxLength(50)]
        public string Icons { get; set; } = string.Empty;
    }
}
