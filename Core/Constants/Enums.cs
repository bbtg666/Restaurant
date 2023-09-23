using System.ComponentModel;

namespace Core.Constants
{
    public static class Enums
    {
        public enum OrderMealStatus
        {
            [Description("Order place")]
            OrderPlace = 0,
            [Description("In the kitchen")]
            InTheKitchen = 1,
            [Description("On the way")]
            OnTheWay = 2,
            [Description("Delivered")]
            Delivered = 3,
            [Description("Canceled")]
            Canceled = 4
        }
        public enum OrderTableStatus
        {
            [Description("Order Place")]
            OrderPlace = 0,
            [Description("Successfully Placed")]
            Successfully = 1,
            [Description("Canceled")]
            Canceled = 2
        }
        public enum TableStatus
        {
            [Description("Reserved")]
            Reserved = 0,
            [Description("Empty")]
            Empty = 1,
            [Description("Unable")]
            Unable = 2
        }
    }
}
