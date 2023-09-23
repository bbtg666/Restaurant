using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class OrderMeal
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Meal")]
        public int MealID { get; set; }
        public int Amount { get; set; }
        [ForeignKey("Order")]
        public int OrderID { get; set; }
        public Meal Meal { get; set; }
        public UserMealOrder Order { get; set; }
    }
}
