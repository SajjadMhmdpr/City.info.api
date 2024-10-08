using System.ComponentModel.DataAnnotations;


namespace City.info.api.Models
{
    public class PointOfInterestForCreateDto
    {
        [Required(ErrorMessage ="نام الزامیست")]
        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? Description { get; set; }
    }
}
