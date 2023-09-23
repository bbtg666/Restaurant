using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class OrderTable
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Table")]
        public int TableID { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Table Table { get; set; }
        public UserTableOrder Order { get; set; }
    }
}
