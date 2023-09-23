using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Core.Constants.Enums;

namespace Data.Entities
{
    public class UserTableOrder
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public OrderTableStatus Status { get; set; }

        public User User { get; set; }
        public List<OrderTable> OrderTables { get; set; }
    }
}
