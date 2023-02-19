using System.ComponentModel.DataAnnotations;

namespace BL.Models
{
    public class FoodType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
