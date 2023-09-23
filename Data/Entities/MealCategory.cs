using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("MealCategory")]
    public class MealCategory
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Meal> Meals { get; set; }
    }
}
