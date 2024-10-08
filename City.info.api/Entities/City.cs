using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace City.info.api.Entities
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }
        public City(string name)
        {
            Name = name;
        }
        //relation
        public ICollection<PointOfInterest> PointOfInterest { get; set; }=
            new List<PointOfInterest>();
    }
}
