using Core.Models.Requests;

namespace Core.Models
{
    public class Cart
    {
        public List<CartItem> CartItems { get; set; }
        public List<MealRequest> Meals { get; set; }
    }

    public class CartItem
    {
        public int MealId { get; set; }
        public int Amount { get; set; }
    }
}
