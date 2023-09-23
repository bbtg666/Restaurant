using System.ComponentModel.DataAnnotations;
using static Core.Constants.Enums;

namespace Data.Entities
{
    public class Table
    {
        [Key]
        public int ID { get; set; }
        public string Location { get; set; }
        public TableStatus Status { get; set; }
    }
}