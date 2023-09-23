using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class User
    {
        [Key]
        public int ID { set; get; }
        public string AccountId { set; get; }
        public string? Address { set; get; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
    }
}