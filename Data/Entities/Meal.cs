using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Meal")]
    public class Meal
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Decimal Price { get; set; }
        public string URL { get; set; }

        [ForeignKey("MealCategory")]
        public int CategoryID { get; set; }
        public MealCategory MealCategory { get; set; }
    }
}
