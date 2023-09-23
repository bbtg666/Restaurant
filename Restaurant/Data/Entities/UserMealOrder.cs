using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Core.Constants.Enums;

namespace Data.Entities
{
    public class UserMealOrder
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public int Status { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }

        public User User { get; set; }
        public List<OrderMeal> OrderMeals { get; set; }
    }
}
